using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Movement;

public class PlayerController : MonoBehaviour
{
    private Movement.Movement _playerMovement;
    private Transform _playerTransform;
    private Animator _playerAnimator;
    private readonly float _jumpHeight = 1f;
    private readonly float _moveTime = 0.5f;
    private readonly float _waitTimeBeforeMove = 6f;
    private Vector3 _movingOffset = new Vector3(0f, 0f, 8.15f);

    private void OnEnable()
    {
        UIManager.OnForwardMovement += MovePlayer;
    }

    private void OnDisable()
    {
        UIManager.OnForwardMovement -= MovePlayer;
    }

    private void Start()
    {
        //TODO: DELETE HERE AFTER TEST
        InitPlayerComps();
    }

    private void InitPlayerComps()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                _playerMovement = child.GetComponent<Movement.Movement>();
                _playerTransform = child;
                _playerAnimator = child.GetComponent<Animator>();
            }
        }
    }

    #region PlayerMovement

    private void MovePlayer(List<int> diceValues)
    {
        StartCoroutine(MoveCalculation(diceValues, _playerTransform, _jumpHeight, _moveTime,
            _playerAnimator));
    }

    private IEnumerator MoveCalculation(List<int> diceValues,Transform playerTrans, float height, float time,
        Animator animator)
    {
        var stepAmount = diceValues.Sum();

        Action moveMethod = _playerMovement switch
        {
            PeonMovement peonMovement => () => peonMovement.Move(playerTrans, height, time, animator),
            CarMovement carMovement => () => carMovement.Move(playerTrans, time, animator),
            _ => null
        };

        yield return new WaitForSeconds(_waitTimeBeforeMove);

        for (int i = 1; i <= stepAmount; i++)
        {
            moveMethod?.Invoke();

            yield return new WaitForSeconds(time + 0.1f);
        }
    }

    #endregion
}