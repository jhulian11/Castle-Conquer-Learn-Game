using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float runSpeed = 10f;  
    [SerializeField]
    private float jumpSpeed = 6.8f;
    [SerializeField]
    private Vector2 hitKick = new Vector2(50f, 60f);
    [SerializeField]
    private float climbSpeed = 1.8f;  
    [SerializeField]
    private float timeHurted = 1.5f; 

    [SerializeField]
    private Transform hurtBox; 
    [SerializeField]
    private float attackRadius = 3f; 
    

    private Animator playerAnimator;

    private Rigidbody2D myRigidbody2D;
    private Collider2D myCollider2D;
    private Collider2D myFeetCollider2D;
    //private Transform myTransform;

    private bool isHurting = false;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        //myTransform = GetComponent<Transform>();
        playerAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<BoxCollider2D>();
        myFeetCollider2D = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        if (!isHurting)
        {
            Jump();
            Run();
            Climb();
            Attack();

            if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
                PlayerHit();
        }

        EnterAnotherLevel();
    }

    private void EnterAnotherLevel()
    {
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Interactable")) && CrossPlatformInputManager.GetButtonDown("Vertical"))
        {
           FindObjectOfType<ExitDoor>().StartLoadingNextLevel();
        }
    }

    private void Attack()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            playerAnimator.SetTrigger("Attacking");
           Collider2D[] enemiesToHit = Physics2D.OverlapCircleAll(hurtBox.position, attackRadius, LayerMask.GetMask("Enemy"));

           foreach (var enemy in enemiesToHit)
           {
              enemy.GetComponent<Enemy>().Dying();
           }
        }
    }
    public void PlayerHit()
    {
        myRigidbody2D.velocity = hitKick * new Vector2(-transform.localScale.x, 1f);
        playerAnimator.SetTrigger("Hitting");
        isHurting = true;

        StartCoroutine(StopHurting()); 
    }
    IEnumerator StopHurting()
    {
        yield return new WaitForSeconds(timeHurted);

        isHurting = false;
    }
    private void Jump()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) && !myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climb")))
            return;

        Debug.Log("teste");
        var isJumping = CrossPlatformInputManager.GetButtonDown("Jump");

        if (isJumping)
        {
            Vector2 jumpVelocity = new Vector2(myRigidbody2D.velocity.x, jumpSpeed);
            myRigidbody2D.velocity = jumpVelocity;
        }
    }

    private void Run()
    {
        var controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed,myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;

        var runningHorizontaly = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

        FlipSprite(runningHorizontaly);
        ChangingRunningState(runningHorizontaly);

    }

    private void ChangingRunningState(bool running)
    {
        playerAnimator.SetBool("Running", running);

    }

    private void FlipSprite(bool runningHorizontaly)
    {
        if (!runningHorizontaly) return;

        transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x),1f);

        //if (Mathf.Sign(myRigidbody2D.velocity.x) > 0)
        //    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        //else if(Mathf.Sign(myRigidbody2D.velocity.x) < 0)
        //    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
    }

    private void Climb()
    {

        myRigidbody2D.gravityScale = 1;
        var isClimbing = false;
        ChangingClimbingState(isClimbing);
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climb")))
            return;

        var controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 verticalVelocity = new Vector2(myRigidbody2D.velocity.x,  (controlThrow * climbSpeed));
        myRigidbody2D.gravityScale = 0;
        myRigidbody2D.velocity = verticalVelocity;

        isClimbing = myRigidbody2D.velocity.y != 0;
        ChangingClimbingState(isClimbing);

    }

    private void ChangingClimbingState(bool climbing)
    {
        playerAnimator.SetBool("Climbing", climbing);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hurtBox.position, attackRadius);
    }
}

