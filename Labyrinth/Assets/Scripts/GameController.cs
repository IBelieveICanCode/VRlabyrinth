using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [SerializeField]
    private EnemySpawner _enemySpawner;
    [SerializeField]
    private PlayerControl _player;
    public IPlayerCommunicator PlayerCommunicator => _player;

    public UnityEvent WinEvent;
    public UnityEvent LoseEvent;
    private void Start()
    {
        SetupPlayer(_player);
        _enemySpawner.SetupEnemies();
    }

    private void SetupPlayer(PlayerControl _player)
    {
        _player.gameObject.SetActive(true);
        //_player.transform.position = NavMeshUtil.GetRandomPoint(transform.position, 100);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(0);
    }

}
