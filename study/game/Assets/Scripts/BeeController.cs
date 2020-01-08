using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeController : MonoBehaviour
{
    public Transform target;

    private GameController _gameController;
    private NavMeshAgent _navMeshAgent;
    private Animator _beeAnimator;
    private AudioSource _audioSource;
    private float _distanceToTarget = Mathf.Infinity;
    private Boolean _isSuccessful;
    private float initTime; 

    private static readonly int Attack = Animator.StringToHash("attack");
    private static readonly int Move = Animator.StringToHash("move");
    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Die = Animator.StringToHash("die");


    void Start()
    {
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        if (!_gameController.ballonModeEnabled)
        {
            _beeAnimator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _audioSource = GetComponent<AudioSource>();
            _beeAnimator.SetBool(Idle, true);
        }
        else
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        initTime = Time.timeSinceLevelLoad; 
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
            else if (_distanceToTarget <= _navMeshAgent.stoppingDistance)
            {
                AttackTarget();
            }
        }
    }

    private void ChaseTarget()
    {
        if (!_gameController.ballonModeEnabled)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            _beeAnimator.SetBool(Idle, false);
            _beeAnimator.SetBool(Move, true);
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
            _beeAnimator.SetBool(Move, false);
            _beeAnimator.SetTrigger(Attack);
            _beeAnimator.SetBool(Idle, true);
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
    
    }

    public void OnTriggerEnter(Collider other)
    {
        _isSuccessful = true;
        _gameController.BeeScores();
        _navMeshAgent.enabled = false;
        _gameController.objectTimeAlive.Add((Time.timeSinceLevelLoad - initTime));
    }



    public void GetAway()
    {
        if (!_gameController.ballonModeEnabled)
        {
            _gameController.playerHit();
            _audioSource.Stop();
            Destroy(gameObject);
        }
        else
        {
            _gameController.playerHit();
            Destroy(gameObject);
        }

        _gameController.objectTimeAlive.Add((Time.timeSinceLevelLoad - initTime));
    }
}
