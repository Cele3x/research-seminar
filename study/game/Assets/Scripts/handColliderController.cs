using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handColliderController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bee")
        {

            Destroy(other.gameObject);
        }
    }
}
