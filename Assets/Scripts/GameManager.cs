using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;

    public ParticleSystem explosionGR;
    public ParticleSystem explosionRD;

    public int score = 0;
    public int lazerCount;

    public bool playerIsDead = false;

    public GameObject ScoreTXT;
    public GameObject CoordinatesTXT;
    public GameObject AngleTXT;
    public GameObject SpeedTXT;
    public GameObject LazerTXT;
    public Image filler;

    public GameObject message;
    public GameObject result;

    public float respawnTime = 3.0f;
    public float godMode = 3.0f;

    private float max = 1000.0f;
    private float curr;

    Stopwatch sw = new Stopwatch();
    Regex regex = new Regex("-");

    static AudioSource mainTheme;

    private void Start()
    {
        mainTheme = GetComponent<AudioSource>();
        //mainTheme.Play();
    }

    private void Update()
    {
        if (!mainTheme.isPlaying)
            mainTheme.Play();
        

        //here we update UI
        ScoreTXT.GetComponent<Text>().text = "Score: " + score.ToString();
        CoordinatesTXT.GetComponent<Text>().text = "X: " + player.gameObject.transform.position.x.ToString("0.0") + " Y: " + player.gameObject.transform.position.y.ToString("0.0");
        AngleTXT.GetComponent<Text>().text = "Angle: " + regex.Replace(player.CurrentAngle.ToString("0"), "") + "°";
        SpeedTXT.GetComponent<Text>().text = "Speed: " + player.CurrentSpeed.ToString("0.0") + " km/s";
        LazerTXT.GetComponent<Text>().text = "Lazers: " + lazerCount.ToString();

        //we link progress bar filling with time
        curr += (float)sw.Elapsed.TotalSeconds;

        //when we done filling progress bar we call new lazer and reset progress bar
        if (curr > max)
        {
            LazerAdd();
            curr = 0.0f;
        }

        Fill(curr);
    }

    //returns bool value for Player class, when we want to shoot Lazer
    public bool LazerCount()
    {
        if (lazerCount > 0)
        {
            lazerCount--;
            sw.Start(); //here we start timer (filling progress bar)

            return true;
        }
        else
            return false;
    }

    private void LazerAdd()
    {
        if (lazerCount < 10)
        {
            lazerCount++;
            sw.Restart();
        }

        //reset timer
        if (lazerCount == 10)
            sw.Reset();
    }

    //fill progress bar
    private void Fill(float current)
    {
        float fillAmount = current / max;
        filler.fillAmount = fillAmount;
    }

    //count the score based on asteroid's size
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosionGR.transform.position = asteroid.transform.position;
        explosionGR.Play();

        score += (int)((asteroid.maxSize - asteroid.size) * 100.0f);
    }

    //count the score based on ufo's speed
    public void UfoDestroyed(Ufo ufo)
    {
        explosionGR.transform.position = ufo.transform.position;
        explosionGR.Play();

        score += (int)((ufo.maxSpeed - ufo.speed) * 100.0f);
    }

    public void Respawn()
    {
        message.SetActive(false);
        result.SetActive(false);

        playerIsDead = false;

        score = 0;
        lazerCount = 10;

        //reset position to 0
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions"); //makes player invulnerable to prevent collisions with other gameobjects*
        player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollisions), godMode); // *after game start (for 3 sec)
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void GameOver()
    {
        explosionRD.transform.position = player.transform.position;
        explosionRD.Play();

        result.GetComponent<Text>().text = score.ToString();
        result.SetActive(true);
        message.SetActive(true);

        playerIsDead = true;
    }
}
