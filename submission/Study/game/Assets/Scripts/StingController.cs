using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingController : MonoBehaviour
{

    public GameObject parentBee;
    private BeeController _beeController;

    public void Start()
    {
        _beeController = parentBee.GetComponent<BeeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _beeController.CollisionFromChild(other);
    }
}
