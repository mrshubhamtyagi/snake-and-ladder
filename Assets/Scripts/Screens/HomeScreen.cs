using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] private GameObject p2Selected;
    [SerializeField] private GameObject p3Selected;
    [SerializeField] private GameObject p4Selected;

    void Start()
    {
        Players_Click(GameManager.Instance.playerCount);
    }


    public void Players_Click(int _count)
    {
        GameManager.Instance.playerCount = _count;
        switch (_count)
        {
            case 2:
                UIManager.Instance.ButtonTween(p2Selected.transform);
                p2Selected.SetActive(true);
                p3Selected.SetActive(false);
                p4Selected.SetActive(false);
                break;

            case 3:
                UIManager.Instance.ButtonTween(p3Selected.transform);
                p3Selected.SetActive(true);
                p2Selected.SetActive(false);
                p4Selected.SetActive(false);
                break;

            case 4:
                UIManager.Instance.ButtonTween(p4Selected.transform);
                p4Selected.SetActive(true);
                p2Selected.SetActive(false);
                p3Selected.SetActive(false);
                break;
        }
    }

    public void Profile_Click(Transform _transform)
    {
        UIManager.Instance.ButtonTween(_transform);
        UIManager.Instance.popupManager.ShowProfilePopup();
    }


    public void Settings_Click(Transform _transform)
    {
        UIManager.Instance.ButtonTween(_transform);
        UIManager.Instance.popupManager.ShowSettingsPopup();
    }


    public void Play_Click(Transform _transform)
    {
        GameManager.Instance.boardScreen.InitializeGame();
        UIManager.Instance.ButtonTween(_transform);
        UIManager.Instance.screenManager.HideHomeScreen();
    }

}
