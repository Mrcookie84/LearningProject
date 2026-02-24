using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : NetworkBehaviour
{
    public CharacterController characterController;
    public Camera Camera;
    private Vector3 movement;
    [SerializeField] private float speed = 1.2f;

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        movement = new Vector3(1, 0, 0);
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        characterController.Move(new Vector3(movement.x, 0.0f,movement.y) * speed * Time.deltaTime);
    }
}
