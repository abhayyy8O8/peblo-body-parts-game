using UnityEngine;

public class MastiAnimationController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayHappy()
    {
        animator.SetTrigger("Happy");
    }

    public void PlayThink()
    {
        animator.SetTrigger("Think");
    }
}
