using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PullCord : MonoBehaviour
{
    private bool isDragging;
    public float pullThreshold;
    private Vector3 defaultPos;
    public float maxPull;
    private Collider2D col;
    private ItemSpawner spawner;

    private void Awake()
    {
        spawner = FindObjectOfType<ItemSpawner>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        defaultPos = transform.position;
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (isDragging)
        {
            // Letting go past a certain pull limit spawns an item and resets the pull cord
            if(!Mouse.current.leftButton.isPressed)
            {
                transform.position = defaultPos;
                if (mousePos.y - col.offset.y < defaultPos.y - pullThreshold)
                {
                    spawner.SpawnItem();
                }
                isDragging = false;
            }
            else
            {
                float clampedY = Mathf.Clamp(mousePos.y - col.offset.y, defaultPos.y - maxPull, defaultPos.y);
                transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
            }
        }
        else
        {
            if(Mouse.current.leftButton.isPressed && col.OverlapPoint(mousePos))
            {
                isDragging = true;
            }
        }
    }
}
