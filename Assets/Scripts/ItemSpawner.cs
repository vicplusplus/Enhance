using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSpawner : MonoBehaviour
{
    // Can randomly pick an item to spawn for more variety
    public List<WeightedObject> itemPrefabs;
    public GameObject currentObject;

    private float totalWeights;

    private void Start()
    {
        totalWeights = 0;
        foreach (WeightedObject o in itemPrefabs)
        {
            totalWeights += o.weight;
        }
        print(totalWeights);
    }

    public void SpawnItem()
    {
        if (!currentObject)
        {
            GameObject selectedPrefab = null;
            float rand = Random.value;
            foreach (WeightedObject o in itemPrefabs)
            {
                if(rand < o.weight / totalWeights)
                {
                    selectedPrefab = o.obj;
                    break;
                }
                rand -= o.weight / totalWeights;
            }
            // Accounting for fall animation
            Vector3 pos = selectedPrefab.GetComponent<Item>().defaultPosition + 2.78f * Vector3.up;
            currentObject = Instantiate(selectedPrefab, pos, Quaternion.identity, transform);
        }
    }

    [System.Serializable]
    public struct WeightedObject
    {
        public GameObject obj;
        public float weight;
    }
}
