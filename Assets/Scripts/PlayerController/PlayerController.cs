using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerController :MonoBehaviour
{
    protected bool IsDie;
    protected bool IsSit = false;
    protected int CurrentJumpCount; 
    protected bool IsGrounded;
    private bool OnceJumpRayCheck;

    public bool isDownJumpGroundCheck; 
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
        if(IsLeft == bLeft || IsDie)
            return;
        IsLeft = bLeft;
        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);
    }

    public void SetIsGrounded(bool grounded)
    {
        IsGrounded = grounded;
        if (IsGrounded)
            CurrentJumpCount = 0;
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

        if (CurrentJumpCount == 0 && IsGrounded == false)
            CurrentJumpCount++;
        
        OnceJumpRayCheck = true;
        IsGrounded = false;

        CurrentJumpCount++;
    }

    public void Die(bool d)
    {
        IsDie = d;
        if(IsDie)
            mRigidbody.velocity = new Vector2(0, mRigidbody.velocity.y);
        MAnim.Play(IsDie ? "Die" :"Idle");
        StartCoroutine(Delay(2f, ()=>loader.ReloadScene()));
    }

    private IEnumerator Delay(float sec, UnityAction action)
    {
        yield return new WaitForSeconds(sec);
        action();
    }

    protected void DownJump()
    {
        if (!IsGrounded)
            return;

        if (!isDownJumpGroundCheck)
        {
            MAnim.Play("Jump");

            mRigidbody.AddForce(-Vector2.up * 10);
            IsGrounded = false;

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
        if (!OnceJumpRayCheck)
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
                if (IsGrounded)
                {
                    LandingEvent();
                    OnceJumpRayCheck = false;
                }
            }
            _pretmpY = transform.position.y;
        }
    }
    
    protected abstract void LandingEvent();
}
