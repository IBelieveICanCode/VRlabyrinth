using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : LivingBeing, IPlayerCommunicator
{
    Vector3 _startTouchPos;
    Vector3 _endTouchPos;

    [SerializeField]
    private int _distanceOfRaycast = 5;
    public float Speed = 3.5f;
    private const float _gravity = 9.81f;

    private CharacterController _controller;
    public Vector3 Position => transform.position;

    private bool _isMoving = false;

    protected override void Init()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            CheckClickableObjects();
            _startTouchPos = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _endTouchPos = Input.GetTouch(0).position;
            if (_endTouchPos.y > _startTouchPos.y)
                _isMoving = true;
            else if (_endTouchPos.y < _startTouchPos.y)
                _isMoving = false;
        }

        if (_isMoving)
            PlayerMovement();
    }

    public override void Die()
    {
        HUD.Instance.Defeat();
    }
    void PlayerMovement()
    {
        Vector3 direction = Vector3.forward;
        Vector3 velocity = direction * Speed;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y -= _gravity;
        _controller.Move(velocity * Time.deltaTime);
    }

    private void CheckClickableObjects()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _distanceOfRaycast))
        {
            IClickable _clickable = hit.collider.gameObject.GetComponent<IClickable>();
            if (_clickable != null)
                _clickable.ReactToClick();
        }
    }


}
