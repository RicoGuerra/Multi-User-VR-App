using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour {

    public InputField Textfield;

    private string myText;

    public enum Example {
        Text,
        Button,
        Nothing
    };

    public Example Options;

    private void Start() {
        myText = GetComponentInChildren<Text>().text;
    }

    public void MakeInput() {
        if (myText == "DEL") {
            Textfield.text = Textfield.text.Remove(Textfield.text.Length - 1);
        } else {
            Textfield.text += myText;
        }
    }
}
