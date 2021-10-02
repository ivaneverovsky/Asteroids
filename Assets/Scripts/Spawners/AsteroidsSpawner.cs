using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float spawnRate = 5.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15f;

    public float trajectory = 15.0f;

    private void Start()
    {
        //repeat method several times
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            //set spawn on random point inside the circle
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;

            //set angle for spawner
            float variance = Random.Range(-trajectory, trajectory);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            //set asteroid and its size
            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

            //set asteroid move to player field
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
