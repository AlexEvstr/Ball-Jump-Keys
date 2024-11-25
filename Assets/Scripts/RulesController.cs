using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RulesController : MonoBehaviour
{
    [SerializeField] private GameObject RulesWindow;
    [SerializeField] private Image targetImage;
    private float duration = 0.5f;
    private Coroutine currentCoroutine;

    public void ScaleUp()
    {
        if (targetImage == null) return;
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ScaleTo(new Vector3(1, 1, 1), true));
    }

    public void ScaleDown()
    {
        if (targetImage == null) return;
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ScaleTo(new Vector3(0, 0, 0), false));
    }

    private IEnumerator ScaleTo(Vector3 targetScale, bool isActive)
    {
        if (isActive == true) RulesWindow.SetActive(isActive);

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

        if (isActive == false) RulesWindow.SetActive(isActive);
    }
}