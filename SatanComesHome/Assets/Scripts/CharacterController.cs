using System;
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

    private Collectable currentPickedupCollectable;
    private Collectable currentTouchedCollectable;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anchor = GetComponentInChildren<Transform>();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.transform.tag == "Collectable")
    //    {
    //        Collectable c = collision.collider.GetComponent<Collectable>();
    //        if (c.dropped)
    //            c.Pickup(anchor);
    //        c.dropped = false;

    //        Debug.Log("Picked up object" + gameObject.name);
    //    }
    //    go = collision.gameObject;

    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.collider.transform.tag == "Collectable")
    //    {
    //        Collectable c = collision.collider.GetComponent<Collectable>();
    //        c.dropped = true;

    //        c.Drop();

    //        Debug.Log("Dropped object" + gameObject.name);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collectable newCollectable = collision.GetComponent<Collectable>();

        if (newCollectable != null)
        {
            if (currentPickedupCollectable != null)
                return;
            else
            {
                currentTouchedCollectable = newCollectable;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Collectable newCollectable = collision.GetComponent<Collectable>();

        if (newCollectable == currentTouchedCollectable)
        {
            currentTouchedCollectable = null;
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

        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentPickedupCollectable != null)
            {
                currentPickedupCollectable.Drop();
                currentPickedupCollectable = null;
                speed = 100;
            }
            else if (currentTouchedCollectable != null)
            {
                currentTouchedCollectable.Pickup(anchor);
                currentPickedupCollectable = currentTouchedCollectable;
                PickupScriptableObject CurrObject =  currentPickedupCollectable.pickupObject;
                speed *= CurrObject.SpeedModifier;
            }
        }
    }
}
