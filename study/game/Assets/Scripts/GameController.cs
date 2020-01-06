using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject beePrefab;
    public int playerScore = 0;
    public int beeScore = 0;
    public int playerHitCount = 0;
    public float beeSpeed = 10f; 


    [SerializeField] private TMP_Text playerHitCountField;
    [SerializeField] private TMP_Text timeLeftField;
    [SerializeField] private int maxLevelTime;
    [SerializeField] private GameObject gameoverOverlay; 

    private readonly Vector3[] _spawnPoints = new [] { new Vector3(-10, 0, 0), new Vector3(-10, 0, -40), 
                                                       new Vector3(10, 0, -20), new Vector3(-30, 0, -20)};

    private int _currentSpawnIndex = 0;

    private int timeLeftinLevel; 
    
    void Start()
    {
        GameObject bee = Instantiate(beePrefab, _spawnPoints[_currentSpawnIndex++], Quaternion.identity);
        bee.GetComponent<BeeController>().target = player.transform;

        timeLeftField.SetText(maxLevelTime.ToString());
    }

    public void BeeScores()
    {
        beeScore += 1;
        GameObject bee = Instantiate(beePrefab, _spawnPoints[_currentSpawnIndex++ % _spawnPoints.Length], Quaternion.identity);
        bee.GetComponent<BeeController>().target = player.transform;
    }

    public void PlayerScores()
    {
        playerScore += 1;
        GameObject bee = Instantiate(beePrefab, _spawnPoints[_currentSpawnIndex++ % _spawnPoints.Length], Quaternion.identity);
        bee.GetComponent<BeeController>().target = player.transform;
    }

    public void playerHit()
    {
        playerHitCount = playerHitCount + 1;
        playerHitCountField.SetText(playerHitCount.ToString());
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad < maxLevelTime)
        {
            updateTimeField();
        }
     
    }


    public void updateTimeField()
    {
        timeLeftinLevel = maxLevelTime - (Mathf.RoundToInt(Time.timeSinceLevelLoad));
        if (timeLeftinLevel == 0)
        {

            gameoverOverlay.SetActive(true);
            Time.timeScale = 0;
            GameObject [] bees = GameObject.FindGameObjectsWithTag("Bee");
            foreach(GameObject bee in bees )
            {
                GameObject.Destroy(bee);
            }
            
        }

        else
        {

            timeLeftField.SetText(timeLeftinLevel.ToString());

        }
    }
}
