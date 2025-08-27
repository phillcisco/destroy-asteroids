using System;
using System.Collections;
using UnityEngine;

public class MoveShipSprite : MonoBehaviour
{
    [SerializeField] RectTransform previous;
    [SerializeField] RectTransform next;
    
    [SerializeField] bool isMainSprite;

    RectTransform target;
    
    
    void Awake()
    {
        target = GetComponent<RectTransform>();
    }

   
    public void Move(Vector2 finalPos, Vector2 finalSize, int direction)
    {
        if (target.anchoredPosition.x > finalPos.x)
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
            target.anchoredPosition = finalPos;
            target.sizeDelta = finalSize;
    }
    
    IEnumerator IEMoveSpriteUI(Vector2 finalPos, Vector2 finalSize, int direction)
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
            yield return null;
        }
        target.anchoredPosition = finalPos;
        target.sizeDelta = finalSize;
    }
}
