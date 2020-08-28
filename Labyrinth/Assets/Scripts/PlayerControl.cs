using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : LivingBeing, IPlayerCommunicator
{
    [SerializeField]
    private int _distanceOfRaycast = 5;
    public float Speed = 3.5f;

    private const float _gravity = 9.81f;

    private CharacterController _controller;
    public Vector3 Position => transform.position;

    protected override void Init()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CheckClickableObjects();
        }
        PlayerMovement();
    }

    public override void Die()
    {
        HUD.Instance.Defeat();
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

    void PlayerMovement()
    {
        Vector3 direction = Vector3.forward;//new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * Speed;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y -= _gravity;
        _controller.Move(velocity * Time.deltaTime);
    }
}
