using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlowField : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AIPath aiPath = collision.GetComponent<AIPath>();

        if (aiPath != null)
        {
            aiPath.maxSpeed = 0.75f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        AIPath aiPath = collision.GetComponent<AIPath>();

        if (aiPath != null)
        {
            aiPath.maxSpeed = 2f;
        }
    }
}
