using UnityEngine;

[System.Serializable]
public class PlayerPrefsManager
{
    public string playerName = "";
    public string playerAge = "";
    public string playerGender = "";
    public GameManager.Language language = GameManager.Language.English;

    private readonly string playerNameKey = "PlayerName";
    private readonly string playerAgeKey = "PlayerAge";
    private readonly string playerGenderKey = "PlayerGender";
    private readonly string playerLanguageKey = "Language";

    public void SavePlayerPref(string _name = "", string _age = "", string _gender = "")
    {
        playerName = string.IsNullOrWhiteSpace(_name) ? playerName : _name;
        PlayerPrefs.SetString(playerNameKey, playerName);

        playerAge = string.IsNullOrWhiteSpace(_age) ? playerAge : _age;
        PlayerPrefs.SetString(playerAgeKey, playerAge);

        playerGender = string.IsNullOrWhiteSpace(_gender) ? playerGender : _gender;
        PlayerPrefs.SetString(playerGenderKey, playerGender);
    }

    public void SaveLanguagePrefs(int _language)
    {
        language = (GameManager.Language)_language;
        PlayerPrefs.SetInt(playerLanguageKey, _language);
    }


    public void ReadPlayerPrefs()
    {
        playerName = PlayerPrefs.HasKey(playerNameKey) ? PlayerPrefs.GetString(playerNameKey) : "";
        playerAge = PlayerPrefs.HasKey(playerAgeKey) ? PlayerPrefs.GetString(playerAgeKey) : "";
        playerGender = PlayerPrefs.HasKey(playerGenderKey) ? PlayerPrefs.GetString(playerGenderKey) : "";
        language = PlayerPrefs.HasKey(playerLanguageKey) ? (GameManager.Language)PlayerPrefs.GetInt(playerLanguageKey) : GameManager.Language.English;
    }


}
