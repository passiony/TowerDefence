using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : Targetable
{
    private NavMeshAgent m_NavMeshAgent;
    public NodePath Path;
    private int nodeIndex;
    private bool startMove;
    
    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        StartMove();
    }
    
    void StartMove()
    {
        nodeIndex = 0;
        var target = Path.waypoints[nodeIndex];
        m_NavMeshAgent.SetDestination(target.position);
        startMove = true;
    }
    
    void MoveNext()
    {
        nodeIndex++;
        if (nodeIndex < Path.waypoints.Length)
        {
            var target = Path.waypoints[nodeIndex];
            m_NavMeshAgent.SetDestination(target.position);
        }
    }
    
    void Update()
    {
        if (startMove)
        {
            if (m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance)
            {
                MoveNext();
            }
        }
    }
        
    public override void OnRemoved()
    {
        base.OnRemoved();
        m_NavMeshAgent.isStopped = true;
        m_NavMeshAgent.enabled = false;
        Destroy(gameObject);
    }
}
