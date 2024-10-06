using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Movement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public static event Action OnMovementEnd;
    public static event Action<Animator> OnEndMap;
    public static event Action<Animator> OnEnterMap;

    private Movement.Movement _playerMovement;
    private Transform _playerTransform;
    private Animator _playerAnimator;
    private Collider _playerCollider;
    private int _currentIndex;

    //TODO: DÜZENLENECEK VE HER İKİ CHARA DA KOYULACAK!!!!!!!!!!!!!!!!!!!!****
    public GameObject endParticle;

    #region ReadonlyFields

    private readonly float _jumpHeight = 1f;
    private readonly float _moveTime = 0.5f;
    private readonly float _waitTimeBeforeMove = 4f;
    private readonly Vector3 _startOverPos = new Vector3(0f, 4f, 0f);

    #endregion

    #region Properties

    public int BlockCount { get; set; }

    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            List<int> intList = new List<int>();
            intList.Add(24);
            MovePlayer(intList);
        }
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
                _playerCollider = child.GetComponent<Collider>();
            }
        }
    }

    #region PlayerMovement

    private void MovePlayer(List<int> diceValues)
    {
        StartCoroutine(MoveCalculation(diceValues, _moveTime,
            _playerAnimator, _currentIndex, BlockCount));
    }

    private IEnumerator MoveCalculation(List<int> diceValues, float time,
        Animator animator, int currentIndex, int blockCount)
    {
        var stepAmount = diceValues.Sum();
        Action moveMethod = GetMoveMethod(animator, currentIndex, blockCount);

        yield return new WaitForSeconds(_waitTimeBeforeMove);

        for (int i = 1; i <= stepAmount; i++)
        {
            _currentIndex++;

            if (_currentIndex == BlockCount)
            {
                _currentIndex = 0;

                yield return HandleEndOfMap(_playerTransform, time);

                continue;
            }

            moveMethod?.Invoke();

            yield return new WaitForSeconds(time + 0.1f);
            
            if (i == stepAmount)
                EnableCollider(true);
        }

        yield return new WaitForSeconds(time);

        EnableCollider(false);
        OnMovementEnd?.Invoke();
    }

    private Action GetMoveMethod(Animator animator, int currentIndex, int blockCount)
    {
        return _playerMovement switch
        {
            PeonMovement peonMovement => () =>
                peonMovement.Move(_playerTransform, _jumpHeight, _moveTime, animator, currentIndex, blockCount),
            CarMovement carMovement => () =>
                carMovement.Move(_playerTransform, _moveTime, animator, currentIndex, blockCount),
            _ => null
        };
    }

    private IEnumerator HandleEndOfMap(Transform playerTrans, float time)
    {
        OnEndMap?.Invoke(_playerAnimator);
        _playerMovement.VerticalJump(playerTrans, time, true);
        yield return new WaitForSeconds(time);

        playerTrans.localPosition = _startOverPos;
        OnEnterMap?.Invoke(_playerAnimator);
        _playerMovement.VerticalJump(playerTrans, time, false);
        yield return new WaitForSeconds(time + 0.1f);
    }

    private void EnableCollider(bool state)
    {
        if (_playerCollider.enabled != state)
            _playerCollider.enabled = state;
    }

    #endregion
}