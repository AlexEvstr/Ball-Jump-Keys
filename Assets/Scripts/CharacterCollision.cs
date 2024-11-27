using System.Collections;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    [SerializeField] private GameWindows _gameWindows;
    [SerializeField] private Timer _timer;
    [SerializeField] private GameAudioController _gameAudioController;
    private GameObject _door;
    private float duration = 0.5f;

    private void Start()
    {
        _door = GameObject.FindGameObjectWithTag("Door");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.SetActive(false);
            _door.transform.GetChild(1).gameObject.SetActive(false);
            _door.transform.GetChild(0).gameObject.SetActive(true);
            _gameAudioController.KeySound();
        }

        else if (collision.gameObject.CompareTag("OpenedDoor"))
        {
            _gameAudioController.DoorSound();
            _timer.isTimerRunning = false;
            StartCoroutine(ScaleToZero());
            int levelIndex = PlayerPrefs.GetInt("CurrentLevel", 1);
            levelIndex++;
            PlayerPrefs.SetInt("CurrentLevel", levelIndex);

            int bestLevel = PlayerPrefs.GetInt("BestLevel", 1);
            if (bestLevel < levelIndex)
            {
                bestLevel = levelIndex;
                PlayerPrefs.SetInt("BestLevel", bestLevel);
            }
        }
    }

    private IEnumerator ScaleToZero()
    {
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        _gameWindows.OpenWin();
    }
}