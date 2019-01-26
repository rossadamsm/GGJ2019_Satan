﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	[SerializeField]
	private float timeAdd = 10f;
	[SerializeField]
	private float speedModifier = 0f;
	[SerializeField]
	private int pointValue;
	[SerializeField]
	private string type;

	private bool beingCarried = false;
    public bool dropped = true;

	new BoxCollider2D collider;

	void Start()
	{
		collider = GetComponent<BoxCollider2D>();
	}

    public void Pickup(Transform anchor)
    {
        transform.SetParent(anchor.transform);
        transform.position = anchor.position;
    }

    public void Drop()
    {
        transform.SetParent(null);
    }

	public void Interact(Transform transform)
	{
		if (beingCarried)
		{
			beingCarried = false;
			collider.enabled = true;
			transform.SetParent(null);
		}
		else
		{
			beingCarried = true;
			collider.enabled = false;
			transform.parent = transform;
		}

        Debug.Log(beingCarried);

	}
}
