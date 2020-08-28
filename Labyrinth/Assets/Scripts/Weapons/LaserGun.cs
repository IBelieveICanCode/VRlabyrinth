using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserGun : Weapon
{
    private bool _isShooting = false;
    [SerializeField]
    private Material _matForLaser;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.material = _matForLaser;
        _lineRenderer.enabled = false;
    }

    public override void Use(Transform target)
    {
        if (Time.time > _nextSpawn)
        {
            Shoot(target);
            _nextSpawn = Time.time + _reload;           
        }
        else
            _isShooting = false;
    }

    private void Update()
    {
        if(!_isShooting)
            _lineRenderer.enabled = false;
    }

    protected override void Shoot(Transform target)
    {
        _isShooting = true;
        _lineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                _lineRenderer.SetPosition(1, hit.point);
                _lineRenderer.enabled = true;
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                if (damageable != null)
                    damageable.ReceiveDamage(_damage);
                    
            }
        }        
    }

    protected override void Reset()
    {
        _lineRenderer.enabled = false;
    }
    //public void DrawLaser(LineRenderer line, Vector3 lastPoint)
    //{
    //    line.SetPosition(0, muzzle.transform.position);
    //    line.SetPosition(1, lastPoint);
    //    line.enabled = true;
    //}
}
