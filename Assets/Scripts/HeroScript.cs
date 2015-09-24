using UnityEngine;
using System.Collections;

public class HeroScript : MonoBehaviour
{
    public float Speed = 0.04f;
    public float JumpForce = 13f;
    public Texture Heart;
    public LayerMask whatIsGround;
    public Collider2D GroundCheck;
    public float Scaleheart = 0.3f;
    public PolygonCollider2D RunCollider;
    public PolygonCollider2D JumpCollider;
    public PolygonCollider2D FallCollider;

    private Vector3 spawn;
    private readonly Vector2 positionHearts = new Vector2(5, 5);
    private Rigidbody2D rigidBody;
    private Animator animator;
    //private int defaultLayer;

    private int countHearts = 3;
    private const float groundRadius = 0.2f;

    private bool isGround = true;
    private bool isJump;

    private const string run = "Run";
    private const string jump = "Jump";
    private const string fall = "Fall";

    private Vector2 oldPosition;
    private Vector2 newPosition;

    // Use this for initialization
    void Start()
    {
        //oldPosition = rigidBody.position;
        //newPosition = rigidBody.position - new Vector2(1, 1);
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        spawn = rigidBody.position;

        //defaultLayer = LayerMask.GetMask("Default");
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(GroundCheck.transform.position, groundRadius, whatIsGround);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && Physics2D.IsTouching(collision.collider, GroundCheck) ||
            Physics2D.OverlapCircle(GroundCheck.transform.position, groundRadius, whatIsGround))
        {
            Grounded();
        }
        if (collision.collider.tag == "Die")
        {
            Died();
        }
    }

    //Надо подправить
    void Died()
    {
        transform.position = spawn;
        countHearts--;
        Grounded();
    }

    void Jumped()
    {
        isJump = true;
        isGround = false;
    }

    void Grounded()
    {
        isJump = false;
        isGround = true;
    }

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

    // Update is called once per frame
    void Update()
    {
        oldPosition = rigidBody.position;
        if (!isGround && !isJump)
        {
            ActivateCollider(fall);
            animator.CrossFade(fall, 0f);
            //State of infinity faling
            //But it's not work
            if (Mathf.Abs(newPosition.x - oldPosition.x) < 0.00001 && Mathf.Abs(newPosition.y - oldPosition.y) < 0.00001)
            {
                rigidBody.AddForce(new Vector2(0.1f, 0.1f), ForceMode2D.Impulse);
            }
        }
        else if (isGround && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jumped();
            rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            ActivateCollider(jump);
            animator.CrossFade(jump, 0f);
            //return;
        }
        else if (isGround && !isJump)
        {
            ActivateCollider(run);
            animator.CrossFade(run, 0f);
        }
        //State of infinity jumping
        else if (!isGround && isJump &&
            Mathf.Abs(newPosition.x - oldPosition.x) < 0.00001 && Mathf.Abs(newPosition.y - oldPosition.y) < 0.00001)
        {
            rigidBody.AddForce(new Vector2(0.1f, 0.1f), ForceMode2D.Impulse);
        }
        rigidBody.velocity = new Vector2(Speed, rigidBody.velocity.y);
        newPosition = rigidBody.position;
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
