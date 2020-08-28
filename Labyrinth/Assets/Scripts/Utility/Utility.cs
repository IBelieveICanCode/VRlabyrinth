using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utility 
{
    public static IEnumerator Fade(float time, Graphic fadePlane)
    {
        var _from = fadePlane.color;
        var _to = new Color(0, 0, 0);

        var _speed = 1 / time;
        float _percent = 0;
        while (_percent < 1)
        {
            _percent += Time.deltaTime * _speed;
            fadePlane.color = Color.Lerp(_from, _to, _percent);
            yield return null;
        }
    }
}
