using System;
using TMPro;
using UnityEngine;
using Utils;

public class UIController : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] TMP_Dropdown dropdown;
    
    public Action<InputDeviceEnum> OnInputDeviceChanged;

    void Awake()
    {
        dropdown.value = PlayerPrefs.GetInt(Constants.INPUT_DEVICE);
    }

    public void ChangeInputMovementType(TMP_Dropdown dropdown)
    {
        InputDeviceEnum _device = InputDeviceEnum.Mouse;
        switch (dropdown.options[dropdown.value].text)
        {
            case "Mouse":
                _device = InputDeviceEnum.Mouse;
                break;
            case "Controle":
                _device = InputDeviceEnum.Gamepad;
                break;
        }
        OnInputDeviceChanged?.Invoke(_device);
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
