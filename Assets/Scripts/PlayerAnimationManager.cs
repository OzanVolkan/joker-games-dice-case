using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private readonly string _forwardAnimTrigger = "Forward";

    private void OnEnable()
    {
        Movement.Movement.OnForwardMovement += ForwardAnimation;
    }

    private void OnDisable()
    {
        Movement.Movement.OnForwardMovement -= ForwardAnimation;
    }


    private void ForwardAnimation(Animator animator)
    {
        print("girdi");
        animator.SetTrigger(_forwardAnimTrigger);
    }
}
