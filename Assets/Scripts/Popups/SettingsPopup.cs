using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private GameObject englishSelected;
    [SerializeField] private GameObject tamilSelected;
    [SerializeField] private int language = 0;


    void Start()
    {
        language = (int)GameManager.Instance.playerPrefs.language;
        if (language == 0)
        {
            englishSelected.SetActive(true);
            tamilSelected.SetActive(false);
        }
        else
        {
            tamilSelected.SetActive(true);
            englishSelected.SetActive(false);
        }
    }

    public void Language_Click(int _language)
    {
        language = _language;
        if (language == 0)
        {
            englishSelected.SetActive(true);
            tamilSelected.SetActive(false);
        }
        else
        {
            tamilSelected.SetActive(true);
            englishSelected.SetActive(false);
        }
    }

    public void Submit_Click(Transform _transform)
    {
        GameManager.Instance.playerPrefs.SaveLanguagePrefs(language);
        UIManager.Instance.ButtonTween(_transform);
        UIManager.Instance.popupManager.HideSettingsPopup();
    }
}
