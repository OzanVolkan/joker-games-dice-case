using PlayerControl;
using UnityEngine;

namespace Managers
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        private readonly string _forwardAnimTrigger = "Forward";
        private readonly string _mapEndAnimTrigger = "EndMap";
        private readonly string _mapEnterAnimTrigger = "EnterMap";

        private void OnEnable()
        {
            PlayerControl.Movement.Movement.OnForwardMovement += ForwardAnimation;
            PlayerController.OnEndMap += EndMapAnimation;
            PlayerController.OnEnterMap += EnterMapAnimation;
        }

        private void OnDisable()
        {
            PlayerControl.Movement.Movement.OnForwardMovement -= ForwardAnimation;
            PlayerController.OnEndMap -= EndMapAnimation;
            PlayerController.OnEnterMap -= EnterMapAnimation;
        }


        private void ForwardAnimation(Animator animator)
        {
            animator.SetTrigger(_forwardAnimTrigger);
        }

        private void EndMapAnimation(Animator animator)
        {
            animator.SetTrigger(_mapEndAnimTrigger);
        }

        private void EnterMapAnimation(Animator animator)
        {
            animator.SetTrigger(_mapEnterAnimTrigger);
        }
    }
}
