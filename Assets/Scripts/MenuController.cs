using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _levelsWindow;

    [SerializeField] private GameObject _backButton;

    private MenuAudioController _menuAudioController;

    private float duration = 0.5f;

    private void Start()
    {
        _menuAudioController = GetComponent<MenuAudioController>();

        _backButton.SetActive(false);
        Color color = fadeImage.color;
        color.a = 1;
        fadeImage.color = color;

        StartCoroutine(FadeOut());
    }

    public void FadeToMenu()
    {
        _menuAudioController.TapButtonSound();
        StartCoroutine(FadeSequence(() =>
        {
            _levelsWindow.SetActive(false);
            _backButton.SetActive(false);
            _menuWindow.SetActive(true);
        }));
    }

    public void FadeToLevels()
    {
        _menuAudioController.TapButtonSound();
        StartCoroutine(FadeSequence(() =>
        {
            _menuWindow.SetActive(false);
            _backButton.SetActive(true);
            _levelsWindow.SetActive(true);
        }));;
    }

    private IEnumerator FadeSequence(System.Action action)
    {
        yield return FadeIn();
        action?.Invoke();
        yield return FadeOut();
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }
}