using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utils;

public class SwitchNaveSprite : MonoBehaviour
{

    [SerializeField] EventSystem eventSystem;
    [SerializeField] RectTransform centerSprite;
    [SerializeField] RectTransform sideSprite;
    [SerializeField] Button btnConfirmarNave;
    [SerializeField] Button btnVoltar;
    
    Vector2 centerSpriteSize, sideSpriteSize;

    [Header("Sprites")] 
    [SerializeField] List<Image> naveSpritesContainer;
    List<Vector2> _currentSpritesPositions;
    [SerializeField] ResourceLoader loader;
    List<Sprite> _loadedNaveSprites;

    bool _isMoving;
    bool isUserSelectingShip;
    
    int indexNextPos;
    int totalNumVisibleSprites;
    int totalLoadedSprites;
    int firstUISpriteIndex;
    int lastUISpriteIndex;
    
    void Awake()
    {
    
        centerSpriteSize = centerSprite.sizeDelta;
        sideSpriteSize = sideSprite.sizeDelta;
        _currentSpritesPositions = new List<Vector2>();
        _loadedNaveSprites = new List<Sprite>();

        for (int i = 0; i < naveSpritesContainer.Count; i++)
        {
            _currentSpritesPositions.Add(naveSpritesContainer[i].rectTransform.anchoredPosition);
        }
        firstUISpriteIndex = 0;
        lastUISpriteIndex = totalNumVisibleSprites-1;
    }

    void Start()
    {
        var ret = loader.GetResources("Nave", typeof(Texture2D));
        for (int i = 0; i < ret.Length; i++)
        {
            try
            {
                Texture2D t = (Texture2D)ret[i];
                Sprite sprite = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), 
                                                               new Vector2(0.5f, 0.5f), 
                                                               100.0f);
                sprite.name = t.name;
                _loadedNaveSprites.Add(sprite);
            }
            catch (Exception e)
            {
                print(e.Message);
            }
        }
        totalLoadedSprites = _loadedNaveSprites.Count;
        //Carregar os sprites na UI
        for (int i = 0; i < naveSpritesContainer.Count; i++)
        {
            naveSpritesContainer[i].sprite = _loadedNaveSprites[i];
        }
    }
    void OnEnable()
    {
        isUserSelectingShip = true;
        totalNumVisibleSprites = naveSpritesContainer.Count;
        btnVoltar.enabled = false;
        btnConfirmarNave.enabled = false;
    }
    void OnDisable()
    {
        isUserSelectingShip = false;
    }
    void OnSwitch(InputValue inputValue)
    {
        if (!_isMoving && isUserSelectingShip && inputValue.Get<Vector2>().x != 0)
        {
            int dir = Math.Sign(inputValue.Get<Vector2>().x) ;
            _isMoving = true;
            StartCoroutine(IEMoving());
            for (int i = 0; i < naveSpritesContainer.Count; i++)
            {
                Vector2 finalPos = _currentSpritesPositions[Mod(i + indexNextPos+dir,totalNumVisibleSprites)];
                Vector2 finalSize = finalPos.x == 0 ? centerSpriteSize : sideSpriteSize;
                naveSpritesContainer[i].GetComponent<MoveShipSprite>().Move(finalPos,finalSize,dir);
            }
            firstUISpriteIndex = Mod(firstUISpriteIndex - dir, totalNumVisibleSprites);
            lastUISpriteIndex = Mod(firstUISpriteIndex + totalNumVisibleSprites-1, totalNumVisibleSprites);
            
            //Tornando o índice circular
            indexNextPos = Mod(indexNextPos+dir, totalNumVisibleSprites);
            int newSpriteIndex;
            int indexToChangeSprite;
            if (dir > 0)
            {
                int currentFirstVisibleSprite = int.Parse(naveSpritesContainer[Mod(firstUISpriteIndex+1,totalNumVisibleSprites)].sprite.name);
                indexToChangeSprite = firstUISpriteIndex;
                newSpriteIndex = Mod(currentFirstVisibleSprite - 2, totalLoadedSprites);  
            }
            else
            {
                int currentFirstVisibleSprite = int.Parse(naveSpritesContainer[Mod(lastUISpriteIndex-1,totalNumVisibleSprites)].sprite.name);
                indexToChangeSprite = lastUISpriteIndex;
                newSpriteIndex = Mod(currentFirstVisibleSprite, totalLoadedSprites);    
            }
            naveSpritesContainer[indexToChangeSprite].sprite = _loadedNaveSprites[newSpriteIndex];
        }
        else if (inputValue.Get<Vector2>().y != 0)
        {
            isUserSelectingShip = false;
            float dir = inputValue.Get<Vector2>().y;
            if (dir < 0)
            {
                btnVoltar.enabled = true;
                btnConfirmarNave.enabled = true;
                eventSystem.SetSelectedGameObject(btnConfirmarNave.gameObject);
            }
            else
            {
                isUserSelectingShip = true;
                btnVoltar.enabled = false;
                btnConfirmarNave.enabled = false;
            }
        }
    }

    public void PersistShipSprite()
    {
        int selectedSpriteIndex = Mod(firstUISpriteIndex + 2,totalNumVisibleSprites);
        int selectSpriteID = int.Parse(naveSpritesContainer[selectedSpriteIndex].sprite.name);
        PlayerPrefs.SetInt(Constants.PP_NAVE_SPRITE,selectSpriteID);
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

    void Reset()
    {
        eventSystem = FindAnyObjectByType<EventSystem>();
    }
}