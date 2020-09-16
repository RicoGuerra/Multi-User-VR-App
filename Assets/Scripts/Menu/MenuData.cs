using System;
using System.IO;
using UnityEngine;

[Serializable]
public class MenuData : MonoBehaviour {

    public string PlayerName;
    public bool ComfortMode;
    public Color Color;

    public void WriteData() {
        string path = Application.persistentDataPath + "/menuData.json";
        string json = JsonUtility.ToJson(new MenuData() { PlayerName = PlayerName, ComfortMode = ComfortMode, Color = Color }, true);
        File.WriteAllText(path, json);
    }

    public void LoadData() {
        LoadMenuData load = new LoadMenuData(gameObject);
        load.LoadData();
    }

    public void SetComfortMode(bool onOff) {
        ComfortMode = onOff;
    }

    public void SetPlayerColor(int color) {
        if (color == 1) {
            Color = Color.red;
        } else if (color == 2) {
            Color = Color.green;
        } else if (color == 3){
            Color = Color.blue;
        }
    }

    public void SetPlayerName(string name) {
        PlayerName = name;
    }
}
