using TMPro;
using UnityEngine;

public class ProfilePopup : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField ageInputField;
    [SerializeField] private GameObject genderBoySelected;
    [SerializeField] private GameObject genderGirlSelected;
    [SerializeField] private string gender;


    void Start()
    {
        SetProfileData();
    }

    private void SetProfileData()
    {
        nameInputField.text = GameManager.Instance.playerPrefs.playerName;
        ageInputField.text = GameManager.Instance.playerPrefs.playerAge;
        Gender_Click(GameManager.Instance.playerPrefs.playerGender);
    }

    public void Gender_Click(string _gender)
    {
        gender = _gender;
        if (_gender.Equals("Boy"))
        {
            genderGirlSelected.SetActive(false);
            genderBoySelected.SetActive(true);
        }
        else if (_gender.Equals("Girl"))
        {
            genderBoySelected.SetActive(false);
            genderGirlSelected.SetActive(true);
        }
        else
        {
            genderBoySelected.SetActive(false);
            genderGirlSelected.SetActive(false);
        }
    }

    public void Submit_Click(Transform _transform)
    {
        if (ValidateInputs())
        {
            GameManager.Instance.playerPrefs.SavePlayerPref(nameInputField.text.Trim(), ageInputField.text.Trim(), gender);
            UIManager.Instance.ButtonTween(_transform);
            UIManager.Instance.popupManager.HideProfilePopup();
        }
        else
            UIManager.Instance.popupManager.ShowToast("Invalid inputs!", UIManager.Instance.popupManager.toastRed);

    }


    private bool ValidateInputs()
    {
        if (string.IsNullOrWhiteSpace(nameInputField.text)) return false;
        if (string.IsNullOrWhiteSpace(ageInputField.text)) return false;
        if (string.IsNullOrWhiteSpace(gender)) return false;

        return true;
    }

}
