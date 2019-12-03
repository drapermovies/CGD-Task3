using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Vector3> waypoints { get; set; }

    [Header("Pathfinding")]
    [SerializeField] private List<Vector3> v3_waypoints = new List<Vector3>();
    [SerializeField] private List<Transform> t_waypoints = new List<Transform>();

    private void Start()
    {
        MergeWaypoints();
    }

    //Vector3's easier for code, Transform easiers for level editors
    //Merge these together to combine them
    private void MergeWaypoints()
    {
        waypoints = new List<Vector3>();

        foreach (Vector3 vector in v3_waypoints)
        {
            waypoints.Add(vector);
        }

        foreach (Transform tform in t_waypoints)
        {
            waypoints.Add(tform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach(Vector3 wpoint in v3_waypoints)
        {
            Gizmos.DrawCube(wpoint, new Vector3(0.5f, 0.5f, 0.5f));
        }

        foreach (Transform tpoint in t_waypoints)
        {
            Gizmos.DrawCube(tpoint.position, new Vector3(0.5f, 0.5f, 0.5f));
        }
    }
}
