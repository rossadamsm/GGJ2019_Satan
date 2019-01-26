using UnityEngine;

public class Collectable : MonoBehaviour
{
	private float timeAdd = 10f;
	private float speedModifier = 0f;
	private int pointValue;
	private string type;

    public PickupScriptableObject pickupObject;

	[HideInInspector] public bool beingCarried = false;
    [HideInInspector] public bool dropped = true;

	private new BoxCollider2D collider;
	private SpriteRenderer spriteRenderer;
	private Animator animator;


    public cakeslice.Outline highlight;

    void Awake()
	{
		collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        highlight = GetComponent<cakeslice.Outline>();
        animator = GetComponent<Animator>();

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
        type = pickupObject.Type;

        if (pickupObject.animationOverrideController != null)
        {
            animator.runtimeAnimatorController = pickupObject.animationOverrideController;
        }
        highlight.enabled = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        highlight.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        highlight.enabled = false;
    }

    public void Pickup(Transform anchor)
    {
		if (SoundManager.instance != null)
		{
			SoundManager.instance.PlayDropSound();
		}
        beingCarried = true;
        transform.SetParent(anchor.transform);
        transform.position = anchor.position;
    }

    public void Drop()
    {
        beingCarried = false;
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

	public void PlaceInHell()
	{
		Debug.Log("Place in hell");
		if (pickupObject.Type == GameMaster.instance.satanTaskManager.CurrentTask)
		{
			GameMaster.instance.ChangeTimer(pickupObject.TimeToAdd * 2);
			GameMaster.instance.ChangeScore(pickupObject.PointValue * 2);
		}
		else
		{
			GameMaster.instance.ChangeTimer(pickupObject.TimeToAdd);
			GameMaster.instance.ChangeScore(pickupObject.PointValue);
		}
		GameMaster.instance.collectables.Remove(this);
		collider.enabled = false;
		highlight.enabled = false;
	}
}
