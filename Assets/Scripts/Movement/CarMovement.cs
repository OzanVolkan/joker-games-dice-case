using System;
using System.Collections;
using Interfaces;
using UnityEngine;

namespace Movement
{
    public sealed class CarMovement : Movement, IDrivable
    {
        public static event Action OnCarMovement;
        public void Move(Transform playerTrans, float time, Animator animator, int currentIndex,
            int blockCount)
        {
            if (_isMoving) return;

            if (currentIndex != blockCount)
                base.Move(animator);

            OnCarMovement?.Invoke();
            
            StartCoroutine(DriveCoroutine(playerTrans, time, currentIndex, blockCount));
        }

        public IEnumerator DriveCoroutine(Transform playerTrans, float time, int currentIndex,
            int blockCount)
        {
            _isMoving = true;
            float elapsedTime = 0;
            Vector3 startPosition = transform.position;
            Vector3 target = playerTrans.position + _movingOffset;

            if (currentIndex != blockCount)
            {
                while (elapsedTime < time)
                {
                    float t = elapsedTime / time;

                    float easeT = EaseType(t);

                    Vector3 currentPosition = Vector3.Lerp(startPosition, target, easeT);

                    transform.position = currentPosition;

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }

            transform.position = target;
            _isMoving = false;
        }

        //EaseExpoInOut
        protected override float EaseType(float t)
        {
            if (t == 0) return 0f;
            if (t == 1) return 1f;

            if (t < 0.5f)
            {
                return Mathf.Pow(2, 20 * t - 10) / 2;
            }
            else
            {
                return (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
            }
        }
    }
}