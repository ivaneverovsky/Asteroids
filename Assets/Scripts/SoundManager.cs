using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static AudioClip BulletShoot, AsteroidHit, ShipDead, lazerShoot, ufoKilled, ufoShoot, ufoKillsPlayer, hyperspaceSound;
    static AudioSource audioSource;

    public Slider volumeSlider;

    void Start()
    {
        //game objects and events sounds
        BulletShoot = Resources.Load<AudioClip>("bulletshot");
        AsteroidHit = Resources.Load<AudioClip>("asteroidHit");
        ShipDead = Resources.Load<AudioClip>("shipdead");
        lazerShoot = Resources.Load<AudioClip>("lazer_shoot");
        ufoKilled = Resources.Load<AudioClip>("ufo_killed");
        ufoShoot = Resources.Load<AudioClip>("ufo_shoot");
        ufoKillsPlayer = Resources.Load<AudioClip>("ufo_kills_player");
        hyperspaceSound = Resources.Load<AudioClip>("hyperspace");

        audioSource = GetComponent<AudioSource>();

        //set volume value or reset previous one
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
            Load();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "bulletshot":
                audioSource.PlayOneShot(BulletShoot);
                break;
            case "asteroidHit":
                audioSource.PlayOneShot(AsteroidHit);
                break;
            case "shipdead":
                audioSource.PlayOneShot(ShipDead);
                break;
            case "lazer_shoot":
                audioSource.PlayOneShot(lazerShoot);
                break;
            case "ufo_killed":
                audioSource.PlayOneShot(ufoKilled);
                break;
            case "ufo_shoot":
                audioSource.PlayOneShot(ufoShoot);
                break;
            case "ufo_kills_player":
                audioSource.PlayOneShot(ufoKillsPlayer);
                break;
            case "hyperspace":
                audioSource.PlayOneShot(hyperspaceSound);
                break;
        }
    }
}
