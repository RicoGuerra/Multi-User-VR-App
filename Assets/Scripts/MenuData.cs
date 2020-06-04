using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class MenuData : MonoBehaviour {

    public string PlayerName;
    public bool ComfortMode;
    public Color Color;

    public void WriteData() {
        //writing menu data into a json file
        string path = Application.persistentDataPath + "/menuData.json";
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(path, json);
    }
}
