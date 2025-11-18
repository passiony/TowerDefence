using UnityEngine;

public static class Ballistics
{
    public static Vector3 CalculateBallisticFireVector(Vector3 firePosition, Vector3 targetPosition,
        float launchAngle, float gravity)
    {
        Vector3 target = targetPosition;
        target.y = firePosition.y;
        Vector3 toTarget = target - firePosition;
        float targetDistance = toTarget.magnitude;
        float shootingAngle = launchAngle;
        float relativeY = firePosition.y - targetPosition.y;

        float theta = Mathf.Deg2Rad * shootingAngle;
        float cosTheta = Mathf.Cos(theta);
        float num = targetDistance * Mathf.Sqrt(gravity) * Mathf.Sqrt(1 / cosTheta);
        float denom = Mathf.Sqrt((2 * targetDistance * Mathf.Sin(theta)) + (2 * relativeY * cosTheta));

        if (denom > 0)
        {
            float v = num / denom;

            // 展平瞄准向量以便我们可以旋转它
            Vector3 aimVector = toTarget / targetDistance;
            aimVector.y = 0;
            Vector3 rotAxis = Vector3.Cross(aimVector, Vector3.up);
            Quaternion rotation = Quaternion.AngleAxis(shootingAngle, rotAxis);
            aimVector = rotation * aimVector.normalized;

            return aimVector * v;
        }

        return Vector3.zero;
    }
}