using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    Camera cb;
    Text conText;
	// Use this for initialization
	void Start () {
        cb = Camera.main;
        conText = GetComponent<Text>();
	}

    public void setFreeMode()
    {
        conText.text = "Free Mode";
    }

    public void setFixMode()
    {
        conText.text = "Fix Mode";
    }
}
