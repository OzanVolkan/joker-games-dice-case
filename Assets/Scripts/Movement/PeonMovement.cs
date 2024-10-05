using System.Collections;
using Interfaces;
using UnityEngine;

namespace Movement
{
    public sealed class PeonMovement : Movement, IJumpable
    {
        public void Move(Vector3 target, float height, float time)
        {
            if(_isMoving) return;

            StartCoroutine(JumpCoroutine(target, height, time));
        }

        public IEnumerator JumpCoroutine(Vector3 target, float height, float time)
        {
            _isMoving = true;
            float elapsedTime = 0;
            Vector3 startPosition = transform.position;

            while (elapsedTime < time)
            {
                float t = elapsedTime / time;

                float easeT = EaseType(t);

                Vector3 currentPosition = Vector3.Lerp(startPosition, target + Vector3.forward * 8.15f, easeT);

                float verticalOffset = Mathf.Sin(easeT * Mathf.PI) * height;
                currentPosition.y += verticalOffset;

                transform.position = currentPosition;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = target + Vector3.forward * 8.15f;
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
