using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
	[SerializeField]
	Vector2 areaBounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(areaBounds.x, areaBounds.y));
	}
}
