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
    List<Vector2> _currentSpritesPositions;
    [SerializeField] ResourceLoader loader;
    
    Animator _animatorController;
    bool _isMoving;
    bool isUserSelectingShip;
    int index;
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
        _currentSpritesPositions = new List<Vector2>();

        for (int i = 0; i < naveSpritesContainer.Count; i++)
        {
            _currentSpritesPositions.Add(naveSpritesContainer[i].rectTransform.anchoredPosition);
        }
    }

    private void Start()
    {
        var ret = loader.GetResources("Nave", typeof(Texture2D));

        int spriteSCounter = 0;
        for (int i = 0; i < ret.Length; i++)
        {
            
            try
            {
                
                print(i);
                Texture2D t = (Texture2D)ret[i];
                Sprite sprite = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), 
                                                               new Vector2(0.5f, 0.5f), 
                                                               100.0f);
                sprite.name = t.name;
                naveSpritesContainer[spriteSCounter].sprite = sprite;
   
                spriteSCounter++;
                // if (spriteSCounter == 2)
                //     break;
            }
            catch (Exception e)
            {
                print(e.Message);
            }
        }

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
            print("apertou");
            int dir = Math.Sign(inputValue.Get<Vector2>().x) ;
            _isMoving = true;
            StartCoroutine(IEMoving());
            for (int i = 0; i < naveSpritesContainer.Count; i++)
            {
                Vector2 finalPos = _currentSpritesPositions[Mod(i + (index+dir),totalNumSprites)];
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
