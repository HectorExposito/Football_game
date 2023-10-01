using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlayers : MonoBehaviour
{
    MatchManager matchManager;
    Vector3 startPoint;
    Vector3 endPoint;
    Camera camera;

    private bool thisPlayerSelected;
    bool isMoving;

    private Vector3 originalPosition;

    void Awake()
    {
        thisPlayerSelected = false;
        camera = Camera.main;
        matchManager = FindObjectOfType<MatchManager>();
    }


    void Update()
    {
        if (!matchManager.matchStarted)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        if (thisPlayerSelected)
        {
            if (Input.GetMouseButtonUp(0))
            {
                thisPlayerSelected = false;
            }

            if (Input.GetMouseButton(0))
            {
                //this.GetComponent<Transform>().position = Input.mousePosition;

                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                this.GetComponent<Transform>().position = mousePosition;

                //Forbids the player to place the player outside its part of the field
                if (GetComponent<PlayerManager>().team)
                {
                    //checks the middle of the field
                    if (this.GetComponent<Transform>().position.x > -0.6)
                    {
                        this.GetComponent<Transform>().position = new Vector3(-0.6f, GetComponent<Transform>().position.y, 0);
                    }
                    //checks the left border of the field
                    if (this.GetComponent<Transform>().position.x < -7.6)
                    {
                        this.GetComponent<Transform>().position = new Vector3(-7.6f, GetComponent<Transform>().position.y, 0);
                    }
                    //checks the bottom border of the field
                    if (this.GetComponent<Transform>().position.y < -4)
                    {
                        this.GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, -4, 0);
                    }
                    //checks the top border of the field
                    if (this.GetComponent<Transform>().position.y > 4)
                    {
                        this.GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, 4, 0);
                    }
                }
                else
                {
                    if (this.GetComponent<Transform>().position.x < 0.6)
                    {
                        this.GetComponent<Transform>().position = new Vector3(0.6f, GetComponent<Transform>().position.y, 0);
                    }
                    if (this.GetComponent<Transform>().position.x > 7.6)
                    {
                        this.GetComponent<Transform>().position = new Vector3(7.6f, GetComponent<Transform>().position.y, 0);
                    }
                    if (this.GetComponent<Transform>().position.y < -4)
                    {
                        this.GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, -4, 0);
                    }
                    if (this.GetComponent<Transform>().position.y > 4)
                    {
                        this.GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, 4, 0);
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (hit.collider != null && hit.collider.transform == this.transform)
                {
                    if (hit.collider.tag == "Player")
                    {
                        thisPlayerSelected = true;
                        startPoint = camera.ScreenToWorldPoint(Input.mousePosition);
                        startPoint.z = 15;
                        DisablePlayerMovement();
                    }
                }
            }
        }

    }

    private void DisablePlayerMovement()
    {
        if (!thisPlayerSelected)
        {
            enabled = false;
        }
    }

    internal void SavePosition()
    {
        originalPosition = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaa");
    }
}
