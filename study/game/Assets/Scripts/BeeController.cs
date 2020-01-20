using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeController : MonoBehaviour
{
    public Transform target;
    public int id;

    private GameController _gameController;
    private NavMeshAgent _navMeshAgent;
    private Animator _beeAnimator;
    private AudioSource _audioSource;
    private float _distanceToTarget = Mathf.Infinity;
    private Boolean _isSuccessful;
    private float initTime; 

    private CSVLogger _logger;

    private static readonly int Attack = Animator.StringToHash("attack");
    private static readonly int Move = Animator.StringToHash("move");
    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Die = Animator.StringToHash("die");


    void Start()
    {
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        _logger = GameObject.FindWithTag("GameController").GetComponent<CSVLogger>();

        target = GameObject.FindWithTag("Body").GetComponent<Transform>();
        if (!_gameController.ballonModeEnabled)
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();

        }
        else
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        initTime = Time.timeSinceLevelLoad; 
        _logger.BeeSpawn(id);
    }
    
    void Update()
    {
        if (_isSuccessful)
        {
            GetAway();
        }
        else
        {
            _distanceToTarget = Vector3.Distance(target.position, transform.position);
            if (_distanceToTarget >= _navMeshAgent.stoppingDistance)
            {
                ChaseTarget();
            } 
            //else if (_distanceToTarget <= _navMeshAgent.stoppingDistance)
            else if (_distanceToTarget <= 3)
            {
                AttackTarget();
            }
        }

        Debug.Log(this.transform.position.y);
    }

    private void ChaseTarget()
    {
        if (!_gameController.ballonModeEnabled)
        {
     

            _navMeshAgent.SetDestination(target.position);
            _navMeshAgent.speed = _gameController.objectSpeed;
        }
        else
        {
            _navMeshAgent.SetDestination(target.position);
            _navMeshAgent.speed = _gameController.objectSpeed;
        }
    }

    private void  AttackTarget()
    {
        if (!_gameController.ballonModeEnabled)
        {
  
        }
        else
        {
            
        }
    }

    public void CollisionFromChild(Collider other)
    {
        if (!_gameController.ballonModeEnabled)
        {

            if (_isSuccessful) return;
            _isSuccessful = true;
            _gameController.BeeScores();
            _navMeshAgent.enabled = false;
        }
        _logger.BeeDeath(id);
    }


    public void OnCollisionEnter(Collision collision)
    {
        if(collision.other.gameObject.tag == "Hand")
        {
            _logger.BeeDeath(id);
            Destroy(this.gameObject);
        }

        if (collision.other.gameObject.tag == "Body")
        {
            _logger.BeeHit(id);
            Destroy(this.gameObject.GetComponent<Rigidbody>());
            Destroy(this.gameObject, 0.5f);
            GetAway();

        }


    }

    public void OnTriggerEnter(Collider collision)
    {
  
     

    }







    public void GetAway()
    {
        if (!_gameController.ballonModeEnabled)
        {
            
            _gameController.playerHit();

         
            transform.position += Vector3.up * Time.deltaTime;
            Destroy(gameObject, 8.0f);

            //Destroy(gameObject);
        }
        else
        {
            _gameController.playerHit();
            Destroy(this.gameObject);
        }

 
        
    }
}
