using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // singleton code
    private static SoundManager instance;
    public GameObject audioSourcePrefab;

    public AudioClip[] towerSounds;
    public AudioClip[] uiSounds;
    public AudioClip[] fxSounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static SoundManager Get { get => instance; }

    public AudioSource menuMusic;
    public AudioSource gameMusic;

    public void StartMenuMusic()
    {
        menuMusic.Play();
        gameMusic.Stop();
    }

    public void StartGameMusic()
    {
        menuMusic.Stop();
        gameMusic.Play(); 
    }

    public void PlayTowerSound(TowerType type)
    {
        int index = 0;
        switch(type)
        {
            case TowerType.Archer:
                index = Random.Range(0, 3);
                break;
            case TowerType.Sword:
                index = Random.Range(3, 6);
                break;
            case TowerType.Wizard:
                index = Random.Range(6, 9);
                break;
        }

        // Instantiate an AudioSource GameObject
        GameObject soundGameObject = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();

        // Configure and play the sound
        audioSource.clip = towerSounds[index];
        audioSource.Play();

        // Destroy the AudioSource GameObject after the clip finishes playing
        Destroy(soundGameObject, towerSounds[index].length);
    }

    public void PlayUISound()
    {
        int index = Random.Range(0, uiSounds.Length);

        // Instantiate an AudioSource GameObject
        GameObject soundGameObject = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();

        // Configure and play the sound
        audioSource.clip = uiSounds[index];
        audioSource.Play();

        // Destroy the AudioSource GameObject after the clip finishes playing
        Destroy(soundGameObject, uiSounds[index].length);
    }

    public void PlayFXSound(int index)
    {
        if (index < 0 || index >= fxSounds.Length) return; // Bounds check

        // Instantiate an AudioSource GameObject
        GameObject soundGameObject = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();

        // Configure and play the sound
        audioSource.clip = fxSounds[index];
        audioSource.Play();

        // Destroy the AudioSource GameObject after the clip finishes playing
        Destroy(soundGameObject, fxSounds[index].length);
    }
}
