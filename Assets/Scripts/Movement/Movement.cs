using System;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public abstract class Movement : MonoBehaviour
    {
        public static event Action<Animator> OnForwardMovement;

        protected Vector3 _movingOffset = new Vector3(0f, 0f, 8.15f);
        protected bool _isMoving;
        
        protected abstract float EaseType(float t);

        protected virtual void Move(Animator animator)
        {
            OnForwardMovement?.Invoke(animator);
        }
    }
}