using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Swordman : PlayerController
{
    private void Start()
    {
        MCapsuleCollider = this.transform.GetComponent<CapsuleCollider2D>();
        MAnim = this.transform.Find("model").GetComponent<Animator>();
        mRigidbody = this.transform.GetComponent<Rigidbody2D>();
        MainCamera = Camera.main;
    }

    private void Update()
    {
        CheckInput();
        CheckFlip();
        //if (mRigidbody.velocity.magnitude > 30)
        //mRigidbody.velocity = new Vector2(mRigidbody.velocity.x - 0.1f, mRigidbody.velocity.y - 0.1f);
    }

    private void CheckFlip()
    {
        if (MouseToTheLeft())
            Flip(true);
        else
            Flip(false);
    }

    private void FixedUpdate()
    {
        mRigidbody.velocity = new Vector2(MMoveX * moveSpeed, mRigidbody.velocity.y);
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isSit = true;
            MAnim.Play("Sit");
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            MAnim.Play("Idle");
            isSit = false;
        }

        if (MAnim.GetCurrentAnimatorStateInfo(0).IsName("Sit") || MAnim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
                if (currentJumpCount < jumpCount)
                    DownJump();
            return;
        }

        MMoveX = Input.GetAxis("Horizontal");

        GroundCheckUpdate();

        if (MMoveX == 0)
            MAnim.Play("Idle");
        else
            MAnim.Play("Run");

        if (isGrounded == false)
            MAnim.SetFloat($"RunSpeed", 1);
        else if (Input.GetKey(KeyCode.D))
            MAnim.SetFloat($"RunSpeed", CalcSpeedOfRunAnim() * (IsLeft ? -1 : 1));
        else if (Input.GetKey(KeyCode.A))
            MAnim.SetFloat($"RunSpeed", CalcSpeedOfRunAnim() * (IsLeft ? 1 : -1));
        else
            MAnim.SetFloat($"RunSpeed", CalcSpeedOfRunAnim());

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentJumpCount < jumpCount) // 0 , 1
            {
                if (!isSit)
                    PerformJump();
                else
                    DownJump();
            }
        }
    }


    protected override void LandingEvent()
    {
        if (!MAnim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            MAnim.Play("Idle");
    }
}