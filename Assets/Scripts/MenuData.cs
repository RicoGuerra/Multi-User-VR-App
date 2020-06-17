using System;
using System.IO;
using UnityEngine;

[Serializable]
public class MenuData : MonoBehaviour {

    public string PlayerName;
    public bool ComfortMode;
    public Color Color;

    public void Start() {
        LoadData();
    }

    public void WriteData() {
        //writing menu data into a json file
        string path = Application.persistentDataPath + "/menuData.json";
        string json = JsonUtility.ToJson(new MenuData() { PlayerName = PlayerName, ComfortMode = ComfortMode, Color = Color });
        File.WriteAllText(path, json);
    }

    public void LoadData() {
        LoadMenuData load = new LoadMenuData(gameObject);
        load.LoadData();
    }

    public void SetComfortMode(bool onOff) {
        ComfortMode = onOff;
    }
}
