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
	
	// Update is called once per frame
    public void updateSliderText (float value) {
        conText.text = "Convergence " + value;	
	}
}
