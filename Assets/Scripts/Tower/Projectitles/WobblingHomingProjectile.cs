using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WobblingHomingProjectile : HomingProjectile
{
    protected enum State
    {
        Wobbling,
        Turning,
        Targeting
    }

    public Vector2 wobbleTimeRange = new(1, 2);
    public float wobbleDirectionChangeSpeed = 4;
    public float wobbleMagnitude = 7;
    public float turningTime = 0.5f;
    public event Action fired;

    State m_State;
    protected float m_CurrentWobbleTime;
    protected float m_WobbleDuration;
    protected float m_CurrentTurnTime;
    protected float m_WobbleChangeTime;

    protected Vector3 m_WobbleVector, m_TargetWobbleVector;

    protected void Update()
    {
        if (m_HomingTarget == null)
        {
            m_Rigidbody.rotation = Quaternion.LookRotation(m_Rigidbody.velocity);
            return;
        }
        
        switch (m_State)
        {
            case State.Wobbling:
            {
                m_CurrentWobbleTime += Time.deltaTime;
                if (m_CurrentWobbleTime >= m_WobbleDuration)
                {
                    m_State = State.Turning;
                    m_CurrentTurnTime = 0;
                }

                m_WobbleChangeTime += Time.deltaTime * wobbleDirectionChangeSpeed;
                if (m_WobbleChangeTime >= 1)
                {
                    m_WobbleChangeTime = 0;
                    m_TargetWobbleVector = new Vector3(Random.Range(-wobbleMagnitude, wobbleMagnitude),
                        Random.Range(-wobbleMagnitude, wobbleMagnitude), 0);
                    m_WobbleVector = Vector3.zero;
                }

                m_WobbleVector = Vector3.Lerp(m_WobbleVector, m_TargetWobbleVector, m_WobbleChangeTime);
                m_Rigidbody.velocity = Quaternion.Euler(m_WobbleVector) * m_Rigidbody.velocity;
                m_Rigidbody.rotation = Quaternion.LookRotation(m_Rigidbody.velocity);
                break;
            }
            case State.Turning:
            {
                m_CurrentTurnTime += Time.deltaTime;
                var heading = m_HomingTarget.position - transform.position;
                Quaternion aimDirection = Quaternion.LookRotation(heading);

                m_Rigidbody.rotation =
                    Quaternion.Lerp(m_Rigidbody.rotation, aimDirection, m_CurrentTurnTime / turningTime);
                m_Rigidbody.velocity = transform.forward * m_Rigidbody.velocity.magnitude;

                if (m_CurrentTurnTime >= turningTime)
                {
                    m_State = State.Targeting;
                }
            }
                break;
            case State.Targeting:
            {
                var heading = m_HomingTarget.position - transform.position;
                Quaternion aimDirection = Quaternion.LookRotation(heading);

                m_Rigidbody.rotation = aimDirection;
                m_Rigidbody.velocity = transform.forward * m_Rigidbody.velocity.magnitude;
                break;
            }
        }
    }

    protected override void Fire(Vector3 firingVector)
    {
        m_TargetWobbleVector = new Vector3(Random.Range(-wobbleMagnitude, wobbleMagnitude),
            Random.Range(-wobbleMagnitude, wobbleMagnitude), 0);
        m_WobbleDuration = Random.Range(wobbleTimeRange.x, wobbleTimeRange.y);
        m_State = State.Wobbling;
        m_CurrentWobbleTime = 0.0f;

        base.Fire(firingVector);
    }
}