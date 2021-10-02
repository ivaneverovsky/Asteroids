using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;

    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.0f;
    public float speed = 8.0f;
    public float maxLifeTime = 30.0f;

    private SpriteRenderer _spriteRender;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //set random sprite for asteroid
        _spriteRender.sprite = sprites[Random.Range(0, sprites.Length)];

        //set random orientation for sprite (makes it more unique)
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * size;

        //set mass for asteroid
        _rigidbody.mass = size;
    }

    private void Update()
    {
        FindObjectOfType<PositionChecker>().CheckPosition(_rigidbody);
    }

    public void SetTrajectory(Vector2 direction)
    {
        //set direction and speed for asteroid
        _rigidbody.AddForce(direction * speed);
        //Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            SoundManager.PlaySound("asteroidHit");
            if (size >= minSize)
                CreateSplit();

            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Lazer")
        {
            SoundManager.PlaySound("asteroidHit");
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "AlienWeapon")
        {
            SoundManager.PlaySound("asteroidHit");
            if (size >= minSize)
                CreateSplit();

            Destroy(gameObject);
        }
    }

    //create new Asteroid
    private void CreateSplit()
    {
        for (int i = 0; i < 2; i++)
        {
            Vector2 position = transform.position;
            position += Random.insideUnitCircle * 0.5f;

            Asteroid half = Instantiate(this, position, transform.rotation);
            half.size = size * 0.5f;
            half.SetTrajectory(Random.insideUnitCircle.normalized * speed * 0.5f);
        }
    }
}