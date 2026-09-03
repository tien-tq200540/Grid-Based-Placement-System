using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementCtrl : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private static PlacementCtrl instance;
    public static PlacementCtrl Instance => instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Debug.LogError("Only 1 PlacementCtrl allows to exist!");
    }

    public void Spawn(Vector3 position)
    {
        Vector3 spawnPos = new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z));
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        // Select color
        Gizmos.color = new Color(1f, 1f, 1f, 0.2f);

        // Define Grid size
        int gridSize = 10;

        for (int x = -gridSize; x <= gridSize; x++)
        {
            // Draw vertical lines
            Gizmos.DrawLine(new Vector3(x, -gridSize, 0), new Vector3(x, gridSize, 0));
        }

        for (int y = -gridSize; y <= gridSize; y++)
        {
            // Draw horizontal line
            Gizmos.DrawLine(new Vector3(-gridSize, y, 0), new Vector3(gridSize, y, 0));
        }
    }
}
