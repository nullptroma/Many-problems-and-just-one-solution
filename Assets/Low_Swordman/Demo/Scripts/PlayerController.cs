using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerController :MonoBehaviour
{
    public bool isDie = false;
    public bool isSit = false;
    public int currentJumpCount = 0; 
    public bool isGrounded = false;
    public bool onceJumpRayCheck = false;

    public bool isDownJumpGroundCheck = false; 
    protected float MMoveX;
    public Rigidbody2D mRigidbody;
    protected CapsuleCollider2D MCapsuleCollider;
    protected Animator MAnim;
    [SerializeField] protected LevelLoader loader;

    [Header("[Setting]")]
    public float speedOfRunAnim = 0.3f;
    public float minSpeedOfRunAnim = 0.3f;
    public float moveSpeed = 6;
    public int jumpCount = 2;
    public float jumpForce = 15f;
    public float walkAcceleration = 50f; 
    public float groundDeceleration = 100f; 
    public float airAcceleration = 20f; 
    public float airDeceleration = 20f; 
    
    protected Camera MainCamera;

    protected bool IsLeft = true;
    protected void Flip(bool bLeft)
    {
        if(IsLeft == bLeft || isDie)
            return;
        IsLeft = bLeft;
        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);
    }

    protected float CalcSpeedOfRunAnim()
    {
        return Mathf.Max(mRigidbody.velocity.magnitude*speedOfRunAnim, minSpeedOfRunAnim);
    }

    protected bool MouseToTheLeft()
    {
        return MainCamera.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x;
    }

    protected void PerformJump()
    {
        MAnim.Play("Jump");

        mRigidbody.velocity = new Vector2(mRigidbody.velocity.x, 0);

        mRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (currentJumpCount == 0 && isGrounded == false)
            currentJumpCount++;
        
        onceJumpRayCheck = true;
        isGrounded = false;

        currentJumpCount++;
    }

    public void Die(bool d)
    {
        isDie = d;
        if(isDie)
            mRigidbody.velocity = new Vector2(0, mRigidbody.velocity.y);
        MAnim.Play(isDie ? "Die" :"Idle");
        StartCoroutine(Delay(2f, ()=>loader.ReloadScene()));
    }

    protected IEnumerator Delay(float sec, UnityAction action)
    {
        yield return new WaitForSeconds(sec);
        action();
    }

    protected void DownJump()
    {
        if (!isGrounded)
            return;

        if (!isDownJumpGroundCheck)
        {
            MAnim.Play("Jump");

            mRigidbody.AddForce(-Vector2.up * 10);
            isGrounded = false;

            MCapsuleCollider.enabled = false;

            StartCoroutine(GroundCapsuleColliderTimerFuc());
        }
    }

    IEnumerator GroundCapsuleColliderTimerFuc()
    {
        yield return new WaitForSeconds(0.3f);
        MCapsuleCollider.enabled = true;
    }


    float _pretmpY;
    float _groundCheckUpdateTic = 0;
    private const float GroundCheckUpdateTime = 0.01f;

    protected void GroundCheckUpdate()
    {
        if (!onceJumpRayCheck)
            return;
        _groundCheckUpdateTic += Time.deltaTime;
        if (_groundCheckUpdateTic > GroundCheckUpdateTime)
        {
            _groundCheckUpdateTic = 0;

            if (_pretmpY == 0)
            {
                _pretmpY = transform.position.y;
                return;
            }

            float reY = transform.position.y - _pretmpY;  //    -1  - 0 = -1 ,  -2 -   -1 = -3

            if (reY <= 0)
            {
                if (isGrounded)
                {
                    LandingEvent();
                    onceJumpRayCheck = false;
                }
            }
            _pretmpY = transform.position.y;
        }
    }
    
    protected abstract void LandingEvent();
}
