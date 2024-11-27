using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    [SerializeField] private GameObject _soundOn;
    [SerializeField] private GameObject _soundOff;
    [SerializeField] private GameObject _musicOn;
    [SerializeField] private GameObject _musicOff;

    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private AudioClip _tapButtonSound;
    [SerializeField] private AudioClip _doorSound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _keySound;
    [SerializeField] private AudioClip _jumpSound;

    private void Start()
    {
        _musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        if (_musicSource.volume == 0.5f)
        {
            _musicOff.SetActive(false);
            _musicOn.SetActive(true);
        }
        else
        {
            _musicOn.SetActive(false);
            _musicOff.SetActive(true);
        }
        _soundSource.volume = PlayerPrefs.GetFloat("SoundsVolume", 1);
        if (_soundSource.volume == 1)
        {
            _soundOff.SetActive(false);
            _soundOn.SetActive(true);
        }
        else
        {
            _soundOn.SetActive(false);
            _soundOff.SetActive(true);
        }
    }

    public void DisableSounds()
    {
        _soundOn.SetActive(false);
        _soundOff.SetActive(true);
        _soundSource.volume = 0;
        PlayerPrefs.SetFloat("SoundsVolume", 0);
        TapButtonSound();
    }

    public void EnableSounds()
    {
        _soundOff.SetActive(false);
        _soundOn.SetActive(true);
        _soundSource.volume = 1;
        PlayerPrefs.SetFloat("SoundsVolume", 1);
        TapButtonSound();
    }

    public void DisableMusic()
    {
        _musicOn.SetActive(false);
        _musicOff.SetActive(true);
        _musicSource.volume = 0;
        PlayerPrefs.SetFloat("MusicVolume", 0);
        TapButtonSound();
    }

    public void EnableMusic()
    {
        _musicOff.SetActive(false);
        _musicOn.SetActive(true);
        _musicSource.volume = 0.5f;
        PlayerPrefs.SetFloat("MusicVolume", 0.5f);
        TapButtonSound();
    }

    public void TapButtonSound()
    {
        _soundSource.PlayOneShot(_tapButtonSound);
    }

    public void DoorSound()
    {
        _soundSource.PlayOneShot(_doorSound);
    }

    public void WinSound()
    {
        _soundSource.PlayOneShot(_winSound);
    }

    public void LoseSound()
    {
        _soundSource.PlayOneShot(_loseSound);
    }

    public void KeySound()
    {
        _soundSource.PlayOneShot(_keySound);
    }

    public void JumpSound()
    {
        _soundSource.PlayOneShot(_jumpSound);
    }
}