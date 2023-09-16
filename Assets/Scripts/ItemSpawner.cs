using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemPrefabs;
    public Collider2D pullCordCollider;
    public GameObject currentObject;

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (Mouse.current.leftButton.wasPressedThisFrame && pullCordCollider.OverlapPoint(mousePos))
        {
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        if (!currentObject)
        {
            GameObject selectedPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Count - 1)];
            Vector3 pos = selectedPrefab.GetComponent<Item>().defaultPosition;
            currentObject = Instantiate(selectedPrefab, pos, Quaternion.identity, transform);
        }
    }
}
