using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWindows : MonoBehaviour
{
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private GameObject _gameOverWindow;
    private GameObject targetWindow;
    [SerializeField] private Image _winFrame;
    [SerializeField] private Image _loseFrame;
    [SerializeField] private Image _gameOverFrame;
    private Image targetImage;
    private float duration = 0.5f;
    private Coroutine currentCoroutine;

    [SerializeField] private Image fadeImage;

    private GameAudioController _gameAudioController;

    private void OnEnable()
    {
        _gameAudioController = GetComponent<GameAudioController>();
    }

    public void OpenWin()
    {
        _gameAudioController.WinSound();
        targetWindow = _winWindow;
        targetImage = _winFrame;
        ScaleUp();
    }

    public void OpenLose()
    {
        _gameAudioController.LoseSound();
        targetWindow = _loseWindow;
        targetImage = _loseFrame;
        ScaleUp();
    }

    public void OpenGameOver()
    {
        _gameAudioController.WinSound();
        targetWindow = _gameOverWindow;
        targetImage = _gameOverFrame;
        ScaleUp();
    }

    private void ScaleUp()
    {
        if (targetImage == null) return;
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ScaleTo(new Vector3(1, 1, 1), true));
    }

    private IEnumerator ScaleTo(Vector3 targetScale, bool isActive)
    {
        if (isActive == true) targetWindow.SetActive(isActive);

        Vector3 initialScale = targetImage.rectTransform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            targetImage.rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        targetImage.rectTransform.localScale = targetScale;

        if (isActive == false) targetWindow.SetActive(isActive);
    }

    public void SceneLoadButton(string SceneName)
    {
        StartCoroutine(LoadNewScene(SceneName));
    }

    private IEnumerator LoadNewScene(string SceneName)
    {
        yield return StartCoroutine(FadeIn());
        SceneManager.LoadScene(SceneName);
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
}