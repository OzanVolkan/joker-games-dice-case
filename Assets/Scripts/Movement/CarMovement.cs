using System.Collections;
using Interfaces;
using UnityEngine;

namespace Movement
{
    public sealed class CarMovement : Movement, IDrivable
    {
        public void Move(Vector3 target, float time)
        {
            if(_isMoving) return;

            StartCoroutine(DriveCoroutine(target, time));
        }

        public IEnumerator DriveCoroutine(Vector3 target, float time)
        {
            _isMoving = true;
            float elapsedTime = 0;
            Vector3 startPosition = transform.position;

            while (elapsedTime < time)
            {
                float t = elapsedTime / time;

                float easeT = EaseType(t);

                Vector3 currentPosition = Vector3.Lerp(startPosition, target + Vector3.forward * 8.15f, easeT);

                transform.position = currentPosition;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = target + Vector3.forward * 8.15f;
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