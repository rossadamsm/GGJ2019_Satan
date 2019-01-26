using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
	[SerializeField]
	Vector2 areaBounds;

	public Vector2 GetPositionWithinArea()
	{
		return (new Vector2(Random.Range(transform.position.x - (float)((areaBounds.x / 2) * 0.6), transform.position.x + (float)((areaBounds.x / 2) * 0.6)), Random.Range(transform.position.y - (float)((areaBounds.y / 2) * 0.6), transform.position.y + (float)((areaBounds.y / 2) * 0.6))));
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(areaBounds.x, areaBounds.y));
	}
}
