using System.IO;
using UnityEngine;

public class LoadMenuData {

    private MenuData m;
    private GameObject GameObject;

    public LoadMenuData(GameObject gameObject) {
        GameObject = gameObject;
        m = new MenuData();
    }

    public void LoadData() {
        string path = Application.persistentDataPath + "/menuData.json";
        string dataString = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(dataString, m);
        if (GameObject.tag == "Player") {
            GameObject.GetComponent<PlayerManager>().PlayerName = m.PlayerName;
            GameObject.GetComponent<PlayerManager>().ComfortMode = m.ComfortMode;
            GameObject.GetComponent<PlayerManager>().PlayerColor = m.Color;
        }
    }
}
