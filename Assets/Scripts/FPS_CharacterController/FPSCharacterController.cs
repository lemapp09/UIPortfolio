using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCharacterController : MonoBehaviour
{
    [Header("Movement Speeds")] [SerializeField]
    private float _walkSpeed = 0.3f;

    [SerializeField] private float _sprintMultiplier = 2.0f;

    [Header("Camera Settings")] [SerializeField]
    private bool _invertYAxis = false;

    [Header("Jump Parameters")] [SerializeField]
    private float _jumpForce = 5.0f;

    [SerializeField] private float _gravity = 9.81f;

    [Header("Look Sensitivity")] [SerializeField]
    private float _mouseSensitivity = 2.0f;

    [SerializeField] private float _upDownRange = 80.0f;

    private CharacterController _characterController;
    private Camera mainCamera;
    private PlayerInputHandler _inputHandler;
    private Vector3 _currentMovement;
    private float _verticalRotation;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        _inputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        float speed = _walkSpeed * (_inputHandler.SprintInput > 0.0f ? _sprintMultiplier : 1.0f);
        Vector3 inputDirection = new Vector3(_inputHandler.MoveInput.x, 0.0f, _inputHandler.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        worldDirection.Normalize();

        _currentMovement.x = worldDirection.x * speed;
        _currentMovement.z = worldDirection.z * speed;

        HandleJumping();
        _characterController.Move(_currentMovement * Time.deltaTime);
    }

    private void HandleJumping()
    {
        if (_characterController.isGrounded)
        {
            _currentMovement.y = -0.5f;
            if (_inputHandler.JumpInput)
            {
                _currentMovement.y = _jumpForce;
            }
        }
        else
        {
            _currentMovement.y -= _gravity * Time.deltaTime;
        }
    }

    private void HandleRotation()
    {
        float mouseYInput = _invertYAxis ? -_inputHandler.LookInput.y : _inputHandler.LookInput.y;
        float mouseXRotation = _inputHandler.LookInput.x * _mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        _verticalRotation -= mouseYInput * _mouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_upDownRange, _upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }
}