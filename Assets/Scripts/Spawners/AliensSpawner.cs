using UnityEngine;

public class AliensSpawner : MonoBehaviour
{
    public Ufo ufoPrefab;
    public Player player;

    private float spawnRate = 5.0f;
    private int spawnAmount = 1;

    private float spawnDistance = 15.0f;

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

            //set ufo
            Ufo ufo = Instantiate(ufoPrefab, spawnPoint, transform.rotation);

            spawnDirection = player.transform.position - transform.position;
            ufo.speed = Random.Range(ufo.minSpeed, ufo.maxSpeed);

            //set ufo move to player
            ufo.SetTrajectory(transform.rotation * -spawnDirection);
        }
    }
}
