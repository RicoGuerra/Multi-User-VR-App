using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule {

    public Camera Camera;
    public SteamVR_Input_Sources TargetSource;
    public SteamVR_Action_Boolean ClickAction;

    private GameObject current;
    public PointerEventData Data { get; private set; }

    protected override void Awake() {
        base.Awake();
        Data = new PointerEventData(eventSystem);
    }

    public override void Process() {
        Data.Reset();
        Data.position = new Vector3(Camera.pixelWidth / 2, Camera.pixelHeight / 2);

        eventSystem.RaycastAll(Data, m_RaycastResultCache);
        Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        current = Data.pointerCurrentRaycast.gameObject;
        
        m_RaycastResultCache.Clear();
        
        HandlePointerExitAndEnter(Data, current);
        
        if (ClickAction.GetLastStateDown(TargetSource))
            ProcessPress(Data);
        if (ClickAction.GetLastStateUp(TargetSource))
            ProcessRelease(Data);
    }

    public void ProcessPress(PointerEventData data) {
        
        Data.pointerPressRaycast = Data.pointerCurrentRaycast;
        
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(current, Data, ExecuteEvents.pointerDownHandler);
        
        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(current);
        
        Data.pressPosition = Data.position;
        Data.pointerPress = newPointerPress;
        Data.rawPointerPress = current;
    }

    public void ProcessRelease(PointerEventData data) {
        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerUpHandler);
        
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(current);
        
        if (Data.pointerPress == pointerUpHandler)
            ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerClickHandler);
        
        eventSystem.SetSelectedGameObject(null);
        
        Data.pressPosition = Vector2.zero;
        Data.pointerPress = null;
        Data.rawPointerPress = null;
    }
}
