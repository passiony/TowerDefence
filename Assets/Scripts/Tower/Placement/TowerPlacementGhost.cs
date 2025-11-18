using UnityEngine;

//专门用于放置炮塔的类
public class TowerPlacementGhost : MonoBehaviour
{
    protected MeshRenderer[] m_MeshRenderers;

    /// <summary>
    /// 分别用于表示有效和无效放置位置的两种材质
    /// </summary>
    public Material validMaterial;

    public Material invalidMaterial;

    public Tower controller { get; private set; }

    public void Initialize(Tower tower)
    {
        controller = tower;
        m_MeshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void Move(Vector3 worldPosition, Quaternion transformRotation, bool validLocation)
    {
        transform.position = worldPosition;
        transform.rotation = transformRotation;
        foreach (MeshRenderer meshRenderer in m_MeshRenderers)
        {
            meshRenderer.sharedMaterial = validLocation ? validMaterial : invalidMaterial;
        }
    }
}