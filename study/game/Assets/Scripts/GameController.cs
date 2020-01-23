using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO; 
public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject objectToSpawn;

    public bool ballonModeEnabled = false;

    public float objectSpeed = 10f;

    [HideInInspector] public int playerScore = 0;
    [HideInInspector] public int beeScore = 0;
    [HideInInspector] public int playerHitCount = 0;
    [HideInInspector] public string levelString;
    public List<float> objectTimeAlive; 

    [SerializeField] public CSVLogger logger;
    [SerializeField] private TMP_Text playerScoreText; 
    [SerializeField] private TMP_Text playerHitCountField;
    [SerializeField] private TMP_Text timeLeftField;
    [SerializeField] private int maxLevelTime;
    [SerializeField] private GameObject gameoverOverlay; 

    [SerializeField] private  Transform[] _spawnPoints;
    [SerializeField] GameObject beePrefab;
    [SerializeField] GameObject ballonPrefab;

    [SerializeField] float offsetSpawn; 

    private int _beeId = 0;

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
        
        //Starts at 4sec intervall between spawns, ends at 0.92sec with a level runtime of 120sec
        if (timeBetweenSpawns > 1.6f)
        {
            timeBetweenSpawns = timeBetweenSpawns * SpawnsDecayFactor;
            Debug.Log(timeBetweenSpawns);
        }

    }
    IEnumerator beeSpawnerCoHo()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        beeSpawner();
        StartCoroutine("beeSpawnerCoHo");

    }
   
    void levelParser()
    {
        string path = "Assets/Levelfiles/level_A.txt";
        StreamReader reader = new StreamReader(path);
        levelString = reader.ReadToEnd();
        print(levelString);
        reader.Close();


    }

    public void beeSpawner()
    {
        spawnBee(int.Parse(levelString[_currentSpawnIndex].ToString()));

    }

    public void spawnBee(int spawnIndex)
    {
        spawnIndex = spawnIndex - 1; 
        if (!ballonModeEnabled)
        {
       

            GameObject bee = Instantiate(objectToSpawn, new Vector3(
                _spawnPoints[spawnIndex].position.x,
                _spawnPoints[spawnIndex].position.y + offsetSpawn,
                _spawnPoints[spawnIndex].position.z),
                Quaternion.identity);
            bee.GetComponent<BeeController>().target = player.transform;
            bee.GetComponent<BeeController>().id = _beeId;
        }

        else
        {
            GameObject bee = Instantiate(objectToSpawn, new Vector3(
               _spawnPoints[spawnIndex].position.x,
               _spawnPoints[spawnIndex].position.y+offsetSpawn,
               _spawnPoints[spawnIndex].position.z + 0.05f),
               Quaternion.identity);
            bee.GetComponent<BeeController>().target = player.transform;
            bee.GetComponent<BeeController>().id = _beeId;
        }

        _beeId++;

        if(_currentSpawnIndex >= _spawnPoints.Length)
        {
            _currentSpawnIndex = 0; 
        }
        else
        {
            _currentSpawnIndex = _currentSpawnIndex + 1;
        }

        foreach (float item in objectTimeAlive)
        {
// Debug.Log(item);
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

    public void OnApplicationQuit()
    {
        logger.SaveToFile();
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
