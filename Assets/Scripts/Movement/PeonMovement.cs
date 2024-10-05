using System.Collections;
using Interfaces;
using UnityEngine;

namespace Movement
{
    public sealed class PeonMovement : Movement, IJumpable
    {
        public void Move(Transform playerTrans, float height, float time, Animator animator)
        {
            if (_isMoving) return;

            base.Move(animator);

            StartCoroutine(JumpCoroutine(playerTrans, height, time));
        }

        public IEnumerator JumpCoroutine(Transform playerTrans, float height, float time)
        {
            _isMoving = true;
            float elapsedTime = 0;
            Vector3 startPosition = transform.position;
            Vector3 target = playerTrans.position + _movingOffset;

            while (elapsedTime < time)
            {
                float t = elapsedTime / time;

                float easeT = EaseType(t);

                Vector3 currentPosition = Vector3.Lerp(startPosition, target, easeT);

                float verticalOffset = Mathf.Sin(easeT * Mathf.PI) * height;
                currentPosition.y += verticalOffset;

                transform.position = currentPosition;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = target;
            _isMoving = false;
        }

        //EaseInOutQuad
        protected override float EaseType(float t)
        {
            if (t < 0.5f) return 2 * t * t;
            return -1 + (4 - 2 * t) * t;
        }
    }
}