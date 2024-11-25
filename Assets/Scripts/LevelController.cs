using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Image fadeImage;
    private float fadeDuration = 0.5f;

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isUnlocked = i < currentLevel;
            Transform child = levelButtons[i].transform.GetChild(0);
            if (child != null)
                child.gameObject.SetActive(isUnlocked);

            int levelIndex = i + 1;
            levelButtons[i].interactable = isUnlocked;
            levelButtons[i].onClick.AddListener(() => OnLevelButtonClicked(levelIndex));
        }

        SetFadeAlpha(1f);
        StartCoroutine(FadeOut());
    }

    private void OnLevelButtonClicked(int levelIndex)
    {
        PlayerPrefs.SetInt("CurrentLevel", levelIndex);
        StartCoroutine(LoadGameSceneWithFade());
    }

    private IEnumerator LoadGameSceneWithFade()
    {
        yield return FadeIn();
        SceneManager.LoadScene("GameScene");
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
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

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
    }

    private void SetFadeAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}