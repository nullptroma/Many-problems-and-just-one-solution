using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : PlayerController
{
    private void Start()
    {
        MCapsuleCollider  = this.transform.GetComponent<CapsuleCollider2D>();
        MAnim = this.transform.Find("model").GetComponent<Animator>();
        mRigidbody = this.transform.GetComponent<Rigidbody2D>();
        MainCamera = Camera.main;
    }

    private void Update()
    {
        CheckFlip();
        CheckInput();
        if (mRigidbody.velocity.magnitude > 30)
            mRigidbody.velocity = new Vector2(mRigidbody.velocity.x - 0.1f, mRigidbody.velocity.y - 0.1f);
    }

    private void CheckFlip()
    {
        if (MouseToTheLeft())
            Flip(true);
        else
            Flip(false);
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
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //MAnim.Play("Attack");
        }
        else
        {
            if (MMoveX == 0)
            {
                if (!onceJumpRayCheck)
                    MAnim.Play("Idle");
            }
            else
                MAnim.Play("Run");
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (isGrounded)
                transform.transform.Translate(Vector2.right * (MMoveX * moveSpeed * Time.deltaTime));
            else
                transform.transform.Translate(new Vector3(MMoveX * moveSpeed * Time.deltaTime, 0, 0));
            MAnim.SetFloat($"RunSpeed", IsLeft?-1:1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (isGrounded)
                transform.transform.Translate(Vector2.right * (MMoveX * moveSpeed * Time.deltaTime));
            else
                transform.transform.Translate(new Vector3(MMoveX * moveSpeed * Time.deltaTime, 0, 0));
            MAnim.SetFloat($"RunSpeed", IsLeft?1:-1);
        }
        else
            MAnim.SetFloat($"RunSpeed", 1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentJumpCount < jumpCount)  // 0 , 1
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
