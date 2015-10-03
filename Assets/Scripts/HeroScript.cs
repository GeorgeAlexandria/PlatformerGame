using UnityEngine;
using System.Collections;
using System;

public class HeroScript : MonoBehaviour
{
    #region PublicVariables
    public float Speed = 0.04f;
    public float JumpForce = 13f;
    public Texture Heart;
    public LayerMask whatIsGround;
    public Collider2D GroundCheck;
    public float Scaleheart = 0.3f;

    public PolygonCollider2D RunCollider;
    public PolygonCollider2D JumpCollider;
    public PolygonCollider2D FallCollider;
    #endregion

    #region PrivateVariables
    private Vector3 spawn;
    private readonly Vector2 positionHearts = new Vector2(5, 5);
    private Rigidbody2D rigidBody;
    private Animator animator;
    private int countHearts = 3;
    private const float groundRadius = 0.2f;
    #endregion

    #region State
    private bool isGrounded = true;
    private bool isPreviouslyGrounded;
    private bool isJump;
    private bool isJumping;
    #endregion

    #region NowNotUse
    private const string run = "Run";
    private const string jump = "Jump";
    private const string fall = "Fall";
    #endregion

    #region CollisionDetectionVariables
    private Vector2 oldPosition;
    private Vector2 newPosition;
    private int count;
    private float epsilon = 0.001f;

    private float normalGravity = 2f;
    private float extendedGravity = 20f;
    private int countRepeat = 10;
    #endregion

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawn = rigidBody.position;
    }

    bool CheckCount(int arg)
    {
        if (count++ >= arg)
        {
            count = 0;
            return true;
        }
        return false;
    }

    void FixedUpdate()
    {
        newPosition = rigidBody.position;
        rigidBody.gravityScale = normalGravity;

        IsGrounded();
        if (isGrounded)
        {
            if (isJump)
            {
                rigidBody.AddForce(new Vector2(Speed, JumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
            else if (isPreviouslyGrounded && isJumping)
            {
                //Just jump, but caught in infinity jumping
                count = Mathf.Abs(oldPosition.x - newPosition.x) < epsilon && Mathf.Abs(oldPosition.y - newPosition.y) < epsilon ? count + 1 : 0;
                rigidBody.gravityScale = CheckCount(countRepeat) ? extendedGravity : rigidBody.gravityScale;
            }
        }
        else
        {
            if (!isPreviouslyGrounded)
            {
                //Infinity falling if !isJumping else infinity jumping
                count = Mathf.Abs(oldPosition.x - newPosition.x) < epsilon && Mathf.Abs(oldPosition.y - newPosition.y) < epsilon ? count + 1 : 0;
                rigidBody.gravityScale = CheckCount(countRepeat) ? extendedGravity : rigidBody.gravityScale;
            }
        }
        rigidBody.velocity = new Vector2(Speed, rigidBody.velocity.y);

        isJump = false;
        oldPosition = rigidBody.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJump = true;
        }
        if (!isGrounded && !isJumping)
        {
            //ActivateCollider(fall);
            animator.CrossFade(fall, 0f);
        }
        else if (isJumping)
        {
            //ActivateCollider(jump);
            animator.CrossFade(jump, 0f);
        }
        else if (isGrounded)
        {
            //ActivateCollider(run);
            animator.CrossFade(run, 0f);
        }
    }

    private void IsGrounded()
    {
        isPreviouslyGrounded = isGrounded;
        isGrounded = false;

        //isGrounded = Physics2D.IsTouchingLayers(GroundCheck, whatIsGround);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.transform.position, groundRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                break;
            }
        }
        if (!isPreviouslyGrounded && isGrounded)
        {
            isJumping = false;
        }
    }

    #region NotUse
    void ActivateCollider(string arg)
    {
        RunCollider.enabled = false;
        JumpCollider.enabled = false;
        FallCollider.enabled = false;
        switch (arg)
        {
            case run:
                RunCollider.enabled = true;
                break;
            case jump:
                JumpCollider.enabled = true;
                break;
            default:
                FallCollider.enabled = true;
                break;
        }
    }
    private void StickToGroundHelper()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, groundRadius, Vector3.down, out hitInfo, 1f, whatIsGround))
        {
            if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
            {
                rigidBody.velocity = Vector3.ProjectOnPlane(rigidBody.velocity, hitInfo.normal);
            }
        }
    }
    #endregion

    //Need modify
    void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded();
        if (collision.collider.tag == "Die")
        {
            Died();
        }
    }

    //Need modify
    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Heart":
                Destroy(collider.gameObject);
                countHearts += countHearts < 3 ? 1 : 0;
                break;
            case "Die":
                Died();
                break;
            default:
                break;
        }
    }

    //Need modify
    void Died()
    {
        transform.position = spawn;
        countHearts--;
    }

    void OnGUI()
    {
        float height = Heart.height * Scaleheart;
        float width = Heart.width * Scaleheart;
        for (int i = 0; i < countHearts; i++)
        {
            GUI.Label(new Rect(positionHearts.x + i * height, positionHearts.y, height, width), Heart);
        }
    }
}
