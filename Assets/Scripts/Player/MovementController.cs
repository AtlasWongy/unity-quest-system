using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Rigidbody2D _rb;
        private Vector2 _inputMovement;
        private PlayerMovementController _playerMovementController;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerMovementController = new PlayerMovementController();
        }

        private void OnEnable()
        {
            _playerMovementController.Enable();
            _playerMovementController.Player.Movement.performed += OnMovementPerformed;
            _playerMovementController.Player.Movement.canceled += OnMovementCancelled;
        }

        private void OnDisable()
        {
            _playerMovementController.Disable();
            _playerMovementController.Player.Movement.performed -= OnMovementPerformed;
            _playerMovementController.Player.Movement.canceled -= OnMovementCancelled;
        }

        private void FixedUpdate()
        {
            _rb.velocity = _inputMovement * speed;
        }

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            _inputMovement = context.ReadValue<Vector2>();
        }

        private void OnMovementCancelled(InputAction.CallbackContext context)
        {
            _inputMovement = Vector2.zero;
        }
    }
}

