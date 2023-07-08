using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public GameObject path;
    private GameObject currentPath;
    public GameObject explosion;
    private List<Vector3> waypoints;
    private int currentWaypointIndex = 0;
    private float moveSpeed;

    public List<Vector3> GetWaypoints()
    {
        // Instantiate path at unit's position
        currentPath = Instantiate(path, transform.position, path.transform.rotation);

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
        // Set initial position
        waypoints = points;
        transform.position = waypoints[0];

        // Set speed
        moveSpeed = speed;
    }

    /*private GameObject GetPath()
    {
        // Find path in scene
    }*/

    private void Start()
    {
        // Get speed from unit stat manager
        float speed = GetComponent<UnitStatManager>().GetSpeed();

        // Set params
        SetFollowParams(GetWaypoints(), speed);
    }

    private void Update()
    {
        // follow path.......
        FollowPath();
    }

    private void FollowPath()
    {
        // If not arrived at the end of the path
        if (currentWaypointIndex < waypoints.Count)
        {
            // get next wp position "destination"
            Vector3 destination = waypoints[currentWaypointIndex];

            // move towards "destination"
            transform.position = Vector3.MoveTowards(
                transform.position,
                destination,
                moveSpeed / 8 * Time.deltaTime);

            // once position equal to "destination"
            if (transform.position == destination)
            {
                // increment current wp index
                currentWaypointIndex++;
            }
        }
        else
        {
            // Declare an empty wps list
            List<Vector3> newWps = new List<Vector3>();

            // iterate over wps
            for (int i = 0; i < waypoints.Count; i++)
            {
                // get the distance between the last wp and the first one
                float distance = Vector3.Distance(waypoints[waypoints.Count - 1], waypoints[0]);

                // add the distance to each wp and append it to the list
                newWps.Add(waypoints[i] + new Vector3(0, distance, 0));
            }

            // equal prev waypoints list to the new one 
            waypoints = newWps;

            // reset current wp index
            currentWaypointIndex = 0;
        }
    }


    // On Projectile Hit
    void OnTriggerEnter2D(Collider2D col)
    {
        print(col.tag);
        if (col.tag == "DefenderProjectile")
        {
            //float str = col.GameObject<ProjectileStat>().GetStrengh();
            //GetComponent<UnitStatManager>().DecrementHealth(str);
        }
        else if (col.tag == "Settlement")
        {
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            Destroy(gameObject);
            Destroy(currentPath);
        }
    }
}
