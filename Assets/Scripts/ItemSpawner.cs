using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSpawner : MonoBehaviour
{
    // Can randomly pick an item to spawn for more variety
    public List<GameObject> itemPrefabs;
    public int rarityExponent;
    public GameObject currentObject;

    public void SpawnItem()
    {
        if (!currentObject)
        {
            GameObject selectedPrefab = itemPrefabs[(int)(Mathf.Pow(Random.value, rarityExponent) * (itemPrefabs.Count))];
            // Accounting for fall animation
            Vector3 pos = selectedPrefab.GetComponent<Item>().defaultPosition + 2.78f * Vector3.up;
            currentObject = Instantiate(selectedPrefab, pos, Quaternion.identity, transform);
        }
    }
}
