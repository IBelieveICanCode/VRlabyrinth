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
        if (Time.time > _nextSpawn && !_isShooting)
        {
            _isShooting = true;
            Shoot(target);
            _nextSpawn = Time.time + _reload;
            //_lineRenderer.enabled = false;
        }
        //else if (Time.time < _nextSpawn)
        //    _lineRenderer.enabled = false;
    }

    protected override void Shoot(Transform target)
    {
        _lineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                _lineRenderer.SetPosition(1, hit.point);
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                if (damageable != null)
                    damageable.ReceiveDamage(_damage);
                StartCoroutine(LaserShoot());   
            }
        }        
    }

    private IEnumerator LaserShoot()
    {
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _lineRenderer.enabled = false;
        _isShooting = false;
    }

    protected override void Reset()
    {
        _lineRenderer.enabled = false;
    }
    public void DrawLaser(LineRenderer line, Vector3 lastPoint)
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, lastPoint);
        line.enabled = true;
    }
}
