using UnityEngine;

public class SpriteOrderer : MonoBehaviour
{
	private SpriteRenderer sr;
    private Collectable collectable;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
        collectable = GetComponent<Collectable>();
	}

	void Update()
	{
        if (collectable != null)
        {
            if (!collectable.beingCarried)
                sr.sortingOrder = -(int)(transform.position.y / 10);
            else
                sr.sortingOrder = FindObjectOfType<CharacterController>().spriteRenderer.sortingOrder + 5;
        }
        else
            sr.sortingOrder = -(int)(transform.position.y / 10);

    }
}
