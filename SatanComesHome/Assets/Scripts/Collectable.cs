using UnityEngine;

public class Collectable : MonoBehaviour
{
	private float timeAdd = 10f;
	private float speedModifier = 0f;
	private int pointValue;
	[SerializeField]
	private string type;

    public PickupScriptableObject pickupObject;

	private bool beingCarried = false;
    public bool dropped = true;

	private new BoxCollider2D collider;
	private SpriteRenderer spriteRenderer;

    void Awake()
	{
		collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (pickupObject)
		{
			Init(pickupObject);
		}
	}

	public void Init(PickupScriptableObject pickupObject)
	{
		this.pickupObject = pickupObject;
		spriteRenderer.sprite = pickupObject.mySprite;
		timeAdd = pickupObject.TimeToAdd;
		speedModifier = pickupObject.SpeedModifier;
		pointValue = pickupObject.PointValue;
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
