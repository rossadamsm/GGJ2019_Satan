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

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 Move, roamDirection = new Vector2(1,0);
    private Transform player;

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
        Vector2 direction = player.position - transform.position;
        direction.Normalize();
        Move = direction;
    }

    private void Idle()
    {
        Move = new Vector2(0, 0);
    }

    private void Roam()
    {
        //Move = new Vector2(UnityEngine.Random.Range(-1f,1f), Random.Range(-1f, 1f));
        Move = roamDirection;
        Move.Normalize();
    }
}
