using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlacementCtrl : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject prefabGhost;
    [SerializeField] private GameObject objGhost;
    private static PlacementCtrl instance;
    public static PlacementCtrl Instance => instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Debug.LogError("Only 1 PlacementCtrl allows to exist!");
    }

    private void Update()
    {
        Vector3 mouseScreenPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseScreenPos.z = 0f;
        Vector3 spawnPos = new Vector3(Mathf.RoundToInt(mouseScreenPos.x), Mathf.RoundToInt(mouseScreenPos.y), Mathf.RoundToInt(mouseScreenPos.z));
        if (objGhost == null) objGhost = Instantiate(prefabGhost, spawnPos, Quaternion.identity);
        else objGhost.transform.position = spawnPos;
    }

    public void Spawn(Vector3 position)
    {
        Vector3 spawnPos = new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z));

        // Create a virtual 0.9x0.9 box at that position to check for overlaps
        // (Using 0.9 instead of 1.0 to avoid accidentally hitting adjacent cells)
        Collider2D hit = Physics2D.OverlapBox(spawnPos, new Vector2(0.9f, 0.9f), 0f);

        // If something is hit (hit is not null) -> An object already exists -> Skip placement
        if (hit != null)
        {
            Debug.Log("There's already a structure in this cell!");
            return;
        }

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
