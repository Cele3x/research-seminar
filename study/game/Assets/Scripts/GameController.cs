using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject beePrefab;
    public int playerScore = 0;
    public int beeScore = 0;
    
    private readonly Vector3[] _spawnPoints = new [] { new Vector3(-10, 0, 0), new Vector3(-10, 0, -40), 
                                                       new Vector3(10, 0, -20), new Vector3(-30, 0, -20)};

    private int _currentSpawnIndex = 0;
    
    void Start()
    {
        GameObject bee = Instantiate(beePrefab, _spawnPoints[_currentSpawnIndex++], Quaternion.identity);
        bee.GetComponent<BeeController>().target = player.transform;
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
}
