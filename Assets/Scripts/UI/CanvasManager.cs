using System.Collections.Generic;
using UnityEngine;

public enum ScreenType
{
    NULL = -1,
    Menu,
    CreateRoom,
    JoinRoom,
}

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private ScreenType defaultScreen;

    private List<CanvasController> canvasControllers = new List<CanvasController>();
    private CanvasController lastActiveScreen;
    

    private void Awake()
    {

        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out CanvasController newestController))
            {
                canvasControllers.Add(newestController);
                newestController.gameObject.SetActive(false);
                if (newestController.screenType == defaultScreen)
                {
                    newestController.gameObject.SetActive(true);
                    lastActiveScreen = newestController;
                }
            }
        }
    }

    public void SwitchCanvas(ScreenType type)
    {
        if (lastActiveScreen != null)
        {
            lastActiveScreen.gameObject.SetActive(false);
        }

        CanvasController desiredCanvas = canvasControllers.Find(x => x.screenType == type);

        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            lastActiveScreen = desiredCanvas;
        }
        
    }
}