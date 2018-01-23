using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour {

    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void AdjustLength(float length)
    {
        renderer.material.SetFloat("_Length", length);
    }
}

