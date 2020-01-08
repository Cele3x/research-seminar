using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO; 
public class GameController : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    public GameObject objectToSpawn;

    public bool ballonModeEnabled = false;

    public float objectSpeed = 10f;

    [HideInInspector] public int playerScore = 0;
    [HideInInspector] public int beeScore = 0;
    [HideInInspector] public int playerHitCount = 0;
    [HideInInspector] public string levelString;


    [SerializeField] private TMP_Text playerScoreText; 
    [SerializeField] private TMP_Text playerHitCountField;
    [SerializeField] private TMP_Text timeLeftField;
    [SerializeField] private int maxLevelTime;
    [SerializeField] private GameObject gameoverOverlay; 

    [SerializeField] private  Transform[] _spawnPoints;
    [SerializeField] GameObject beePrefab;
    [SerializeField] GameObject ballonPrefab; 
 


    private int _currentSpawnIndex = 0;

    private int timeLeftinLevel;

    private float timeBetweenSpawns = 4.0f;
    [SerializeField]private float SpawnsDecayFactor = 0.9f; 
    private void Awake()
    {

        timeLeftField.SetText(maxLevelTime.ToString());

        //Start level parsing 
        levelParser();

    }
    void Start()
    {
        if(ballonModeEnabled)
        {
            objectToSpawn = ballonPrefab;
            playerScoreText.SetText("Ballons killed:");
            beeSpawner();

        }
        else
        {
            objectToSpawn = beePrefab;
            playerScoreText.SetText("Bees refreshed:");
            beeSpawner();
        }


        //Starting Beespawn
        StartCoroutine("beeSpawnerCoHo");

        //Invoking difficulty adjustment function 

        InvokeRepeating("adjustDifficulty", 3.0f, 3.0f);
        

    }

    void adjustDifficulty()
    {
        Debug.Log("Adjusting dif now");
        //Starts at 4sec intervall between spawns, ends at 0.92sec with a level runtime of 120sec
        if (timeBetweenSpawns > 1.0f)
        {
            timeBetweenSpawns = timeBetweenSpawns * SpawnsDecayFactor;
        }
        Debug.Log(timeBetweenSpawns);

    }
    IEnumerator beeSpawnerCoHo()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        Debug.Log("Spawning bee");
        beeSpawner();
        StartCoroutine("beeSpawnerCoHo");

    }
   
    void levelParser()
    {
        string path = "Assets/Levelfiles/level_A.txt";
        StreamReader reader = new StreamReader(path);
        levelString = reader.ReadToEnd();
        reader.Close();

    }

    public void beeSpawner()
    {
        spawnBee(int.Parse(levelString[_currentSpawnIndex].ToString()));

    }

    public void spawnBee(int spawnIndex)
    {
        if (!ballonModeEnabled)
        {


            GameObject bee = Instantiate(objectToSpawn, new Vector3(
                _spawnPoints[spawnIndex].position.x,
                _spawnPoints[spawnIndex].position.y,
                _spawnPoints[spawnIndex].position.z),
                Quaternion.identity);
            bee.GetComponent<BeeController>().target = player.transform;
        }

        else
        {
            GameObject bee = Instantiate(objectToSpawn, new Vector3(
               _spawnPoints[spawnIndex].position.x,
               _spawnPoints[spawnIndex].position.y,
               _spawnPoints[spawnIndex].position.z + 0.05f),
               Quaternion.identity);
            bee.GetComponent<BeeController>().target = player.transform;

        }

        

        if(_currentSpawnIndex >= _spawnPoints.Length)
        {
            _currentSpawnIndex = 0; 
        }
        else
        {
            _currentSpawnIndex = _currentSpawnIndex + 1;
        }

    }

    public void BeeScores()
    {
        beeScore += 1;
        //spawnBee(int.Parse(levelString[_currentSpawnIndex].ToString()));
    }

    public void PlayerScores()
    {
        playerScore += 1;
        //spawnBee(int.Parse(levelString[_currentSpawnIndex].ToString()));
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

        /*   if(Time.timeSinceLevelLoad >20 && timeBetweenSpawns == 4.0f)
           {
               Debug.Log("Adjusting Difficulty");
               timeBetweenSpawns = 3.5f; 
           }
       */

      

     
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
