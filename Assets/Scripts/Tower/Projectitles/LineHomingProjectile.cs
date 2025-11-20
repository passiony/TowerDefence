using System;
using UnityEngine;

public class LineHomingProjectile : HomingProjectile
{
    public float homingSpeed = 10f; // 追踪速度
    
    private void FixedUpdate()
    {
        if (m_HomingTarget == null) return;
    
        // 目标方向（归一化）
        var heading = (m_HomingTarget.position - transform.position).normalized;
    
        // 期望朝向
        Quaternion aimDirection = Quaternion.LookRotation(heading);
    
        // 平滑旋转（使用 FixedDeltaTime 以匹配物理步长）
        Quaternion newRotation =
            Quaternion.Slerp(m_Rigidbody.rotation, aimDirection, homingSpeed * Time.fixedDeltaTime);
    
        // 使用 MoveRotation 与物理系统交互
        m_Rigidbody.MoveRotation(newRotation);
    
        // 保持当前速度大小，方向与新朝向一致
        float speed = m_Rigidbody.velocity.magnitude;
        m_Rigidbody.velocity = newRotation * Vector3.forward * speed;
    }

}