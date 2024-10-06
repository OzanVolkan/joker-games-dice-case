using System;
using System.Collections;
using UnityEngine;

namespace Movement
{
    public abstract class Movement : MonoBehaviour
    {
        public static event Action<Animator> OnForwardMovement;

        protected readonly Vector3 _movingOffset = new Vector3(0f, 0f, 8.15f);
        private float _verticalJumpHeight = 4f;
        protected bool _isMoving;

        protected abstract float EaseType(float t);

        protected virtual void Move(Animator animator)
        {
            OnForwardMovement?.Invoke(animator);
        }

        public void VerticalJump(Transform playerTrans, float time, bool isJumping)
        {
            StartCoroutine(VerticalJumpCoroutine(playerTrans, _verticalJumpHeight, time, isJumping));
        }

        protected IEnumerator VerticalJumpCoroutine(Transform playerTrans, float height, float time, bool isJumpingUp)
        {
            float elapsedTime = 0;
            Vector3 startPosition = transform.position;
    
            Vector3 target = isJumpingUp 
                ? playerTrans.position + Vector3.up * height
                : playerTrans.position - Vector3.up * height;

            while (elapsedTime < time)
            {
                float t = elapsedTime / time;

                float easeT = EaseType(t);

                Vector3 currentPosition = Vector3.Lerp(startPosition, target, easeT);

                transform.position = currentPosition;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = target;
        }
    }
}