using System;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private int _howManyEnemies = 3;

    public void SetupEnemies()
    {
        for (int i = 0; i < _howManyEnemies; i++)
        {
            CreateEnemy("FirstEnemy");
            CreateEnemy("SecondEnemy");
            CreateEnemy("ThirdEnemy");
        }
    }
    public void CreateEnemy(string tag)
    {
        GameObject enemy = ObjectPooler.Instance.GetPooledObject(tag);
        enemy.transform.position = NavMeshUtil.GetRandomPoint(transform.position.normalized);
        EventHandler handler = null;
        handler = (sender, e) =>
        {
            ResetEnemy(enemy);
            enemy.GetComponent<Enemy>().Death -= handler;          
        };
        enemy.GetComponent<Enemy>().Death += handler;
        enemy.gameObject.SetActive(true);
    }


    private void ResetEnemy(GameObject enemy)
    {
        enemy.transform.position = NavMeshUtil.GetRandomPoint(transform.position.normalized);
        enemy.gameObject.SetActive(true);
    }
}


