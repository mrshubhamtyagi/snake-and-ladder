using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [Header("-----Splash Screen-----")]
    [SerializeField] private GameObject splashScreen;
    [SerializeField] private float splashScreenTimer = 2;

    [Header("-----Home Screen-----")]
    [SerializeField] private GameObject homeScreen;

    [Header("-----Splash Screen-----")]
    [SerializeField] private GameObject boardScreen;


    IEnumerator Start()
    {
        UIManager.Instance.currentsScreen = UIManager.ScreenType.Splash;
        splashScreen.SetActive(true);
        homeScreen.SetActive(true);
        boardScreen.SetActive(true);

        yield return new WaitForSeconds(splashScreenTimer);
        UIManager.Instance.ExitTween(splashScreen.transform, UIManager.ScreenType.Home);
    }

    public void ShowHomeScreen()
    {
        UIManager.isComingFromGameScreen = false;
        UIManager.Instance.popupManager.HideExitPopup();
        UIManager.Instance.EnterTween(homeScreen.transform, UIManager.ScreenType.Home);
    }

    public void HideHomeScreen()
    {
        UIManager.Instance.ExitTween(homeScreen.transform, UIManager.ScreenType.Game);
    }

}
