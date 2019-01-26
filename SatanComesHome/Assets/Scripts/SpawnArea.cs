using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
	[SerializeField]
	Vector2 areaBounds;

	public Vector2 GetPositionWithinArea()
	{
		return (new Vector2(Random.Range(transform.position.x - areaBounds.x / 2, transform.position.x + areaBounds.x / 2), Random.Range(transform.position.y - areaBounds.y / 2, transform.position.y + areaBounds.y / 2)));
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(areaBounds.x, areaBounds.y));
	}
}
