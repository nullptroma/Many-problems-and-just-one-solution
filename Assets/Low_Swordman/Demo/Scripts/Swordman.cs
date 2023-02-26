﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Swordman : PlayerController
{
    [SerializeField] private Magnet magnet;
    [SerializeField] private FlipXScale flipper;
    
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
        Flip(MouseToTheLeft());
    }
    
    private void FixedUpdate()
    {
        if (isSit || isDie)
            MMoveX = 0;
        var velocity = mRigidbody.velocity;
        if (MMoveX != 0)
        {
            if(isGrounded)
                velocity.x = Mathf.MoveTowards(velocity.x, moveSpeed * MMoveX, walkAcceleration*Time.deltaTime);
            else
                velocity.x = Mathf.MoveTowards(velocity.x, moveSpeed * MMoveX, airAcceleration*Time.deltaTime);
        }
        else if(isGrounded)
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration*Time.deltaTime);
        else
            velocity.x = Mathf.MoveTowards(velocity.x, 0, airDeceleration*Time.deltaTime);
        mRigidbody.velocity = velocity;
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.R) && loader != null)
            loader.ReloadScene();
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

        
        if (isDie)
        {
            MMoveX = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
            magnet.SetEnableMagnet(true);
        else if(Input.GetKeyUp(KeyCode.Mouse0))
            magnet.SetEnableMagnet(false);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            magnet.ChangeMode();
            flipper.Flip();
        }
        if (isSit)
        {
            MMoveX = 0;
            if (Input.GetKeyDown(KeyCode.Space))
                if (currentJumpCount < jumpCount)
                    DownJump();
            return;
        }
        else
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

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag($"Kill"))
            Die(true);
    }


    protected override void LandingEvent()
    {
        if (!MAnim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            MAnim.Play("Idle");
    }
}