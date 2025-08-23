using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SwitchNaveSprite : MonoBehaviour
{

    [SerializeField] List<Sprite> naveSprites;
    [SerializeField] Image chosenSprite;
    [SerializeField] Image leftSprite;
    [SerializeField] Image rightSprite;
    [SerializeField] EventSystem eventSystem;

    bool isUserSelectingShip;
    int index;
    int totalNumSprites;
    void OnEnable()
    {
        isUserSelectingShip = true;
        totalNumSprites = naveSprites.Count;
    }

    void OnDisable()
    {
        isUserSelectingShip = false;
    }

    public void SwitchShipSprite()
    {
        if (isUserSelectingShip)
        {

            if (Keyboard.current.aKey.isPressed)
            {
                index--;
                chosenSprite.sprite = naveSprites[Mathf.Abs(index % totalNumSprites)];
                leftSprite.sprite = naveSprites[Mathf.Abs((index - 1) % totalNumSprites)];
                rightSprite.sprite = naveSprites[Mathf.Abs((index + 1) % totalNumSprites)];
            }
            
        }
    }

    void Update()
    {
        SwitchShipSprite();
    }

    void Reset()
    {
        eventSystem = FindAnyObjectByType<EventSystem>();
    }
}
