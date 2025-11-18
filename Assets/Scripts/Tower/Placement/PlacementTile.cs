using UnityEngine;

public class PlacementTile : MonoBehaviour
{
    public Material emptyMaterial;
    public Material filledMaterial;
    public Renderer tileRenderer;

    public void SetState(bool fill)
    {
        if (fill)
        {
            tileRenderer.sharedMaterial = filledMaterial;
        }
        else
        {
            tileRenderer.sharedMaterial = emptyMaterial;
        }
    }
}