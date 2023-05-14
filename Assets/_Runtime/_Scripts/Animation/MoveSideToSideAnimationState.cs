using UnityEngine;

public struct ObstacleMovementAnimationsConstants
{
    public const string SideToSideMultiplier = "SideToSideMultiplier";
    public const string DeadTrigger = "DeadTrigger"; 
}

public class MoveSideToSideAnimationState : StateMachineBehaviour
{

    private ObstacleMoving obstacle;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(layerIndex);

        if(clips.Length > 0)
        {
            AnimatorClipInfo runClipInfo = clips[0];
            obstacle = animator.transform.parent.parent.parent.GetComponent<ObstacleMoving>();

            float timeToCompleteAnimationCycle = obstacle.sideToSideMoveTime * 2;
            float speedMultiplier = runClipInfo.clip.length / timeToCompleteAnimationCycle;

            animator.SetFloat(ObstacleMovementAnimationsConstants.SideToSideMultiplier, speedMultiplier);
        }

    }

}
