using UnityEngine;

public class Collectable : MonoBehaviour
{
	public float timeAdd = 10f;
	public float speedModifier = 0f;
	public int pointValue;
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
		switch (pickupObject.weightCategory)
		{
			case WeightCategory.SuperHeavy:
				timeAdd = 10;
				speedModifier = 0.5f;
				pointValue = 40;
				break;
			case WeightCategory.Heavy:
				timeAdd = 8;
				speedModifier = 0.6f;
				pointValue = 30;
				break;
			case WeightCategory.Medium:
				timeAdd = 6;
				speedModifier = 0.75f;
				pointValue = 20;
				break;
			case WeightCategory.Light:
				timeAdd = 4;
				speedModifier = 0.9f;
				pointValue = 10;
				break;
			case WeightCategory.VeryLight:
				timeAdd = 2;
				speedModifier = 1f;
				pointValue = 5;
				break;
		}
		
        type = pickupObject.Type;

        if (pickupObject.animationOverrideController != null)
        {
            animator.runtimeAnimatorController = pickupObject.animationOverrideController;
        }
        highlight.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.transform.tag == "Player")
		{
			highlight.enabled = true;
		}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.transform.tag == "Player")
		{
			highlight.enabled = false;
		}
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

        FindObjectOfType<ShakeManager>().ShakeCamera();
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
			GameMaster.instance.ShowScoreMultiplier();
			GameMaster.instance.ChangeTimer(timeAdd * 2);
			GameMaster.instance.ChangeScore(pointValue * 2);
		}
		else
		{
			GameMaster.instance.ChangeTimer(timeAdd);
			GameMaster.instance.ChangeScore(pointValue);
		}
		GameMaster.instance.collectables.Remove(this);
		collider.enabled = false;
		highlight.enabled = false;
	}
}
