using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePath : MonoBehaviour
{
    public Color color;
    public float nodeRadius = 1;
    public Node[] waypoints;

    public Node GetNode(int index)
    {
        return waypoints[index];
    }
    
    private void OnDrawGizmos()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                var point1 = waypoints[i];
                Gizmos.color = color;
                Gizmos.DrawWireSphere(point1.position, nodeRadius);
            }

            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                var point1 = waypoints[i];
                var point2 = waypoints[i + 1];

                Gizmos.color = color;
                Gizmos.DrawLine(point1.position, point2.position);
            }
        }
    }
}