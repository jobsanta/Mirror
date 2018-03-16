using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowAnchor : MonoBehaviour {


    public float LerpFactor = 10;
    public float delay = 5.0f;
    public Renderer[] Renderers
    {
        get;
        private set;
    }

    public Color CurrentColor
    {
        get { return _currentColor; }
    }

    private List<Material> _materials = new List<Material>();
    private Color _currentColor;
    private Color _targetColor;
    private IEnumerator stopGlow;


    void Start()
    {
        Renderers = GetComponentsInChildren<Renderer>();

        foreach (var renderer in Renderers)
        {
            _materials.AddRange(renderer.materials);
        }

    }

    public void OnColorChanged(Color col)
    {
        _targetColor = col;
        enabled = true;

        stopGlow = OnColorStay();
        StartCoroutine(stopGlow);
    }

    IEnumerator OnColorStay()
    {
        yield return new WaitForSecondsRealtime(delay);
        _targetColor = Color.black;

        enabled = true;
    }

    /// <summary>
    /// Loop over all cached materials and update their color, disable self if we reach our target color.
    /// </summary>
    private void Update()
    {
        _currentColor = Color.Lerp(_currentColor, _targetColor, Time.deltaTime * LerpFactor);

        for (int i = 0; i < _materials.Count; i++)
        {
            _materials[i].SetColor("_GlowColor", _currentColor);
        }

        if (_currentColor.Equals(_targetColor))
        {
            enabled = false;
        }
    }
}
