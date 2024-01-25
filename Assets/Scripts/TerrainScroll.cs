using UnityEngine;

public class TerrainScroll : MonoBehaviour
{
    public float scrollSpeed = 5.0f;
    public Renderer terrainRenderer;

    void Update()
    {
        // Reverse the movement of the texture to scroll upwards
        float yTextureOffset = Time.time * scrollSpeed;
        terrainRenderer.material.mainTextureOffset = new Vector2(0, -yTextureOffset);
    }
}
