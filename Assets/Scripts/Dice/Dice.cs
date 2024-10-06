using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Action<Animator> _onMovementStart;

    private void OnEnable()
    {
        _onMovementStart = (Animator _) => DeactivateDice();
        Movement.Movement.OnForwardMovement += _onMovementStart;
    }

    private void OnDisable()
    {
        Movement.Movement.OnForwardMovement -= _onMovementStart;
    }

    private void DeactivateDice()
    {
        gameObject.SetActive(false);
    }
}
