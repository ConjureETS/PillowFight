using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioSource MainMenu;
    public AudioSource Gameplay;
    public AudioSource Victory;
    public AudioSource Defeat;

    public static MusicManager Instance
    {
        get { return _instance; }
    }

    private static MusicManager _instance;

	void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlayMainMenuMusic()
    {
        if (Gameplay.isPlaying)
        {
            Gameplay.Stop();
        }

        if (!MainMenu.isPlaying)
        {
            MainMenu.Play();
        }
    }

    public void PlayGameplayMusic()
    {
        if (MainMenu.isPlaying)
        {
            MainMenu.Stop();
        }

        if (!Gameplay.isPlaying)
        {
            Gameplay.Play();
        }
    }

    public void PlayVictoryMusic()
    {
        if (!Victory.isPlaying)
        {
            Victory.Play();
        }
    }

    public void PlayDefeatMusic() {
        if (!Defeat.isPlaying) {
            Defeat.Play();
        }
    }
}
