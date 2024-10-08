using System;
using UnityEngine;

namespace Dice
{
    public class Dice : MonoBehaviour
    {
        private Action<Animator> _onMovementStart;

        [SerializeField] private ParticleSystem _impactParticle;
        [SerializeField] private Collider _diceCollider;

        private void OnEnable()
        {
            _onMovementStart = (Animator _) => DeactivateDice();
            PlayerControl.Movement.Movement.OnForwardMovement += _onMovementStart;
        }

        private void OnDisable()
        {
            PlayerControl.Movement.Movement.OnForwardMovement -= _onMovementStart;
        }

        private void DeactivateDice()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                _impactParticle.Play();
                _diceCollider.enabled = false;
            }
        }
    }
}
