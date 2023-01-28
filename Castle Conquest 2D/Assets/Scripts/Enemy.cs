using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyRunSpeed = 5f;
    private Rigidbody2D enemRigidbody2D;
    private Collider2D enemyCollider2D;
    private Collider2D enemyHitCollider2D;

    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        enemRigidbody2D = GetComponent<Rigidbody2D>();
        enemyCollider2D =GetComponent<BoxCollider2D>();
        enemyHitCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    public void Dying()
    {
       myAnimator.SetTrigger("Die");
       enemyHitCollider2D.enabled = false;
       enemyCollider2D.enabled = false;
       enemRigidbody2D.bodyType = RigidbodyType2D.Static;

       StartCoroutine(DestroyEnemy());
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void EnemyMovement()
    {
        if (enemRigidbody2D)
            enemRigidbody2D.velocity = IsFacingLeft() ? new Vector2(-enemyRunSpeed, enemRigidbody2D.velocity.y) : new Vector2(enemyRunSpeed, enemRigidbody2D.velocity.y);
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
