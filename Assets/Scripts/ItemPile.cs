using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPile : MonoBehaviour
{
    public float maxRadius;
    public float displacemnt;
    private Rect currentBounds;
    private ItemSpawner spawner;

    private void Awake()
    {
        spawner = FindObjectOfType<ItemSpawner>();
    }

    private void Start()
    {
        currentBounds = new Rect(transform.position, Vector2.zero);
    }

    public void MoveToPile(GameObject obj)
    {
        float x = Random.Range(currentBounds.xMin, currentBounds.xMax);
        float y = Random.Range(currentBounds.yMin, currentBounds.yMax);
        float angle = Random.Range(0, 2 * Mathf.PI);
        float finalX = Mathf.Clamp(x + displacemnt * Mathf.Cos(angle), transform.position.x - maxRadius, transform.position.x + maxRadius);
        float finalY = Mathf.Max(y + displacemnt * Mathf.Sin(angle), currentBounds.yMin);
        Vector3 pos = new Vector3(finalX, finalY, 0);

        currentBounds.max = Vector2.Max(currentBounds.max, pos);
        currentBounds.min = Vector2.Min(currentBounds.min, pos);

        obj.transform.parent = transform;
        obj.transform.position = pos;

        Item itemComponent = obj.GetComponent<Item>();
        if (itemComponent) Destroy(itemComponent);
        spawner.currentObject = null;
    }
}
