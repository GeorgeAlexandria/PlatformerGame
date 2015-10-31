using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour
{
    #region PublicVariables
    public float Speed = 0.04f;
    public float JumpForce = 13f;
    public LayerMask whatIsGround;
    public Collider2D GroundCheck;

    public Collider2D[] RunColliders;
    public Collider2D[] JumpColliders;
    public Collider2D[] FallColliders;
    #endregion

    #region Events
    public delegate void ChangeCountHeartsEventHandler(int countHearts);
    public event ChangeCountHeartsEventHandler ChangeCountHeartsRequest;

    public delegate void FinishLevelEventHandler();
    public event FinishLevelEventHandler FinishLevel;

    public delegate void AwakeEventHandler();
    public event AwakeEventHandler AwakeRequest;

    public delegate void DiedEventHandler();
    public event DiedEventHandler DiedRequest;

    public delegate void LoadEventHandler();
    public event LoadEventHandler LoadRequest;
    #endregion

    #region PrivateVariables
    private Rigidbody2D rigidBody;
    private Animator animator;
    private int innerCountHearts = 3;
    private int countHearts
    {
        get { return innerCountHearts; }
        set
        {
            innerCountHearts = value;
            ChangeCountHeartsRequest(innerCountHearts);
            if (innerCountHearts == 0) DiedRequest();
        }
    }
    private const float groundRadius = 0.2f;
    #endregion

    #region State
    private const string run = "Run";
    private const string jump = "Jump";
    private const string fall = "Fall";

    private bool isGrounded = true;
    private bool isPreviouslyGrounded;
    private bool isJump;
    private bool isJumping;

    private InnerState state;
    #endregion

    #region CollisionDetectionVariables
    private Vector2 oldPosition;
    private Vector2 newPosition;
    private int count;
    private float epsilon = 0.001f;

    private float normalGravity;
    private float extendedGravity = 20f;
    private int countRepeat = 10;
    #endregion

    private struct InnerState
    {
        public readonly Vector3 position;
        public readonly int countHearts;
        public readonly int lastLevel;

        public InnerState(Vector3 position, int countHearts, int lastLevel = 0)
        {
            this.position = position;
            this.countHearts = countHearts;
            this.lastLevel = lastLevel;
        }
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        normalGravity = rigidBody.gravityScale;
        state = new InnerState(rigidBody.position, countHearts);
    }

    // Use this for initialization
    void Start()
    {
        //gameObject.SetActive(false);
        //AwakeRequest();

        //rigidBody = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        //normalGravity = rigidBody.gravityScale;
        //state = new InnerState(rigidBody.position, countHearts);
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
            ActivateCollider(fall);
            animator.CrossFade(fall, 0f);
        }
        else if (isJumping)
        {
            ActivateCollider(jump);
            animator.CrossFade(jump, 0f);
        }
        else if (isGrounded)
        {
            ActivateCollider(run);
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

    void ActivateCollider(string state)
    {
        Func<Collider2D[], bool, bool> ISEnabled = (array, arg) =>
        {
            foreach (var item in array) item.enabled = arg;
            return true;
        };

        ISEnabled(RunColliders, false);
        ISEnabled(JumpColliders, false);
        ISEnabled(FallColliders, false);

        switch (state)
        {
            case run:
                ISEnabled(RunColliders, true);
                break;
            case jump:
                ISEnabled(JumpColliders, true);
                break;
            default:
                ISEnabled(FallColliders, true);
                break;
        }
    }

    //Need modify
    void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded();
        if (collision.collider.tag == "Die")
        {
            Died();
        }
        else if (collision.collider.tag == "Finish")
        {
            FinishLevel();
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
        rigidBody.transform.position = Vector3.Lerp(rigidBody.position, state.position, 1f);
        countHearts--;
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0) return;
        //gameObject.SetActive(true);

        //LoadRequest();
        //gameObject.SetActive(true);
        if (level == state.lastLevel)
        {
            rigidBody.transform.position = Vector3.Lerp(rigidBody.position, state.position, 1f);
            countHearts = state.countHearts;
            return;
        }
        rigidBody.transform.position = Vector3.Lerp(rigidBody.position, GameObject.Find("HeroPosition").transform.position, 1f);
        //Only for invoke event
        countHearts = countHearts;
        state = new InnerState(rigidBody.position, countHearts, level);
    }
}
