using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        HUD.Instance.UpdateHealth(_health);
    }

    public override void ReceiveDamage(float damage)
    {
        base.ReceiveDamage(damage);
        HUD.Instance.UpdateHealth(_health);
    }

    void Update()
    {

    #if UNITY_EDITOR
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));;
        PlayerMovement(moveInput);
        if (Input.GetKey(KeyCode.Mouse0))
            CheckClickableObjects();

    #endif
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            CheckClickableObjects();
            _startTouchPos = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _endTouchPos = Input.GetTouch(0).position;
            if (_endTouchPos.y > _startTouchPos.y && (_endTouchPos - _startTouchPos).magnitude > 5f)
                _isMoving = true;
            else if (_endTouchPos.y < _startTouchPos.y && (_endTouchPos - _startTouchPos).magnitude > 5f)
                _isMoving = false;
        }

        if (_isMoving)
            PlayerMovement(Vector3.forward);
    }

    public override void Die()
    {
        HUD.Instance.Defeat();
    }
    void PlayerMovement(Vector3 direction)
    {
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
