using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float runSpeed = 10f;
    [SerializeField]
    private Animator playerAnimator;

    private Rigidbody2D myRigidbody2D;
    //private Transform myTransform;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        //myTransform = GetComponent<Transform>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
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

        if (Mathf.Sign(myRigidbody2D.velocity.x) > 0)
            transform.localRotation = new Quaternion(transform.localRotation.x, 0, transform.localRotation.z, transform.localRotation.w);

        else if(Mathf.Sign(myRigidbody2D.velocity.x) < 0) 
            transform.localRotation = new Quaternion(transform.localRotation.x, -180, transform.localRotation.z, transform.localRotation.w);
    }
}

