﻿using System;
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
	public SpriteRenderer spriteRenderer;

	private Collectable currentPickedupCollectable;
	private Collectable currentTouchedCollectable;

	private Collectable newCollectable;

	private float speedModifier = 1;

	public bool inHell = false;
	[SerializeField]
	PostPManager ppManager;
	[SerializeField]
	Transform hellTeleportPoint;

    public AudioClip hell;
    public AudioClip sneak;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		anchor = GetComponentInChildren<Transform>();

		GameMaster.instance.SetNewTarget();
        SoundManager.instance.PlayLoop(hell);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "Priest")
		{
			if (currentPickedupCollectable != null)
			{
				currentPickedupCollectable.Drop();
			}
			transform.position = hellTeleportPoint.position;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		newCollectable = collision.GetComponent<Collectable>();

		if (newCollectable != null)
		{
			if (currentPickedupCollectable != null)
				return;
			else
			{
				currentTouchedCollectable = newCollectable;
			}
		}

		if (collision.transform.tag == "Hell")
		{
			inHell = true;
			Camera.main.GetComponent<PostPManager>().gotoHell();
            SoundManager.instance.PlayLoop(hell);

			Debug.Log("In hell");
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		newCollectable = collision.GetComponent<Collectable>();

		if (newCollectable == currentTouchedCollectable)
		{
			currentTouchedCollectable = null;
		}

		if (collision.transform.tag == "Hell")
		{
			inHell = false;
			Camera.main.GetComponent<PostPManager>().gotoEarth();
            SoundManager.instance.PlayLoop(sneak);
			Debug.Log("Left hell");
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
		else if (Move.x > 0)
			transform.localScale = new Vector3(1, 1, 1);

		//rb.velocity = Move * speed * speedModifier;
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
				if (inHell)
				{
					currentPickedupCollectable.PlaceInHell();
					GameMaster.instance.SetNewTarget();
				}
				currentPickedupCollectable.gameObject.transform.localScale = new Vector3(1, 1, 1);
				currentPickedupCollectable = null;
				speed = 100;
			}
			else if (currentTouchedCollectable != null)
			{
				currentTouchedCollectable.Pickup(anchor);
				currentPickedupCollectable = currentTouchedCollectable;
				PickupScriptableObject CurrObject = currentPickedupCollectable.pickupObject;
				speed *= CurrObject.SpeedModifier;
			}
		}
	}
}
