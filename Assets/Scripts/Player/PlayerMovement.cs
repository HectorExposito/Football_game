using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;
    Camera camera;

    [SerializeField] private Vector2 minShootPower;
    [SerializeField] private Vector2 maxShootPower;
    [SerializeField] private float shootPower = 10f;
    [SerializeField] private Rigidbody2D rigidbody2d;

    [SerializeField] private LineRenderer trajectoryLine;

    

    private bool thisPlayerSelected;
    bool isMoving;

    MatchManager matchManager;

    private void Awake()
    {
        thisPlayerSelected = false;
        camera = Camera.main;
        matchManager = FindObjectOfType<MatchManager>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (matchManager.matchStarted)
        {
            DragAndShoot();
            if (isMoving)
            {
                if (rigidbody2d.velocity.magnitude == 0)
                {
                    matchManager.ChangeTurn();
                    isMoving = false;
                }
            }
        }
        
    }

    internal void StopPlayer()
    {
        rigidbody2d.velocity = Vector2.zero;
    }

    internal void DisablePlayerMovement()
    {
        if (!thisPlayerSelected)
        {
            enabled = false;
        }
    }

    private void DragAndShoot()
    {
        if (thisPlayerSelected)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 currentPoint = camera.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 0;
                Vector3 arrowVector=transform.position-currentPoint;
                //Debug.Log(transform.position+" "+currentPoint*-1);
                Vector3 forceAtTheMoment = new Vector2(Mathf.Clamp(transform.position.x - currentPoint.x, minShootPower.x, maxShootPower.x),
                                        Mathf.Clamp(transform.position.y - currentPoint.y, minShootPower.y, maxShootPower.y));
                //Debug.Log(forceAtTheMoment);
                RenderLine(transform.position, transform.right*forceAtTheMoment.magnitude);

                float angle = Mathf.Atan2(forceAtTheMoment.y, forceAtTheMoment.x) * Mathf.Rad2Deg;
                //Debug.Log("Angulo "+angle);
                transform.rotation = Quaternion.Euler(0,0,angle); //Quaternion.AngleAxis(angle, Vector3.forward);
                //transform.LookAt(new Vector3(0,0,angle));
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMoving = true;
                endPoint = camera.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minShootPower.x, maxShootPower.x),
                                        Mathf.Clamp(startPoint.y - endPoint.y, minShootPower.y, maxShootPower.y));


                rigidbody2d.AddForce(force * shootPower, ForceMode2D.Impulse);
                EndLine();
                thisPlayerSelected = false;
                StartCoroutine(PlayerStopped());
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
                        matchManager.DisablePlayerMovement();
                    }
                }
            }
        }
    }

    private IEnumerator PlayerStopped()
    {
        yield return new WaitForFixedUpdate();
    }


    private void RenderLine(Vector3 startPoint,Vector3 endPoint)
    {
        trajectoryLine.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint+startPoint;

        trajectoryLine.SetPositions(points);
    }

    public void EndLine()
    {
        trajectoryLine.positionCount =0;
    }
}
