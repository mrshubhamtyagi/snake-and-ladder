using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPopup;
    [SerializeField] private GameObject profilePopup;
    [SerializeField] private GameObject exitPopup;
    [SerializeField] private GameObject congratsPopup;
    [SerializeField] private GameObject avatarPopup;

    [Header("-----Toast-----")]
    public GameObject toast;
    public TextMeshProUGUI toastTMP;
    public Color toastRed = Color.red;
    public Color toastGreen = Color.green;


    void Start()
    {
        settingsPopup.SetActive(false);
        profilePopup.SetActive(false);
        exitPopup.SetActive(false);
        congratsPopup.SetActive(false);
        toast.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UIManager.Instance.currentsScreen == UIManager.ScreenType.Home)
            UIManager.Instance.popupManager.ShowExitPopup();

    }

    public void ShowProfilePopup()
    {
        UIManager.Instance.EnterTween(profilePopup.transform, UIManager.ScreenType.Popup);
    }

    public void HideProfilePopup()
    {
        UIManager.Instance.ExitTween(profilePopup.transform, UIManager.ScreenType.Home);
    }



    public void ShowSettingsPopup()
    {
        UIManager.Instance.EnterTween(settingsPopup.transform, UIManager.ScreenType.Popup);
    }

    public void HideSettingsPopup()
    {
        UIManager.Instance.ExitTween(settingsPopup.transform, UIManager.ScreenType.Home);
    }



    public void ShowExitPopup()
    {
        if (GameManager.currentPlayerInfo)
            GameManager.currentPlayerInfo.isActivePlayer = false;
        UIManager.Instance.EnterTween(exitPopup.transform, UIManager.ScreenType.Popup);
    }

    public void HideExitPopup()
    {
        if (GameManager.currentPlayerInfo)
            GameManager.currentPlayerInfo.isActivePlayer = true;
        UIManager.Instance.ExitTween(exitPopup.transform, UIManager.isComingFromGameScreen ? UIManager.ScreenType.Game : UIManager.ScreenType.Home);
    }



    public void ShowCongratsPopup()
    {
        UIManager.Instance.EnterTween(congratsPopup.transform, UIManager.ScreenType.Popup);
    }

    public void HideCongratsPopup()
    {
        UIManager.Instance.ExitTween(congratsPopup.transform, UIManager.ScreenType.Home);
    }


    public void ShowAvatarPopup()
    {
        UIManager.Instance.EnterTween(avatarPopup.transform, UIManager.ScreenType.Popup);
    }

    public void HideAvatarPopup()
    {
        UIManager.Instance.ExitTween(avatarPopup.transform, UIManager.ScreenType.Game);
    }

    public void ShowToast(string _msg, Color _color, int _holdTime = 2)
    {
        toastTMP.color = _color;
        toastTMP.text = _msg;
        toast.SetActive(true);
        Invoke("HideToast", _holdTime);
    }

    private void HideToast()
    {
        toastTMP.color = Color.white;
        toastTMP.text = "";
        toast.SetActive(false);
    }
}
