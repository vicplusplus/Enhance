using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemPrefabs;
    public GameObject currentObject;

    public void SpawnItem()
    {
        if (!currentObject)
        {
            GameObject selectedPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Count - 1)];
            Vector3 pos = selectedPrefab.GetComponent<Item>().defaultPosition;
            currentObject = Instantiate(selectedPrefab, pos, Quaternion.identity, transform);
        }
    }
}
