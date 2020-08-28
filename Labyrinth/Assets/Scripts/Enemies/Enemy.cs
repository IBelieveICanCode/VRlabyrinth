using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ObjectPool;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingBeing, IClickable
{
    [SerializeField]
    private int _howManyClicksToKill;
    public event EventHandler Death;
    [Space]
    [SerializeField]
    private Transform _eye;
    [SerializeField]
    private FieldOfView _fieldOfView;

    private IPlayerCommunicator _player;
    private NavMeshAgent _navMeshAgent;
    protected GameObject _target;

    [SerializeField]
    protected Weapon _weapon;

    protected override void Init()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameController.Instance.PlayerCommunicator;
        _fieldOfView.SetupView();
        _state = EnemyState.Moving;
    }

    private void Update()
    {
        StatesSwitch();

    }
    public override void Die()
    {
        OnDeath();
    }

    void OnDeath()
    {
        gameObject.SetActive(false);
        Death?.Invoke(this, null);
    }

    public void ReactToClick()
    {
        int damage = ((int)_maxHealth / _howManyClicksToKill) + 1;
        ReceiveDamage(damage);
    }

    #region StatesOfEmeny
    private enum EnemyState
    {
        Waiting,
        Moving,
        Shooting,
    }
    private EnemyState _state;

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
        RotateToTarget();
        _weapon.Use(_target.transform);
        if (_fieldOfView.ReturnTarget() == null)
        {
            _state = EnemyState.Moving;
        }
    }

    private void RotateToTarget()
    {
        if (_target != null)
        {
            Vector3 newDirection = _target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
    #endregion


}
