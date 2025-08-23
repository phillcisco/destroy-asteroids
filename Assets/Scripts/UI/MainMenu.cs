using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {

        [SerializeField] GameObject mainCanvas;
        [SerializeField] GameObject configCanvas;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        
        //Main Canvas Buttons
        public void EnableConfigCanvas()
        {
            configCanvas.SetActive(!configCanvas.activeSelf);
            mainCanvas.SetActive(!configCanvas.activeSelf);
        }
    }
}

