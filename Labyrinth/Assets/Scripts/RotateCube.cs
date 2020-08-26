using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float SpinForce = 45;

    private bool _isSpinning = false;

    // Update is called once per frame
    void Update()
    {
        if (_isSpinning)
            transform.Rotate(0, SpinForce * Time.deltaTime, 0);  
        else
            transform.Rotate(0, 0, 0);
    }

    public void ChangeSpeed()
    {
        _isSpinning = !_isSpinning;
    }
}
