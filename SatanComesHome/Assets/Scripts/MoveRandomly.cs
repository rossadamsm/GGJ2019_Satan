using UnityEngine;

public class MoveRandomly : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 direction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("ChangeDirection", 0, 3);
    }

    private void ChangeDirection()
    {
        direction = UnityEngine.Random.insideUnitCircle;
        direction.Normalize();
    }

    void Update()
    {
        rb.velocity = speed * direction;
    }
}
