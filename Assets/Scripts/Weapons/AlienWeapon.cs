using UnityEngine;

public class AlienWeapon : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private float speed = 100.0f;
    private float lifeTime = 1.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        direction = FindObjectOfType<AliensSpawner>().player.transform.position - transform.position;
        _rigidbody.AddForce(direction * speed);
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
