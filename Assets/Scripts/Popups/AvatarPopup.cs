using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AvatarPopup : MonoBehaviour
{
    public RawImage avatarImage;


    void Start()
    {

    }

    void OnEnable()
    {
        GameManager.Instance.audioManager.PauseAndPlayMusic();
        //GameManager.Instance.isAvatarPopupOpen = true;
        if (GameManager.Instance.currentAvatarIndex < BoardManager.Instance.avatars.Length && GameManager.Instance.currentAvatarIndex > -1)
        {
            avatarImage.texture = BoardManager.Instance.avatars[GameManager.Instance.currentAvatarIndex];
            GameManager.Instance.audioManager.PlayPictureSound(GameManager.Instance.currentAvatarIndex);
            StartCoroutine("Co_HidePopup");
        }
        else
            print("INVALID AVATAR INDEX - " + GameManager.Instance.currentAvatarIndex);
    }

    public void Skip_Click()
    {
        StopCoroutine("Co_HidePopup");
        //GameManager.Instance.isAvatarPopupOpen = false;
        GameManager.Instance.audioManager.StopPictureSound();
        GameManager.currentPlayerInfo.isActivePlayer = true;
        GameManager.Instance.currentAvatarIndex = -1;
        GameManager.Instance.audioManager.PauseAndPlayMusic();
        UIManager.Instance.popupManager.HideAvatarPopup();
    }



    private IEnumerator Co_HidePopup()
    {
        yield return new WaitForSeconds(GameManager.Instance.audioManager.GetPictureClipLength(GameManager.Instance.currentAvatarIndex));

        GameManager.Instance.audioManager.StopPictureSound();
        //GameManager.Instance.isAvatarPopupOpen = false;
        yield return new WaitForSeconds(0.25f);
        GameManager.currentPlayerInfo.isActivePlayer = true;
        GameManager.Instance.currentAvatarIndex = -1;
        GameManager.Instance.audioManager.PauseAndPlayMusic();
        UIManager.Instance.popupManager.HideAvatarPopup();
    }

}
