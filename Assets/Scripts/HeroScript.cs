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

    private int countHearts = 3;
    private bool isGround;
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
        IsDie(collision.collider);
    }

    void OnTriggerEnter2d(Collider2D collider)
    {
        IsDie(collider);
    }

    void IsDie(Collider2D collider)
    {
        if (collider.tag == "die")
        {
            transform.position = spawn;
            countHearts--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + Speed, transform.position.y, transform.position.z);
        if (isGround && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
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
