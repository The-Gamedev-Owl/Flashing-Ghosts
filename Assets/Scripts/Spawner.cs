using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("GameObject to spawn")]
    public GameObject toSpawn;

    [Tooltip("Initial time before first spawn (in s)")]
    public float spawnWait;

    [Tooltip("Time between each spawn (in s)")]
    public float spawnInterval;

    private float timer;

    public void Start()
    {
        timer = spawnWait;
        spawnInterval *= 1000f;
        spawnWait *= 1000f;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Instantiate(toSpawn, transform.position, Quaternion.identity);
            timer = spawnInterval;
        }
    }
}
