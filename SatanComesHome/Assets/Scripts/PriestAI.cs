using System.Collections.Generic;
using UnityEngine;

public enum PriestState
{
    Idle,
    Roam,
    Chase
}

public class PriestAI : MonoBehaviour
{
    public PriestState myState;
    public float speed = 1;
    public ParticleSystem[] foundPlayerParticleSystems;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 Move, roamDirection = new Vector2(1,0);
    private Transform player;
    private PriestState previousState;

    public List<Scarecrow> scarecrows = new List<Scarecrow>();

    Scarecrow scareCrowToChase;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        player = FindObjectOfType<CharacterController>().transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        roamDirection = collision.relativeVelocity;
    }

    private void Update()
    {
        if (scarecrows.Count > 0)
        {
            foreach (var scarecrow in scarecrows)
            {
                if (Vector2.Distance(player.transform.position, scarecrow.transform.position) < 50)
                {
                    scareCrowToChase = scarecrow;
                    myState = PriestState.Chase;
                }
            }

            if (scareCrowToChase == null)
            {
                if (Vector2.Distance(player.transform.position, transform.position) < 50)
                    myState = PriestState.Chase;
                else
                    myState = PriestState.Roam;
            }
        }
        else
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 50)
                myState = PriestState.Chase;
            else
                myState = PriestState.Roam;

            scareCrowToChase = null;
        }

        switch (myState)
        {
            case PriestState.Idle:
                Idle();
                break;
            case PriestState.Roam:
                Roam();
                break;
            case PriestState.Chase:
                Chase();
                break;
            default:
                break;
        }

        if (Move.Equals(Vector2.zero))
            animator.SetBool("walk", false);
        animator.SetBool("walk", true);

        if (Move.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        rb.velocity = Move * speed;
    }

    private void Chase()
    {
        if (previousState != PriestState.Chase)
        {
            SoundManager.instance.PlayPriestSound();
            if (foundPlayerParticleSystems != null)
            {
                foreach (var particleSystem in foundPlayerParticleSystems)
                    particleSystem.Play();
            }
        }

        Vector2 direction;

        if (scareCrowToChase!=null)
            direction = scareCrowToChase.transform.position - transform.position;
        else
            direction = player.position - transform.position;

        direction.Normalize();
        Move = direction;

        previousState = PriestState.Chase;
    }

    private void Idle()
    {
        Move = new Vector2(0, 0);
        previousState = PriestState.Idle;
    }

    private void Roam()
    {
        //Move = new Vector2(UnityEngine.Random.Range(-1f,1f), Random.Range(-1f, 1f));
        Move = roamDirection;
        Move.Normalize();
        previousState = PriestState.Roam;
    }
}
