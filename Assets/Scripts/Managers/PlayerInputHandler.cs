using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    #region Parameters

    [Header("Input Action Asset")] [SerializeField]
    private InputActionAsset _playerControls;

    [Header("Action Map Name References")] [SerializeField]
    private string _actionMapName = "Player";

    [Header("Action NAme References")] [SerializeField]
    private string _moveActionName = "Move";

    [SerializeField] private string _lookActionName = "Look";
    [SerializeField] private string _fireActionName = "Fire";
    [SerializeField] private string _jumpActionName = "Jump";
    [SerializeField] private string _sprintActionName = "Sprint";

    [Header("Dead Zone Min")] // Older controllers can be a bit loose, so we can use a deadzone to prevent jittering
    [SerializeField]
    private float _deadZoneMin = 0.2f;

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _fireAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool FireInput { get; private set; }
    public bool JumpInput { get; private set; }
    public float SprintInput { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_moveActionName);
        _lookAction = _playerControls.FindActionMap(_actionMapName).FindAction(_lookActionName);
        _fireAction = _playerControls.FindActionMap(_actionMapName).FindAction(_fireActionName);
        _jumpAction = _playerControls.FindActionMap(_actionMapName).FindAction(_jumpActionName);
        _sprintAction = _playerControls.FindActionMap(_actionMapName).FindAction(_sprintActionName);
        RegisterInputActions();

        InputSystem.settings.defaultDeadzoneMin = _deadZoneMin;
        // PrintDevices();
    }

    private void PrintDevices()
    {
        foreach (var device in InputSystem.devices)
        {
            if (device.enabled)
            {
                Debug.Log("Active Device:" + device.name);
            }
        }
    }

    private void RegisterInputActions()
    {
        _moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        _lookAction.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        _fireAction.performed += ctx => FireInput = true;
        _jumpAction.performed += ctx => JumpInput = true;
        _sprintAction.performed += ctx => SprintInput = ctx.ReadValue<float>();

        _moveAction.canceled += ctx => MoveInput = Vector2.zero;
        _lookAction.canceled += ctx => LookInput = Vector2.zero;
        _fireAction.canceled += ctx => FireInput = false;
        _jumpAction.canceled += ctx => JumpInput = false;
        _sprintAction.canceled += ctx => SprintInput = 0f;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                Debug.Log("Device Disconnected: " + device.name);
                // Handle device disconnect here.
                break;
            case InputDeviceChange.Reconnected:
                Debug.Log("Device Reconnected: " + device.name);
                // Handle device reconnect here.
                break;
        }
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _lookAction.Enable();
        _fireAction.Enable();
        _jumpAction.Enable();
        _sprintAction.Enable();

        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        _moveAction?.Disable();
        _lookAction?.Disable();
        _fireAction?.Disable();
        _jumpAction?.Disable();
        _sprintAction?.Disable();

        InputSystem.onDeviceChange -= OnDeviceChange;
    }
}