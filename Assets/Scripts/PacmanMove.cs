using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;
    Vector2 prevDir = Vector2.zero;
    Vector2 nextDir = Vector2.zero;

    void Start()
    {
        dest = transform.position;
    }

    void FixedUpdate()
    {
        // Move closer to Destination
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        // Get input for next turn
        if (Input.GetKey(KeyCode.UpArrow))
            nextDir = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow))
            nextDir = -Vector2.up;
        else if (Input.GetKey(KeyCode.LeftArrow))
            nextDir = -Vector2.right;
        else if (Input.GetKey(KeyCode.RightArrow))
            nextDir = Vector2.right;

        // Check if pacman can change direction
        if ((Vector2)transform.position == dest)
        {
            if (valid(nextDir))
            {
                dest = (Vector2)transform.position + nextDir;
                prevDir = nextDir;
            }
            else if (valid(prevDir))
            {
                dest = (Vector2)transform.position + prevDir;
            }

            // Animation Parameters
            Vector2 dir = dest - (Vector2)transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }

    bool valid(Vector2 dir)
    {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}
