using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField]
	private Portal otherPortal;
	private bool active = true;
	[SerializeField]
	private bool teleport = false;
	[SerializeField]
	public GameObject portalAnimation;

	[SerializeField]
	private Transform[] portalTeleportSpots = null;

    public Collider2D myCollider;

    private Coroutine enableRoutine;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.tag == "Player" && active)
		{
			otherPortal.active = false;
			otherPortal.transform.localScale = Vector3.zero; //TODO: Maybe shrink later
            otherPortal.portalAnimation.SetActive(false);
			collision.transform.position = otherPortal.transform.position;
			if (teleport)
			{
				Teleport();
			}
		}
		if (collision.transform.tag == "Priest")
		{
			if (teleport)
			{
				Teleport();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.transform.tag == "Player")
		{
            if (enableRoutine != null)
                StopCoroutine(enableRoutine);

            enableRoutine = StartCoroutine(EnablePortal());
		}
	}

	public void Teleport()
	{
        if(myCollider.enabled)
		    transform.position = portalTeleportSpots[Random.Range(0, portalTeleportSpots.Length)].position;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, 50);
	}

    float currentTime = 0;

    private IEnumerator EnablePortal()
	{
        active = false;
        otherPortal.active = false;

        //yield return (new WaitForSeconds(0.35f));
        portalAnimation.SetActive(true);
        otherPortal.portalAnimation.SetActive(true);

        currentTime = 0;
        otherPortal.myCollider.enabled = false;
        myCollider.enabled = false;

        while (currentTime < 1.25f)
        {
            currentTime += Time.deltaTime;
            otherPortal.transform.localScale = Vector3.one * currentTime;
            yield return null;
        }

        while (currentTime > 1)
        {
            currentTime -= Time.deltaTime;
            otherPortal.transform.localScale = Vector3.one * currentTime;
            yield return null;
        }

        otherPortal.transform.localScale = Vector3.one;
        otherPortal.myCollider.enabled = true;
        myCollider.enabled = true;
        active = true;
        otherPortal.active = true;

    }
}
