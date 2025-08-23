using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigateToUIElement : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Selectable elementToNavigate;

    [SerializeField] List<GameObject> elementsToHide;
    [SerializeField] List<GameObject> elementsToEnable;
    
    public void NavigateToElement()
    {
        eventSystem.SetSelectedGameObject(elementToNavigate.gameObject);
        
        if(elementsToHide.Count > 0)
            elementsToHide.ForEach(e => e.SetActive(false));
        if(elementsToEnable.Count > 0)
            elementsToEnable.ForEach(e => e.SetActive(true));
    }
}