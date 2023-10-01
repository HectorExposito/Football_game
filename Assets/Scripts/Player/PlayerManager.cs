using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool team;//true-> blue    false->orange
    [SerializeField] PlayerMovement playerMovement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerTurn(bool turn)
    {
        if (turn == team)
        {
            playerMovement.enabled = true;
        }
        else
        {
            playerMovement.enabled = false;
        }
    }
}
