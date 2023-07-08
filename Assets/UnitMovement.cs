using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public GameObject path;
    private List<Vector3> waypoints;
    private int currentWaypointIndex = 0;
    private float moveSpeed = 2f;

    public List<Vector3> GetWaypoints()
    {
        // Instantiate path at unit's position
        GameObject currentPath = Instantiate(path, transform.position, path.transform.rotation);

        // Declare new waypoint list (to return)
        List<Vector3> waypoints = new List<Vector3>();

        // Iterate over childs of new instantiated path
        foreach (Transform child in currentPath.transform)
        {
            // Add to new declared list of wps
            waypoints.Add(child.position);
        }

        // return wps
        return waypoints;
    }

    public void SetFollowParams(List<Vector3> points, float speed)
    {
        waypoints = points;
        transform.position = waypoints[0];

        moveSpeed = speed;
    }

    /*private GameObject GetPath()
    {
        // Find path in scene
    }*/

    private void Start()
    {
        //path = GetPath();
        SetFollowParams(GetWaypoints(), 2f);
    }

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            Vector3 destination = waypoints[currentWaypointIndex];

            transform.position = Vector3.MoveTowards(
                transform.position,
                destination,
                moveSpeed * Time.deltaTime);

            if (transform.position == destination)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                waypoints[i] += new Vector3(0, 3, 0); // FIXED Y VALUE OF THE HIGHEST MOST WAYPOINT NODE
            }
            currentWaypointIndex = 0;
        }
        /*
        else die fr
            delete instance of path
        */

    }
}
