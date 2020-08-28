using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : Singleton<HUD>
{
    [SerializeField]
    private TMP_Text healthText;
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
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void Defeat()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(Utility.Fade(_secondsToFade, fadeImage));
        Timer _timer = new Timer(_secondsToRestart + _secondsToFade, GameController.Instance.LoseEvent.Invoke, true);
        _timer.Restart();
    }

    public void UpdateHealth(float health)
    {
        healthText.text = health.ToString();
    }
}
