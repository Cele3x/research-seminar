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
    [SerializeField] GameObject flash;

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
            //Debug.Log(timeBetweenSpawns);
        }

    }
    IEnumerator beeSpawnerCoHo()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        Debug.Log(_beeId);
        if (_beeId < 100)
        {
            beeSpawner();
            StartCoroutine("beeSpawnerCoHo");
        }
        else
        {
            Debug.Log("Ending Game.."); 
        }
      

    }
   
    void levelParser()
    {

        levelString = "32144212312341231241231234441231234124124141234123434321423123432423413412344322341234123412344312123433213213234123423412341414321234144412321313223213412341234432141234123412331234123414242131234323143124123412341234321442123123412312412312344412312341241241412341234343214231234324234134123443223412341234123443121234332132132341234234123414143212341444123213132232134123412344321412341234123312341234142421312343231431241234123412343214421231234123124123123444123123412412414123412343432142312343242341341234432234123412341234431212343321321323412342341234141432123414441232131322321341234123443214123412341233123412341424213123432314312412341234123432144212312341231241231234441231234124124141234123434321423123432423413412344322341234123412344312123433213213234123423412341414321234144412321313223213412341234432141234123412331234123414242131234323143124123412341234";



    }

    public void flashNow()
    {

        flash.SetActive(true);
        StartCoroutine("deactivateFlash");

    }

    public IEnumerator deactivateFlash()
    {
        yield return new WaitForSeconds(0.2f);
        flash.SetActive(false);
    }

    public void beeSpawner()
    {
        spawnBee(int.Parse(levelString[_currentSpawnIndex].ToString()));
        Debug.Log(int.Parse(levelString[_currentSpawnIndex].ToString()));
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
