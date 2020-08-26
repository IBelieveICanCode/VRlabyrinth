using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Material))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    private float _secToChange;
    [SerializeField]
    private Renderer _rend;  
    private Color _color;

    private Timer _timer;


    private void Start()
    {
        _timer = new Timer(_secToChange, ChangeColor);
        _timer.Restart();
    }
    public void ChangeColor()
    {
        _color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        _rend.material.SetColor("_OutlineColor", _color);
    }

}
