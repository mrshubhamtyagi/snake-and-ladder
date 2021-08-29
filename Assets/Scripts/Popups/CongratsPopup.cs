using UnityEngine;

public class CongratsPopup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Continue_Click(Transform _transform)
    {
        GameManager.Instance.boardScreen.DeInitializeGame();
        UIManager.Instance.popupManager.HideCongratsPopup();
        UIManager.Instance.ButtonTween(_transform);
        UIManager.Instance.screenManager.ShowHomeScreen();
    }
}
