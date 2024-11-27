using UnityEngine;
using UnityEngine.UI;

public class GameLevels : MonoBehaviour
{
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private Text _leveText;
    private GameWindows _gameWindows;

    private void OnEnable()
    {
        _gameWindows = GetComponent<GameWindows>();

        int levelIndex = PlayerPrefs.GetInt("CurrentLevel", 1);
        _levels[levelIndex -1].SetActive(true);

        if (levelIndex >= 9)
        {
            _gameWindows.OpenGameOver();
            _leveText.text = "8";
        }
        else
        {
            _leveText.text = levelIndex.ToString();
        }
    }
}