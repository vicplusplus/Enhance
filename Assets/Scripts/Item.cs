using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Item : MonoBehaviour
{
    public Vector3 defaultPosition;
    public float maxDragDistance;
    public float interactDragDistance;
    [Range(0, 1)]
    public float upgradeChance;
    public float distanceDragThreshold;
    public State state;
    public Sprite rawSprite;
    public Sprite craftedSprite;
    public Sprite brokenSprite;
    public SpriteRenderer trashIndicator;
    private bool isDragging;
    private float distanceDragged;
    private SpriteRenderer spriteRenderer;
    private ScoreManager scoreManager;
    private ItemPile itemPile;
    private Collider2D col;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        
    }

    void Start()
    {
        trashIndicator = GameObject.Find("Trash Indicator").GetComponent<SpriteRenderer>();
        scoreManager = FindObjectOfType<ScoreManager>();
        itemPile = FindObjectOfType<ItemPile>();
        state = State.Raw;
        spriteRenderer.sprite = rawSprite;
    }

    void Update()
    {
        if (isDragging)
        {
            // To distinguish between a normal click and a drag, measure how much the mouse has moved while the left button is held down
            // This makes a long tap also just a click
            float newX = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x, -maxDragDistance, maxDragDistance);
            distanceDragged += Mouse.current.delta.ReadValue().magnitude;
            if (distanceDragged > distanceDragThreshold)
            {
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }

            trashIndicator.enabled = newX > interactDragDistance;



            if (!Mouse.current.leftButton.isPressed)
            {
                if (distanceDragged > distanceDragThreshold)
                {
                    // This checks if the item is dragged to the right, and trashes it unconditionally
                    if (newX > interactDragDistance)
                    {
                        trashIndicator.enabled = false;
                        Destroy(gameObject);
                    }
                    // This checks if the item is dragged to the left, and submits it if is in a valid crafted state
                    // Otherwise, it just teleports it back to the usual position.
                    else if (newX < -interactDragDistance)
                    {
                        if (state == State.Crafted)
                        {
                            scoreManager.Score++;
                            itemPile.MoveToPile(gameObject);
                        }
                        else
                        {
                            transform.position = defaultPosition;
                        }
                    }
                    else
                    {
                        transform.position = defaultPosition;
                    }
                }
                else
                {
                    OnClick();
                }
                isDragging = false;
            }
        }
        else
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (Mouse.current.leftButton.isPressed && col.OverlapPoint(mousePos))
            {
                isDragging = true;
                // Reset distance dragged on each click
                distanceDragged = 0;
            }
            trashIndicator.enabled = false;
        }
    }

    // Uses a state machine to determine what to do on click, includes changing sprites
    void OnClick()
    {
        switch (state)
        {
            case State.Raw:
                if (Random.value > 1 - upgradeChance)
                {
                    state = State.Crafted;
                    spriteRenderer.sprite = craftedSprite;
                }
                break;
            case State.Crafted:
                state = State.Broken;
                spriteRenderer.sprite = brokenSprite;
                break;
        }
    }

    [System.Serializable]
    public enum State
    {
        Raw,
        Crafted,
        Broken
    }
}
