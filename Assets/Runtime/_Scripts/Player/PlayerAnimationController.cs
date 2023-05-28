using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        animator.SetBool(PlayerAnimationConstants.IsJumping, player.IsJumping);
        animator.SetBool(PlayerAnimationConstants.IsRolling, player.IsRolling);
    }

    public IEnumerator PlayStartGameAnimation()
    {
        FoxStartGameAnimation();

        while(!animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimationConstants.RunningAnimationStateName))
        {
            yield return null;
        }
        
    }

    private void FoxStartGameAnimation()
    {
        animator.SetTrigger(PlayerAnimationConstants.StartGameTrigger);
    }

    public void Die()
    {
        if(player.IsInvicible) return;
        animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
    }

}
