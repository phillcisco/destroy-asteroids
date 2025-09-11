using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Nave : MonoBehaviour
{
   
    
    [SerializeField] float speed;
    
    Camera _mainCamera;
    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer; 
    Material _naveMat;
    float _yDir;
    float _xPosition;
    float _minY, _maxY;
    float _intensity, _intensityDir, _intensityRate = 15;
    bool _hasPowerUp;
    
    
    Arma arma;
    [SerializeField] Arma armaPadrao;
    
    bool _isShooting;
    int _spritePos;

    InputDeviceEnum InputMovementDevice;

    PlayerInput _playerInput;
 
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _xPosition = transform.position.x;

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _naveMat = _spriteRenderer.material;
        _mainCamera = Camera.main;
        _minY = Camera.main.ViewportToWorldPoint(new Vector2(0,0.15f)).y;
        _maxY = Camera.main.ViewportToWorldPoint(new Vector2(0,0.85f)).y;
   
        InputMovementDevice = (InputDeviceEnum) PlayerPrefs.GetInt(Constants.INPUT_DEVICE,Constants.MOUSE);
        
        //Iniciando com Arma Padrão
        arma = Instantiate(armaPadrao,transform);
        print(PlayerPrefs.GetInt(Constants.PP_NAVE_SPRITE, 1));
        //Buscando o Sprite
        //ResourceLoader.Instance.GetResource($"Nave/{PlayerPrefs.GetInt(Constants.PP_NAVE_SPRITE, 1)}",typeof(Texture2D));
        
        Texture2D  texture2D = (Texture2D)ResourceLoader.Instance.GetResource($"Nave/{PlayerPrefs.GetInt(Constants.PP_NAVE_SPRITE, 1)}",typeof(Texture2D));    
        Sprite sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), 
            new Vector2(0.5f, 0.5f), 
            100.0f);
        
        SetSprite(sprite);

    }
    
    void Start()
    {
        FindAnyObjectByType<UIController>().OnInputDeviceChanged += UpdateInputMovementType;
    }

    void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
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
        if (_isShooting)
        {
            Atirar();
        }
        if(_hasPowerUp)
            BlinkSpaceShipVFX();
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

    public void Atirar()
    {
        arma.Atirar();
    }

    public void SetArma(Arma arma)
    {
        Destroy(this.arma.gameObject);
        this.arma = Instantiate(arma,transform);
        ApplyPowerUp();
    }
    
    void BlinkSpaceShipVFX()
    {
        _intensity += _intensityRate * Time.deltaTime * _intensityDir;
        _naveMat.color = new Color(_intensity,_intensity,_intensity);
        if (_intensity >= 3) _intensityDir = -1;
        if (_intensity <= 1) _intensityDir = 1;
    }

    void ApplyPowerUp()
    {
        _hasPowerUp = true;
        StartCoroutine(IEApplyPowerup());
    }

    IEnumerator IEApplyPowerup()
    {
        yield return new WaitForSeconds(3);
        _hasPowerUp = false;
        Destroy(this.arma.gameObject);
        arma = Instantiate(armaPadrao,transform);
        _naveMat.color = new Color(1,1,1);
    }
}

[Serializable]
public enum InputDeviceEnum {
    Mouse, Gamepad
}