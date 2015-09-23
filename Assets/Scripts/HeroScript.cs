using UnityEngine;
using System.Collections;

public class HeroScript : MonoBehaviour
{
    public float Speed = 0.04f;
    public float JumpForce = 13f;
    public Texture Heart;
    public LayerMask whatIsGround;
    public Transform GroundCheck;
    public float Scaleheart = 0.3f;

    private Vector3 spawn;
    private readonly Vector2 positionHearts = new Vector2(5, 5);
    private Rigidbody2D rigidBody;
    private Animator animator;
    //private Animation animation;

    private int countHearts = 3;
    private bool isGround;
    private bool isJump;
    private float groundRadius = 0.02f;

    // Use this for initialization
    void Start()
    {
        spawn = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(GroundCheck.position, groundRadius, whatIsGround);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isJump = false;
        }
        IsDie(collision.collider);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Heart")
        {
            Destroy(collider.gameObject);
            countHearts += countHearts < 3 ? 1 : 0;
            return;
        }
        IsDie(collider);
    }

    void IsDie(Collider2D collider)
    {
        if (collider.tag == "Die")
        {
            transform.position = spawn;
            countHearts--;
            isGround = true;
            isJump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + Speed, transform.position.y, transform.position.z);
        if (!isGround && !isJump)
        {
            animator.CrossFade("Fall", 0f);
            return;
        }
        if (isGround && Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJump = true;
            isGround = false;
            rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            animator.CrossFade("Jump", 0f);
            return;
        }
        if (isGround && !isJump)
        {
            animator.CrossFade("Run", 0f);
        }
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
