using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigateToUIElement : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Selectable elementToNavigate;

    public void NavigateToElement()
    {
        eventSystem.SetSelectedGameObject(elementToNavigate.gameObject);
    }
}