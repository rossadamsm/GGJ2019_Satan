using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed;
    private GameObject go;
    //[SerializeField]
    private Transform anchor;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anchor = GetComponentInChildren<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.tag == "Collectable")
        {
            Collectable c = collision.collider.GetComponent<Collectable>();
            if (c.dropped)
                c.Pickup(anchor);
            c.dropped = false;
        }
        go = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.transform.tag == "Collectable")
        {
            Collectable c = collision.collider.GetComponent<Collectable>();
            c.dropped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Move.Normalize();

        if (Move.Equals(Vector2.zero))
            animator.SetBool("walk", false);
        else
            animator.SetBool("walk", true);

        if (Move.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        rb.velocity = Move * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collectable obj = go.GetComponent<Collectable>();
            obj.Drop();
        }

        //if (Move.x < 0)
        //    spriteRenderer.flipX = true;
        //else if (Move.x > 0)
        //    spriteRenderer.flipX = false;
    }
}
