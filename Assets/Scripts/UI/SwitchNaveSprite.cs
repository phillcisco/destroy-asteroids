using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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

    [Header("Animation Transforms")] 
    [SerializeField] Image centerSpriteAnim;
    [SerializeField] Image leftSpriteAnim;
    [SerializeField] Image rightSpriteAnim;

    [SerializeField] RectTransform firstPosRect;
    
    Vector2 centerSpritePos, leftSpritePos, rightSpritePos;
    Vector2 centerSpriteSize, sideSpriteSize;

    [Header("Sprites")] 
    [SerializeField] List<Image> naveSpritesContainer;

    List<Vector2> currentSpritesPositions;
    
    [Header("Sprites")] 
    [SerializeField] List<RectTransform> spritesPosition;
    
    Animator _animatorController;
    bool _isMoving;
    bool isUserSelectingShip;
    int index = 1;
    int totalNumSprites;
    //Center sprite
    int selectedSpriteIndex = 2;

    void Awake()
    {
        centerSpritePos = chosenSprite.rectTransform.anchoredPosition;
        centerSpriteSize = chosenSprite.rectTransform.sizeDelta;
        leftSpritePos = leftSprite.rectTransform.anchoredPosition;
        rightSpritePos = rightSprite.rectTransform.anchoredPosition;
        sideSpriteSize = leftSprite.rectTransform.sizeDelta;
        
        _animatorController = GetComponent<Animator>();
        currentSpritesPositions = new List<Vector2>();

        for (int i = 0; i < naveSpritesContainer.Count; i++)
        {
            currentSpritesPositions.Add(naveSpritesContainer[i].rectTransform.anchoredPosition);
        }

        //naveSpritesContainer.ForEach(n => print(n.rectTransform.anchoredPosition.x));
    }

    void OnEnable()
    {
        isUserSelectingShip = true;
        totalNumSprites = naveSpritesContainer.Count;
    }

    void OnDisable()
    {
        isUserSelectingShip = false;
    }

    void OnSwitch(InputValue inputValue)
    {

        if (_isMoving) return;
        if (isUserSelectingShip && inputValue.Get<Vector2>().x != 0)
        {
            int dir = Math.Sign(inputValue.Get<Vector2>().x) ;
            _isMoving = true;
            StartCoroutine(IEMoving());
            for (int i = 0; i < naveSpritesContainer.Count; i++)
            {
                Vector2 finalPos = currentSpritesPositions[Mod(i + (index+dir),totalNumSprites)];
                Vector2 finalSize = finalPos.x == 0 ? centerSpriteSize : sideSpriteSize;
                naveSpritesContainer[i].GetComponent<MoveShipSprite>().Move(finalPos,finalSize,dir);
            }
            index = Mod(index+dir, totalNumSprites);
        }
    }
    IEnumerator IEMoving()
    {
        yield return new WaitForSeconds(0.5f);
        _isMoving = false;
    }
    
    int Mod(int a, int b)
    {
        int m = a % b;
        return m < 0 ? m + b : m;
    }
    
    // public void SwitchShipSprite()
    // {
    //     if (isUserSelectingShip)
    //     {
    //
    //         if (Keyboard.current.aKey.isPressed)
    //         {
    //             index--;
    //             chosenSprite.sprite = naveSprites[Mathf.Abs(index % totalNumSprites)];
    //             leftSprite.sprite = naveSprites[Mathf.Abs((index - 1) % totalNumSprites)];
    //             rightSprite.sprite = naveSprites[Mathf.Abs((index + 1) % totalNumSprites)];
    //         }
    //         
    //     }
    // }

    void Reset()
    {
        eventSystem = FindAnyObjectByType<EventSystem>();
    }
}
