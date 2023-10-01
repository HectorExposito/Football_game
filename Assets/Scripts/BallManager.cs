using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GoToCenter();
    }

    public void GoToCenter()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        transform.localPosition = Vector3.zero;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (collision.collider.offset.x==0)
            {
                if(collision.collider.offset.y < 0)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                        gameObject.transform.position.y + 0.01f, gameObject.transform.position.z);
                }
                else
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                        gameObject.transform.position.y - 0.01f, gameObject.transform.position.z);
                }
            }
            else
            {
                if(collision.collider.offset.x < 0)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x+0.01f,
                        gameObject.transform.position.y, gameObject.transform.position.z);
                }
                else
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.01f,
                        gameObject.transform.position.y, gameObject.transform.position.z);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
