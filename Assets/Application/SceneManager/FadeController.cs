using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    private static FadeController Instance = default;
    [Tooltip("フェードスピード")]
    [SerializeField]
    private float _fadeSpeed = 1f;
    [Tooltip("フェードする画像")]
    [SerializeField]
    private Image _fadeImage = default;
    [Tooltip("開始時の色")]
    [SerializeField]
    private Color _startColor = Color.black;
    private void Awake()
    {
        if(Instance)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        _fadeImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// フェードイン後にアクションする
    /// </summary>
    /// <param name="action"></param>
    public static void StartFadeIn(Action action = null)
    {
        Instance.StartCoroutine(Instance.FadeIn(action));
    }
    /// <summary>
    /// フェードアウト後にアクションする
    /// </summary>
    /// <param name="action"></param>
    public static void StartFadeOut(Action action = null)
    {
        Instance.StartCoroutine(Instance.FadeOut(action));
    }
    public static void StartFadeOutIn(Action outAction = null, Action inAction = null)
    {
        Instance.StartCoroutine(Instance.FadeOutIn(outAction, inAction));
    }
    IEnumerator FadeOutIn(Action outAction, Action inAction)
    {
        yield return FadeOut(outAction);
        yield return FadeIn(inAction);
    }
    IEnumerator FadeIn(Action action)
    {
        _fadeImage.gameObject.SetActive(true);
        yield return FadeIn();
        action?.Invoke();
        _fadeImage.gameObject.SetActive(false);
    }
    IEnumerator FadeOut(Action action)
    {
        _fadeImage.gameObject.SetActive(true);
        yield return FadeOut();
        action?.Invoke();
    }
    IEnumerator FadeIn()
    {
        float clearScale = 1f;
        Color currentColor = _startColor;
        while (clearScale > 0f)
        {
            clearScale -= _fadeSpeed * Time.deltaTime;
            if (clearScale <= 0f)
            {
                clearScale = 0f;
            }
            currentColor.a = clearScale;
            _fadeImage.color = currentColor;
            yield return null;
        }
    }
    IEnumerator FadeOut()
    {
        float clearScale = 0f;
        Color currentColor = _startColor;
        while (clearScale < 1f)
        {
            clearScale += _fadeSpeed * Time.deltaTime;
            if (clearScale >= 1f)
            {
                clearScale = 1f;
            }
            currentColor.a = clearScale;
            _fadeImage.color = currentColor;
            yield return null;
        }
    }
}
