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
        Vector3 newPos = NavMeshUtil.GetRandomPoint(transform.position);
        _player.transform.position = new Vector3(newPos.x, 1, newPos.z);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(0);
    }

}
