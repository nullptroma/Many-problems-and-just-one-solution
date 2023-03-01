using UnityEngine;

public class Magnetman : PlayerController
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
    }

    private void CheckFlip()
    {
        Flip(MouseToTheLeft());
    }
    
    private void FixedUpdate()
    {
        if (IsSit || IsDie)
            MMoveX = 0;
        var velocity = mRigidbody.velocity;
        if (MMoveX != 0)
            velocity.x = IsGrounded ? Mathf.MoveTowards(velocity.x, moveSpeed * MMoveX, walkAcceleration*Time.deltaTime) : Mathf.MoveTowards(velocity.x, moveSpeed * MMoveX, airAcceleration*Time.deltaTime);
        else if(IsGrounded)
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration*Time.deltaTime);
        else
            velocity.x = Mathf.MoveTowards(velocity.x, 0, airDeceleration*Time.deltaTime);
        mRigidbody.velocity = velocity;
    }

    private void SetMagnetMode(Magnet.MagnetMode mode)
    {
        if(magnet.Mode != mode)
            flipper.Flip();
        magnet.SetMode(mode);
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.R) && loader != null)
            loader.ReloadScene();
        if (Input.GetKeyDown(KeyCode.Escape) && loader != null)
            loader.LoadMenu();
        if (Input.GetKeyDown(KeyCode.S))
        {
            IsSit = true;
            MAnim.Play("Sit");
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            MAnim.Play("Idle");
            IsSit = false;
        }

        
        if (IsDie)
        {
            MMoveX = 0;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetMagnetMode(Magnet.MagnetMode.Attraction);
            magnet.SetEnableMagnet(true);
        } 
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SetMagnetMode(Magnet.MagnetMode.Repulsion);
            magnet.SetEnableMagnet(true);
        }     
        else if (Input.GetKeyUp(KeyCode.Mouse0))//лкм поднята
        {
            bool rmb = Input.GetKey(KeyCode.Mouse1);
            if(rmb)
                SetMagnetMode(Magnet.MagnetMode.Repulsion);
            magnet.SetEnableMagnet(rmb);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))//пкм поднята
        {
            bool lmb = Input.GetKey(KeyCode.Mouse0);
            if(lmb)
                SetMagnetMode(Magnet.MagnetMode.Attraction);
            magnet.SetEnableMagnet(lmb);
        }
        if (IsSit)
        {
            MMoveX = 0;
            if (Input.GetKeyDown(KeyCode.Space))
                if (CurrentJumpCount < jumpCount)
                    DownJump();
            return;
        }
        else
            MMoveX = Input.GetAxis("Horizontal");

        GroundCheckUpdate();

        MAnim.Play(MMoveX == 0 ? "Idle" : "Run");

        if (IsGrounded == false)
            MAnim.SetFloat($"RunSpeed", 1);
        else if (Input.GetKey(KeyCode.D))
            MAnim.SetFloat($"RunSpeed", CalcSpeedOfRunAnim() * (IsLeft ? -1 : 1));
        else if (Input.GetKey(KeyCode.A))
            MAnim.SetFloat($"RunSpeed", CalcSpeedOfRunAnim() * (IsLeft ? 1 : -1));
        else
            MAnim.SetFloat($"RunSpeed", CalcSpeedOfRunAnim());

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentJumpCount < jumpCount) // 0 , 1
            {
                if (!IsSit)
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