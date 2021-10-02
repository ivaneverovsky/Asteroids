using UnityEngine;

public class Ufo : MonoBehaviour
{
    public Sprite[] sprites;
    public AlienWeapon weaponPrefab;

    public float minSpeed = 1.0f;
    public float maxSpeed = 2.55f;
    public float speed = 1.5f;
    public float size = 1.0f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Vector2 movement;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.mass = size;
        _spriteRenderer.sprite = sprites[0];
        InvokeRepeating(nameof(SpawnShoot), 8.0f, 11.0f);
    }

    private void Update()
    {
        Vector2 direction = FindObjectOfType<AliensSpawner>().player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
    }
    private void FixedUpdate()
    {
        SetTrajectory(movement);
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Lazer")
        {
            SoundManager.PlaySound("ufo_killed");
            FindObjectOfType<GameManager>().UfoDestroyed(this);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            SoundManager.PlaySound("ufo_killed");
            Destroy(gameObject);
        }
    }
    private void SpawnShoot()
    {
        _spriteRenderer.sprite = sprites[1];

        SoundManager.PlaySound("ufo_shoot");

        AlienWeapon weapon = Instantiate(weaponPrefab, transform.position, transform.rotation);
        weapon.Project(transform.up);
    }
}
