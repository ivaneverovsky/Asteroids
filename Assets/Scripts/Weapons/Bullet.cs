using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private float speed = 400.0f;
    private float lifeTime = 1.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FindObjectOfType<PositionChecker>().CheckPosition(_rigidbody);
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
