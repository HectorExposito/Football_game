using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MatchManager : MonoBehaviour
{
    bool teamTurn;//true->blue   false->orange
    [SerializeField]GameObject[] players;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject middleBorder;

    [SerializeField] private TMP_Text turnText;
    [SerializeField] private TMP_Text blueScore;
    [SerializeField] private TMP_Text orangeScore;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject blueTeamReadyPanel;
    [SerializeField] private GameObject orangeTeamReadyPanel;

    int bluePoints;
    int orangePoints;

    public bool matchStarted;

    internal void ResetPlayersPosition()
    {
        foreach (GameObject p in players)
        {
            p.GetComponent<PlayerMovement>().StopPlayer();
            p.GetComponent<PlacePlayers>().ResetPosition();
        }
    }

    private void Start()
    {
        matchStarted = false;
        bluePoints = 0;
        orangePoints = 0;
        int turn=Random.Range(0,2);
        turn = 1;
        if (turn == 0)
        {
            teamTurn = true;
        }
        else
        {
            teamTurn = false;
        }
        ChangeTurn();
    }

    public void ChangeTurn()
    {
        teamTurn = !teamTurn;
        foreach (GameObject p in players)
        {
            p.GetComponent<PlayerManager>().PlayerTurn(teamTurn);
        }
        Debug.Log("turno "+teamTurn);
        if (teamTurn)
        {
            turnText.text = "TURNO EQUIPO AZUL";
            turnText.color = Color.blue;
        }
        else
        {
            turnText.text = "TURNO EQUIPO NARANJA";
            turnText.color =new Color(255,66,0);
        }
    }

    internal void DisablePlayerMovement()
    {
        foreach (GameObject p in players)
        {
            p.GetComponent<PlayerMovement>().DisablePlayerMovement();
        }
    }

    public void SetScore(bool blueGoal)
    {
        if (blueGoal)
        {
            bluePoints++;
        }
        else
        {
            orangePoints++;
        }
        blueScore.text = bluePoints.ToString();
        orangeScore.text = orangePoints.ToString();
    }

    public void SavePlayersPosition(bool isBlue)
    {
        if (isBlue)
        {
            for (int i = 0; i < 5; i++)
            {
                players[i].GetComponent<PlacePlayers>().SavePosition();
                blueTeamReadyPanel.SetActive(false);
                ChangeTurn();
                orangeTeamReadyPanel.SetActive(true);
            }
        }
        else
        {
            for (int i = 5; i < players.Length; i++)
            {
                players[i].GetComponent<PlacePlayers>().SavePosition();
                orangeTeamReadyPanel.SetActive(false);
                scorePanel.SetActive(true);
                turnText.gameObject.SetActive(true);
                ball.SetActive(true);
                matchStarted = true;
                middleBorder.SetActive(false);
                ChangeTurn();
            }

        }
    }
}
