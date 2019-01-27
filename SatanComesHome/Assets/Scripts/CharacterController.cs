using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject scareCrowPrefab;

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

    private bool haveAScareCrowToDrop = true;

    public GameObject exorcismprefab;

    // Start is called before the first frame update
    void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		anchor = GetComponentInChildren<Transform>();

		GameMaster.instance.SetNewTarget();
		GameMaster.instance.satanTaskManager.HideSpeechCloud();
        SoundManager.instance.musicSource.Stop();
        SoundManager.instance.PlayLoop(hell);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "Priest")
		{
			if (currentPickedupCollectable != null)
			{
				currentPickedupCollectable.Drop();
				speed = 100;
			}
			transform.position = hellTeleportPoint.position;
            //Instantiate(exorcismprefab);
            //exorcismprefab.GetComponent<Animator>().StopPlayback();
            //exorcismprefab.GetComponent<Animator>().StartPlayback();
            exorcismprefab.transform.localPosition = new Vector3(0, 17.4f, 0);
            exorcismprefab.GetComponent<Animator>().Play("Exorcism", -1, 0);
            SoundManager.instance.PlayDeathSound();
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
					GameMaster.instance.satanTaskManager.HideSpeechCloud();
					currentPickedupCollectable.PlaceInHell();
					GameMaster.instance.SetNewTarget();
				}
				currentPickedupCollectable.gameObject.transform.localScale = new Vector3(1, 1, 1);
				currentPickedupCollectable = null;
				speed = 100;
                SoundManager.instance.PlayDropSoundsingle();
			}
			else if (currentTouchedCollectable != null)
			{
				currentTouchedCollectable.Pickup(anchor);
				currentPickedupCollectable = currentTouchedCollectable;
				PickupScriptableObject CurrObject = currentPickedupCollectable.pickupObject;
				speed *= currentPickedupCollectable.speedModifier;
			}
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			GameMaster.instance.satanTaskManager.RemindWantedItem();
		}

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (scareCrowPrefab != null && haveAScareCrowToDrop)
            {
                Instantiate(scareCrowPrefab, transform.position, Quaternion.identity);
                haveAScareCrowToDrop = false;
                Invoke("ResetScareCrow", 10);
            }
        }
	}

    private void ResetScareCrow()
    {
        haveAScareCrowToDrop = true;
    }
}
