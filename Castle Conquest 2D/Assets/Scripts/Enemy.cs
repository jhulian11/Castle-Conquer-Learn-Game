using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyRunSpeed = 5f;
    private Rigidbody2D enemRigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        enemRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingLeft())
         enemRigidbody2D.velocity = new Vector2(-enemyRunSpeed, enemRigidbody2D.velocity.y);

        else enemRigidbody2D.velocity = new Vector2(enemyRunSpeed, enemRigidbody2D.velocity.y);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprite();
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(enemRigidbody2D.velocity.x), 1f);
    }

    private bool IsFacingLeft()
    {
        return transform.localScale.x > 0f;
    }
}
