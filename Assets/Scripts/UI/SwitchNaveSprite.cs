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

    int nextPosOffset;
    
    [Header("Sprites")] 
    [SerializeField] List<RectTransform> spritesPosition;
    
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
        currentSpritesPositions = new List<Vector2>();
        naveSpritesContainer.ForEach(n => currentSpritesPositions.Add(n.rectTransform.anchoredPosition));
    }

    void OnEnable()
    {
        isUserSelectingShip = true;
        totalNumSprites = naveSprites.Count;
    }

    void OnDisable()
    {
        isUserSelectingShip = false;
    }

    void OnSwitch(InputValue inputValue)
    {
        
        
        if (_isMoving) return;
        if (isUserSelectingShip)
        {
            _isMoving = true;
            StartCoroutine(IEMoving());
            print("aa");
            nextPosOffset++;
            
            for (int i = 0; i < naveSpritesContainer.Count; i++)
            {
                print(i);
                print(naveSpritesContainer.Count);
                Vector2 finalPos;

                if (Mod(i + 1, totalNumSprites) == 0)
                    finalPos = currentSpritesPositions[0];
                else
                    finalPos = currentSpritesPositions[Mod(i + 1,totalNumSprites)];
              
                Vector2 finalSize = finalPos.x == 0 ? centerSpriteSize : sideSpriteSize;
                naveSpritesContainer[i].GetComponent<MoveShipSprite>().Move(finalPos,finalSize,1);
                currentSpritesPositions[i] = finalPos;
            }
            

            
            //StartCoroutine(IEMoveSpriteUI(naveSpritesContainer[2].rectTransform, rightSpritePos, sideSpriteSize, 1));
            // chosenSprite.gameObject.SetActive(false);
            // _animatorController.Play("NaveSwitchLeft");
            // float dir = inputValue.Get<Vector2>().x;
            // index -= (int)dir;
            //
            // chosenSprite.sprite = naveSprites[Mod(index,totalNumSprites)];
            // leftSprite.sprite = naveSprites[Mod(index - 1,totalNumSprites)];
            // rightSprite.sprite = naveSprites[Mod(index + 1,totalNumSprites)];
        }
    }

    IEnumerator IEMoving()
    {
        yield return new WaitForSeconds(1);
        _isMoving = false;
    }
    
    public void AnimationEnded()
    {
        // //Correct positions and sizes
        // chosenSprite.rectTransform.anchoredPosition = centerSpritePos;
        // chosenSprite.rectTransform.sizeDelta = centerSpriteSize;
        // centerSpriteAnim.rectTransform.anchoredPosition = centerSpritePos;
        // centerSpriteAnim.rectTransform.sizeDelta = centerSpriteSize;
        // centerSpriteAnim.sprite = chosenSprite.sprite;
        //
        // leftSprite.rectTransform.anchoredPosition = leftSpritePos;
        // leftSpriteAnim.rectTransform.anchoredPosition = leftSpritePos;
        //
        // leftSprite.rectTransform.sizeDelta = sideSpriteSize;
        // leftSpriteAnim.rectTransform.sizeDelta = sideSpriteSize;
        //
        // rightSprite.rectTransform.anchoredPosition = rightSpritePos;
        // rightSpriteAnim.rectTransform.anchoredPosition = rightSpritePos;
        //
        // rightSprite.rectTransform.sizeDelta = sideSpriteSize;
        // rightSpriteAnim.rectTransform.sizeDelta = sideSpriteSize;
        //
        //
        //
        // //Copy sprites
        // rightSpriteAnim.sprite = rightSprite.sprite;
        // leftSpriteAnim.sprite = leftSprite.sprite;
        //
        //
        // print("Animacao acabou");
        // chosenSprite.gameObject.SetActive(true);
        
    }
    
    int Mod(int a, int b)
    {
        return ((a % b) + b) % b;
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

    IEnumerator IEMoveSpriteUI(RectTransform target, Vector2 finalPos, Vector2 finalSize, int direction)
    {
        
        float deslocamento = 0;
        float totalduration = 1f;
       
        Vector2 initialPos = target.anchoredPosition;
        Vector2 initialSize = target.sizeDelta;
        while (Mathf.Abs(finalPos.x - target.anchoredPosition.x)  > 0.1f)
        { 
            
            target.anchoredPosition = Vector3.Lerp(initialPos,finalPos,deslocamento/totalduration);
            target.sizeDelta = Vector3.Lerp(initialSize, finalSize, deslocamento/totalduration);
            deslocamento += Time.deltaTime;
            print("a");
            yield return null;
        }

        target.anchoredPosition = finalPos;
        target.sizeDelta = finalSize;

    }

    void Update()
    {
        //SwitchShipSprite();
    }

    void Reset()
    {
        eventSystem = FindAnyObjectByType<EventSystem>();
    }
}
