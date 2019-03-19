using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform enemySpawnPoint;

    public Text waveCountdownText;

    //Used for all subsequent waves sent after wave one
    [SerializeField]
    float timeBetweenWaves;

    //Used to determine when the first wave is sent
    float countDown = 2f;


    int waveIndex = 0;

    void Update() 
    {
        if (countDown <= 0)
        {
            StartCoroutine(SpawnWave());

            countDown = timeBetweenWaves;

            countDown = Mathf.Clamp(countDown, 0, Mathf.Infinity);

            waveCountdownText.text = string.Format("{0:00.00}", countDown);
        }

        countDown -= Time.deltaTime;
        //This is just placeholder, Once art has been made up this will be replaced
        waveCountdownText.text = "Next Wave Spawns In: " + Mathf.Round(countDown).ToString();
    }

    IEnumerator SpawnWave()
    {

        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            
            //After spwaning an enemy wait for a half of a second to 
            //Spawn the next one
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
    }

}
