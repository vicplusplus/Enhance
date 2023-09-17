using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPile : MonoBehaviour
{
    public float maxRadius;
    public float displacemnt;
    public Rect currentBounds;
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
        // Generate a random coordinate
        // x is clamped to a horizontal radius
        // y is always above the base of the pile
        // incrementally goes up based on the position of previous items added to the pile
        float x = Random.Range(currentBounds.xMin, currentBounds.xMax);
        float y = Random.Range(currentBounds.yMin, currentBounds.yMax);
        float angle = Random.Range(-Mathf.PI, Mathf.PI);
        bool flip = Random.value > 0.5f;
        float finalX = Mathf.Clamp(x + displacemnt * Mathf.Cos(angle), transform.position.x - maxRadius, transform.position.x + maxRadius);
        float finalY = Mathf.Max(y + displacemnt * Mathf.Sin(angle), currentBounds.yMin);
        Vector3 pos = new Vector3(finalX, finalY, 0);

        // Expands the pile bounds to include current item
        currentBounds.max = Vector2.Max(currentBounds.max, pos);
        currentBounds.min = Vector2.Min(currentBounds.min, pos);

        // Remove all interaction from the item, leaving it as just a static image sitting in the pile
        obj.transform.parent = transform;
        obj.transform.position = pos;
        obj.transform.rotation = Quaternion.Euler(0, flip ? 180 : 0, Mathf.Rad2Deg * angle / 10);
        Item itemComponent = obj.GetComponent<Item>();
        if (itemComponent) Destroy(itemComponent);
        SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
        sprite.sortingOrder += Random.Range(-2, 3);
        spawner.currentObject = null;
    }
}
