using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Agent : Targetable
{
    private NavMeshAgent m_Agent;
    public NodePath Path;
    private int nodeIndex;
    private bool startMove;
    
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        StartMove();
    }
    
    void StartMove()
    {
        nodeIndex = 0;
        var target = Path.waypoints[nodeIndex];
        m_Agent.SetDestination(target.position);
        startMove = true;
    }
    
    void MoveNext()
    {
        nodeIndex++;
        if (nodeIndex < Path.waypoints.Length)
        {
            var target = Path.waypoints[nodeIndex];
            m_Agent.SetDestination(target.position);
        }
    }
    
    void Update()
    {
        if (startMove)
        {
            if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
            {
                MoveNext();
            }
        }
    }
}
