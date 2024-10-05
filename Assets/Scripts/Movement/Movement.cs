using System.Collections;
using UnityEngine;

namespace Movement
{
    public abstract class Movement : MonoBehaviour
    {
        protected Animator _charAnimator;
        protected Transform _targetPos;
        protected float _moveDuration = 0.5f;
        protected string _moveAnimTrigger = "Forward";
        protected bool _isMoving;
        
        protected abstract float EaseType(float t);

    }
}