using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Left;
    public bool Right;
    private SpriteRenderer spriteRenderer;
    public Color OrigColor;
    public Color FlashColor;
    public float FlashInterval;
    public float FlashTime;
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = OrigColor;
    }

    // Update is called once per frame
    void Update()
    {
        FlashTime += Time.deltaTime;
        if (FlashTime > FlashInterval)
        {
            if (spriteRenderer.color == OrigColor)
            {
                spriteRenderer.color = FlashColor;
            }
            else
            {
                spriteRenderer.color = OrigColor;
            }
            FlashTime = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
    }
    void OnTriggerExit2D(Collider2D col)
    {
    }
}
