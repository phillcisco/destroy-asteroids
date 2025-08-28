using System;
using System.Collections;
using UnityEngine;

public class MoveShipSprite : MonoBehaviour
{
   
    [SerializeField] bool isMainSprite;

    RectTransform rectTransform;
    
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Move(Vector2 finalPos, Vector2 finalSize, int direction)
    {
        if ((rectTransform.anchoredPosition.x > finalPos.x && direction > 0) ||
            (finalPos.x > rectTransform.anchoredPosition.x && direction < 0 ))
        {
            Teleport(finalPos, finalSize, direction);
        }
        else
        {
            StartCoroutine(IEMoveSpriteUI(finalPos, finalSize, direction));
        }
    }

    void Teleport(Vector2 finalPos, Vector2 finalSize, int direction)
    {
            rectTransform.anchoredPosition = finalPos;
            rectTransform.sizeDelta = finalSize;
    }
    
    IEnumerator IEMoveSpriteUI(Vector2 finalPos, Vector2 finalSize, int direction)
    {
        
        float deslocamento = 0;
        float totalduration = 0.5f;
        
        Vector2 initialPos = rectTransform.anchoredPosition;
        Vector2 initialSize = rectTransform.sizeDelta;
        while (Mathf.Abs(finalPos.x - rectTransform.anchoredPosition.x)  > 0.1f)
        { 
            rectTransform.anchoredPosition = Vector3.Lerp(initialPos,finalPos,deslocamento/totalduration);
            rectTransform.sizeDelta = Vector3.Lerp(initialSize, finalSize, deslocamento/totalduration);
            deslocamento += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = finalPos;
        rectTransform.sizeDelta = finalSize;
    }
}
