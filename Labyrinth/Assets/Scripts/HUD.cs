using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : Singleton<HUD>
{
    [SerializeField]
    private float _secondsToFade = 3;
    [SerializeField]
    private float _secondsToRestart = 5;
    [SerializeField] private Image fadeImage;

    private void Start()
    {
        fadeImage.gameObject.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Defeat()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(Utility.Fade(_secondsToFade, fadeImage));
        Timer _timer = new Timer(_secondsToRestart + _secondsToFade, GameController.Instance.LoseEvent.Invoke, true);
        _timer.Restart();
    }
}
