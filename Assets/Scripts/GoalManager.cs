using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private bool isLeftGoal;
    [SerializeField] private MatchManager matchManager; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (isLeftGoal)
            {
                matchManager.SetScore(false);
            }
            else
            {
                matchManager.SetScore(true);
            }
            collision.gameObject.GetComponent<BallManager>().GoToCenter();
            matchManager.ResetPlayersPosition();
        }
    }
}
