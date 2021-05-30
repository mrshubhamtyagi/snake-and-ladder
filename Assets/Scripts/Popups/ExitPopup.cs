using UnityEngine;

public class ExitPopup : MonoBehaviour
{

    void Start()
    {

    }

    public void Yes_Click(Transform _transform)
    {
        if (!UIManager.isComingFromGameScreen)
        {
            print("Application Quit");
            Application.Quit();
            return;
        }

        GameManager.Instance.boardScreen.DeInitializeGame();
        UIManager.Instance.popupManager.HideExitPopup();
        UIManager.Instance.ButtonTween(_transform);
        UIManager.Instance.screenManager.ShowHomeScreen();
    }

    public void No_Click(Transform _transform)
    {
        UIManager.Instance.ButtonTween(_transform);
        UIManager.Instance.popupManager.HideExitPopup();
    }
}
