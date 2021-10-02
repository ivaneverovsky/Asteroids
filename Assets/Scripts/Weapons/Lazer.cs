using UnityEngine;

public class Lazer : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private float speed = 100000.0f;
    private float lifeTime = 1.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
        Destroy(gameObject, lifeTime);
    }
}
