using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public ScreenType desiredScreen;
    private CanvasManager canvasManager;

    private void Awake()
    {
        canvasManager = GetComponentInParent<CanvasManager>();
    }

    public void SwitchCanvas(ScreenType genieDesiredScreen)
    {
        canvasManager.SwitchCanvas(genieDesiredScreen);
    }
    
    public void OnButtonClicked()
    {
        canvasManager.SwitchCanvas(desiredScreen);
    }
}