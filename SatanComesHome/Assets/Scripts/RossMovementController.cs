using UnityEngine;

public class RossMovementController : MonoBehaviour
{
    public KeyCode walkKey = KeyCode.W;

    public Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(walkKey))
        {
            myAnimator.SetBool("walk", true);
        }
        else
        {
            myAnimator.SetBool("walk", false);
        }
    }
}
