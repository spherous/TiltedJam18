using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class WalkingAnimator : MonoBehaviour
{
    [SerializeField]
    public Sprite idle = default;
    [SerializeField]
    private Sprite[] ups = default;
    [SerializeField]
    private Sprite[] downs = default;
    [SerializeField]
    private Sprite[] rights = default;

    private static float frameRate = 6;


    private Sprite[] sprites;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Dir dir = Dir.Idle;
    private int spriteIndex = 0;
    private float elapsed = 0;
    
    private enum Dir
    {
        Idle,
        Up,
        Down,
        Left,
        Right
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (Mathf.Approximately(rb.velocity.x, 0) && Mathf.Approximately(rb.velocity.y, 0))
        {
            sr.sprite = idle;
        }
        else
        {
            UpdateDir();

            if (elapsed > 1f / frameRate)
            {
                elapsed = 0f;
                sr.sprite = sprites[spriteIndex];
                spriteIndex = (spriteIndex + 1) % sprites.Length;
            }
        }
    }

    private void UpdateDir ()
    {
        if (Mathf.Abs (rb.velocity.x) > Mathf.Abs (rb.velocity.y))
        {
            if (rb.velocity.x > 0 && dir != Dir.Right)
            {
                sr.flipX = false;
                sprites = rights;
                dir = Dir.Right;
            }
            else if (dir != Dir.Left)
            {
                sr.flipX = true;
                sprites = rights;
                dir = Dir.Left;
            }
        }
        else
        {
            if (rb.velocity.y > 0 && dir != Dir.Up)
            {
                sr.flipX = false;
                sprites = ups;
                dir = Dir.Up;
            }
            else if (dir != Dir.Down)
            {
                sr.flipX = false;
                sprites = downs;
                dir = Dir.Down;
            }
        }
    }

}
