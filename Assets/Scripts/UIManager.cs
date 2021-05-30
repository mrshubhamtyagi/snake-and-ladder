using UnityEngine;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    public ScreenManager screenManager;
    public PopupManager popupManager;
    public ScreenType currentsScreen = ScreenType.Splash;

    [Header("-----Settings-----")]
    public Ease enterEase = Ease.OutQuint;
    public Ease exitEase = Ease.InQuint;
    public float animDuration = 1;
    public float scaleFactor = 5;
    [Space(20)]
    public Ease buttonEase = Ease.InQuint;
    public float buttonAnimDuration = 1;
    public int vibrato = 5;
    public float buttonScalefactor = 0.1f;
    [Space(20)]
    public float diceAnimDuration = 2;
    public float diceAnimDelay = 0.2f;

    public enum ScreenType { Splash, Home, Game, Popup };

    public static bool isComingFromGameScreen = false;

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    void Start()
    {

    }


    public void EnterTween(Transform _transform, ScreenType _screen)
    {
        currentsScreen = _screen;
        _transform.localScale = Vector3.one * scaleFactor;
        _transform.gameObject.SetActive(true);
        _transform.DOScale(Vector3.one, animDuration).SetEase(enterEase);
    }

    public void ExitTween(Transform _transform, ScreenType _screen)
    {
        currentsScreen = _screen;
        _transform.DOScale(Vector3.one * scaleFactor, animDuration).SetEase(exitEase).OnComplete(delegate { _transform.gameObject.SetActive(false); });
    }

    public void ButtonTween(Transform _transform, Action _onComplete = null)
    {
        _transform.DOPunchScale(Vector3.one * buttonScalefactor, buttonAnimDuration, vibrato).SetEase(buttonEase).OnComplete(delegate { _onComplete?.Invoke(); });
    }
}
