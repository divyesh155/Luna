using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> waypoints;
    public float moveSpeed;
    public int target;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[target].position, moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (transform.position == waypoints[target].position)
        {
            if (target == waypoints.Count - 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                target = 0;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                target = 1;
            }
        }
    }
}
