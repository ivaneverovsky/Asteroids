using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] skins;

    public ParticleSystem explosionPR;

    public Bullet bulletPrefab;
    public Lazer lazerPrefab;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    public float CurrentSpeed;
    public float CurrentAngle;

    private bool _thrusting;
    public float _turnDirection;

    public float thrustSpeed = 1.0f;
    public float turnSpeed = 2.0f;

    private bool boostPlayed = false;

    static AudioSource _booster;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _booster = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //check if player inside game field
        FindObjectOfType<PositionChecker>().CheckPosition(_rigidbody);

        //set parameters
        CurrentAngle = CheckAngle();
        CurrentSpeed = _rigidbody.velocity.magnitude;

        if (FindObjectOfType<Menu>().GameStart & !FindObjectOfType<Menu>().GameIsPaused)
        {
            //controllers
            _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                _turnDirection = 1.0f;
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                _turnDirection = -1.0f;
            else
                _turnDirection = 0.0f;

            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();

            if (Input.GetKeyDown(KeyCode.S))
                Hyperspace();

            if (Input.GetKeyDown(KeyCode.E))
                if (FindObjectOfType<GameManager>().LazerCount())
                    ShootLazer();

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                _spriteRenderer.sprite = skins[1];
                thrustSpeed = 3.0f;

                if (!boostPlayed)
                {
                    _booster.Play();
                    boostPlayed = true;
                }
            }
            else
            {
                _spriteRenderer.sprite = skins[0];
                thrustSpeed = 1.0f;
                _booster.Stop();
                boostPlayed = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_thrusting)
            _rigidbody.AddForce(transform.up * thrustSpeed);

        if (_turnDirection != 0.0f)
            _rigidbody.AddTorque(_turnDirection * turnSpeed);
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction);
    }

    private void Shoot()
    {
        SoundManager.PlaySound("bulletshot");

        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }

    private void ShootLazer()
    {
        SoundManager.PlaySound("lazer_shoot");

        Lazer lazer = Instantiate(lazerPrefab, transform.position, transform.rotation);
        explosionPR.transform.position = lazer.transform.position;

        lazer.Project(transform.up);
        explosionPR.Play();
    }

    private void Hyperspace()
    {
        SoundManager.PlaySound("hyperspace");

        //set random position for player on game field
        _rigidbody.position = new Vector2(Random.Range(-9.4f, 9.4f), Random.Range(-5.5f, 5.5f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            //play sound "ship is down"
            SoundManager.PlaySound("shipdead");

            //stop movement
            _rigidbody.velocity = Vector3.zero;

            //stop rotation
            _rigidbody.angularVelocity = 0.0f;

            //turn off game object
            gameObject.SetActive(false);

            FindObjectOfType<GameManager>().GameOver();
        }
        else if (collision.gameObject.tag == "Ufo" || collision.gameObject.tag == "AlienWeapon")
        {
            SoundManager.PlaySound("ufo_kills_player");

            //stop movement
            _rigidbody.velocity = Vector3.zero;

            //stop rotation
            _rigidbody.angularVelocity = 0.0f;

            //turn off game object
            gameObject.SetActive(false);

            FindObjectOfType<GameManager>().GameOver();
        }
    }

    private float CheckAngle()
    {
        if (_rigidbody.rotation > 360.0f || _rigidbody.rotation < -360.0f)
            _rigidbody.rotation = 0.0f; //reset rotation to zero

        return _rigidbody.rotation;
    }
}
