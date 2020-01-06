using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class shootController : MonoBehaviour
{

    private GameController _gameController;
    [SerializeField] private TMP_Text scoreCounter; 

    private void Start()
    {
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Bee")
                {
                    Debug.DrawRay(transform.position, hit.collider.gameObject.transform.position);
                    Destroy(hit.collider.gameObject);
                    _gameController.PlayerScores();
                    scoreCounter.SetText(_gameController.playerScore.ToString());
                }
            }
        }   

            
        
    }
}
