using UnityEngine;
using System.Collections.Generic;

public class GlowObject : MonoBehaviour
{
	public Color HoverColor;
    public Color GraspColor;
    public Color ConflictColor;
	public float LerpFactor = 10;


    public bool isConflict;
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


	void Start()
	{
		Renderers = GetComponentsInChildren<Renderer>();

		foreach (var renderer in Renderers)
		{	
			_materials.AddRange(renderer.materials);
		}

    }

    public void OnHoverStart()
	{
		_targetColor = HoverColor;
		enabled = true;
	}

    public void OnHoverEnd()
	{
        if (isConflict) _targetColor = ConflictColor;
        else _targetColor = Color.black;

		enabled = true;
	}

    public void OnConflict()
    {
        _targetColor = ConflictColor;
        enabled = true;
    }

    public void OnGraspBegin()
    {
        _targetColor = GraspColor;
        enabled = true;
    }

    public void OnGraspEnd()
    {
        if (isConflict) _targetColor = ConflictColor;
        else _targetColor = Color.black;

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

		//if (_currentColor.Equals(_targetColor))
		//{
		//	enabled = false;
		//}
	}
}
