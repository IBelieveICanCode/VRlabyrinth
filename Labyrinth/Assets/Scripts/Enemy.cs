using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingBeing
{
    [Space]
    [SerializeField]
    private Transform _eye;
    [SerializeField]
    private FieldOfView _fieldOfView;

    private IPlayerCommunicator _player;
    private NavMeshAgent _navMeshAgent;
    private GameObject _target;

    private enum EnemyState 
    {
        Waiting,
        Moving,
        Shooting,
    }
    private EnemyState _state;

    protected override void Init()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerControl>();       
        _state = EnemyState.Moving;
    }

    private void Update()
    {
        StatesSwitch();
    }

    private void StatesSwitch()
    {
        switch (_state)
        {
            case EnemyState.Waiting:
                Stop();
                break;
            case EnemyState.Moving:
                MoveToPlayer();
                break;
            case EnemyState.Shooting:
                ShootTarget();
                break;
        }
    }
    private void Stop()
    {
        _navMeshAgent.velocity = Vector3.zero;
        _navMeshAgent.isStopped = true;
    }

    private void MoveToPlayer()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.destination = _player.Position;
        if (_fieldOfView.ReturnTarget() != null)
        {
            _target = _fieldOfView.ReturnTarget();
            _state = EnemyState.Shooting;
        }
    }

    private void ShootTarget()
    {
        Stop();
        transform.rotation = Quaternion.LookRotation(_target.transform.position);
        _weapon.Use();
    }

}
