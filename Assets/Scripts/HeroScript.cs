using UnityEngine;
using System.Collections;

public class HeroScript : MonoBehaviour
{
    public float Speed = 0.02f;
    public float JumpForce = 1500f;
    private Vector3 spawn;

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
        if (collision.collider.tag == "die")
        {
            transform.position = spawn;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
