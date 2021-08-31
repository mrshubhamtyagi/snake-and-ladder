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
        if (GameManager.Instance.currentAvatarIndex < BoardManager.Instance.avatars.Length && GameManager.Instance.currentAvatarIndex > -1)
            avatarImage.texture = BoardManager.Instance.avatars[GameManager.Instance.currentAvatarIndex];
        else
            print("INVALID AVATAR INDEX - " + GameManager.Instance.currentAvatarIndex);
    }
}
