﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField]
	private Portal otherPortal;
	private bool active = true;

	[SerializeField]
	private Transform[] portalTeleportSpots = null;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.tag == "Player" && active)
		{
			otherPortal.active = false;
			collision.transform.position = otherPortal.transform.position;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.transform.tag == "Player")
		{
			active = true;
			if (portalTeleportSpots != null)
			{
				transform.position = portalTeleportSpots[Random.Range(0, portalTeleportSpots.Length)].position;
			}
		}
	}

	private void OnDrawGizmos()
	{
		
	}
}
