using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject[] obstaclePatterns = null;
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] float decreaseTime = 0.05f;
    [SerializeField] float minTime = 0.65f;

    private int patternNumber;

    // Start is called before the first frame update
    void Start()
    {
        patternNumber = Random.Range(0, obstaclePatterns.Length);
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        yield return new WaitForSeconds(spawnDelay + 1);

        while (true)
        {
            int randomInt = Random.Range(0, obstaclePatterns.Length);
            if (randomInt != patternNumber)
            {
                patternNumber = randomInt;

                GameObject obstaclePattern = Instantiate(obstaclePatterns[patternNumber], transform.position, Quaternion.identity);
                if (spawnDelay > minTime)
                {
                    spawnDelay -= decreaseTime;
                }
                yield return new WaitForSeconds(spawnDelay);
                Destroy(obstaclePattern);
            }
            else
            {
                randomInt = Random.Range(0, obstaclePatterns.Length);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
