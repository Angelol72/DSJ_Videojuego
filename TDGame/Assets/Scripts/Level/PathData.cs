using UnityEngine;
using System.Collections.Generic;

public class PathData : MonoBehaviour
{
    public List<Transform> waypoints; // List of waypoints for the path

    // Visualize the path in the editor
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count < 2) return;

        Gizmos.color = Color.green;

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            if (waypoints[i] != null && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                Gizmos.DrawSphere(waypoints[i].position, 0.1f);
            }
        }

        // Dibuja el último punto
        if (waypoints[waypoints.Count - 1] != null)
        {
            Gizmos.DrawSphere(waypoints[waypoints.Count - 1].position, 0.1f);
        }
    }
}
