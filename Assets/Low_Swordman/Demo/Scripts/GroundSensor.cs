﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GroundSensor : MonoBehaviour {

    public PlayerController mRoot;

    void Start()
    {
        mRoot = this.transform.root.GetComponent<PlayerController>();
    }

    ContactPoint2D[] _contacts = new ContactPoint2D[1];
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag($"Ground") || other.CompareTag($"Block"))
        {
            if (other.CompareTag($"Ground"))
                mRoot.isDownJumpGroundCheck = true;
            else
                mRoot.isDownJumpGroundCheck = false;

            if (mRoot.mRigidbody.velocity.y <= 0)
            {

                mRoot.isGrounded = true;
                mRoot.currentJumpCount = 0;
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        mRoot.isGrounded = false;
    }
}
