using UnityEngine;

public class AgentSlower : MonoBehaviour
{
    protected GameObject m_SlowFx;

    public void Initialize(float slowFactor,GameObject slowEffect)
    {
        var agent = GetComponent<Agent>();
        if (agent != null)
        {
            var originalSpeed = agent.originalMovementSpeed;
            var newSpeed = originalSpeed * slowFactor;
            agent.navMeshNavMeshAgent.speed = newSpeed;
        }
        
        if(m_SlowFx==null && slowEffect!=null)
        {
            m_SlowFx = Instantiate(slowEffect, transform);
            m_SlowFx.transform.localPosition = Vector3.zero;
        }
    }

    public void RemoveSlow(float slowFactor)
    {
        var agent = GetComponent<Agent>();
        agent.navMeshNavMeshAgent.speed = agent.originalMovementSpeed;
        if (m_SlowFx)
        {
            Destroy(m_SlowFx);
        }
    }
}