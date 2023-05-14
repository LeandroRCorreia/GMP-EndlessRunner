using UnityEngine;

public class ObstacleMovingDecoration : ObstacleDecoration
{
    [SerializeField] private Animator animator;

    public override void PlayCollisionFeedBack()
    {
        base.PlayCollisionFeedBack();
        animator.SetTrigger(ObstacleMovementAnimationsConstants.DeadTrigger);

    }

}
