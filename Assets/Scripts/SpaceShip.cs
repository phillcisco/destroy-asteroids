using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class SpaceShip : MonoBehaviour
{
   
    
    [SerializeField] float speed;
    [SerializeField] float _fireRate = 0.5f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject bulletLaunchPos;
    
    Camera _mainCamera;
    Rigidbody2D _rb;
    float _yDir;
    float _xPosition;
    float _minY, _maxY;
    float _lastFire;

    bool _isShooting;

    InputDeviceEnum InputMovementDevice;

    PlayerInput _playerInput;
 
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _xPosition = transform.position.x;
        _mainCamera = Camera.main;
        _minY = Camera.main.ViewportToWorldPoint(new Vector2(0,0.15f)).y;
        _maxY = Camera.main.ViewportToWorldPoint(new Vector2(0,0.85f)).y;
   
        InputMovementDevice = (InputDeviceEnum) PlayerPrefs.GetInt(Constants.INPUT_DEVICE,Constants.MOUSE);
        _lastFire = Time.time + _fireRate;
    }
    
    void Start()
    {
        FindAnyObjectByType<UIController>().OnInputDeviceChanged += UpdateInputMovementType;
    }

    public void ReadShootInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _isShooting = true;
        } else if (ctx.canceled)
        {
            _isShooting = false;
        }
    }
    
    public void ReadMovementInput(InputAction.CallbackContext ctx)
    {

        InputDevice type = ctx.control.device;

        if (!type.name.Contains(InputMovementDevice.ToString())) return;
        
        if (ctx.performed)
        {
            if (InputMovementDevice == InputDeviceEnum.Mouse)
            {
                _yDir = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y;
            }
            else
            {
                _yDir = ctx.ReadValue<Vector2>().y;
            }
        }

        if (ctx.canceled && InputMovementDevice != InputDeviceEnum.Mouse) _yDir = 0;
    }
    
    void FixedUpdate()
    {
        MoveShip();
    }

    void Update()
    {
        if (_isShooting && Time.time - _lastFire > _fireRate)
        {
            ShootBullet();
        }
    }

    void MoveShip()
    {
        if (InputMovementDevice == InputDeviceEnum.Mouse)
        {
            Vector2 position = new Vector2(_xPosition,Mathf.Clamp(_yDir, _minY,_maxY));
            _rb.MovePosition(position);
        }
        else
        {
            _rb.linearVelocityY = speed * _yDir;
        }
    }

    void UpdateInputMovementType(InputDeviceEnum device)
    {
        InputMovementDevice = device;
        PlayerPrefs.SetInt(Constants.INPUT_DEVICE,(int)device);
        _yDir = 0;
    }

    void ShootBullet()
    {
        Instantiate(projectilePrefab, bulletLaunchPos.transform.position, Quaternion.identity);
        _lastFire = Time.time;
    }
    
}

[Serializable]
public enum InputDeviceEnum {
    Mouse, Gamepad
}