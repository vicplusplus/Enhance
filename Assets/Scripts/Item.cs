using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof (SpriteRenderer), typeof(Collider2D))]
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
    private bool isDragging;
    private float distanceDragged;
    private SpriteRenderer spriteRenderer;
    private ScoreManager scoreManager;
    private ItemPile itemPile;
    private Collider2D col;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        scoreManager = FindObjectOfType<ScoreManager>();
        itemPile = FindObjectOfType<ItemPile>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        state = State.Raw;
        spriteRenderer.sprite = rawSprite;
    }

    void Update()
    {
        if (isDragging)
        {
            float newX = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x, -maxDragDistance, maxDragDistance);
            distanceDragged += Mouse.current.delta.ReadValue().magnitude;
            if (distanceDragged > distanceDragThreshold)
            {
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            if (!Mouse.current.leftButton.isPressed)
            {
                if (distanceDragged > distanceDragThreshold)
                {
                    if (newX > interactDragDistance)
                    {
                        Destroy(gameObject);
                    }
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
                distanceDragged = 0;
            }
        }
    }

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
