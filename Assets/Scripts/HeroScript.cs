using UnityEngine;
using System.Collections;

public class HeroScript : MonoBehaviour
{
    public float Speed = 0.02f;
    public float JumpForce = 1500f;
    public Texture Heart;
    public float Scaleheart = 0.3f;

    private Vector3 spawn;
    private readonly Vector2 positionHearts = new Vector2(5, 5);
    private int countHearts = 3;

    // Use this for initialization
    void Start()
    {
        spawn = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + Speed, transform.position.y, transform.position.z);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
        }
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
