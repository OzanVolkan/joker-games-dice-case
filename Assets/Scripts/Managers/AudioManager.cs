using System;
using System.Collections.Generic;
using System.Collections;
using Map.Blocks;
using PlayerControl;
using PlayerControl.Movement;
using UI;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        private Action<int, List<int>> _onDiceRoll;
        private Action<int, Vector3> _onAppleCollect;
        private Action<int, Vector3> _onPearCollect;
        private Action<int, Vector3> _onStrawberryCollect;
        private Action<int, int, int> _onPopEffect;
        private Action<Animator> _onTeleport;

        [Header("Audio Source")] [SerializeField]
        private AudioSource _effectAudioSource;

        [SerializeField] private AudioSource _musicAudioSource;

        [Header("Audio Clips")] [SerializeField]
        private AudioClip _peonMovement;

        [SerializeField] private AudioClip _carMovement;
        [SerializeField] private AudioClip _buttonClick;
        [SerializeField] private AudioClip _gameStart;
        [SerializeField] private AudioClip _diceRoll;
        [SerializeField] private AudioClip _collectReward;
        [SerializeField] private AudioClip _collectEmpty;
        [SerializeField] private AudioClip _popEffect;
        [SerializeField] private AudioClip _tpEffect;
        [SerializeField] private AudioClip _levelMusic;

        #region EffectAudioSourceValues

        private readonly float _pitchIncrement = 0.1f;
        private readonly float _maxPitch = 2.0f;
        private readonly float _pitchResetDelay = 2.0f;
        private readonly float _defaultPitch = 1.0f;

        #endregion

        private Coroutine _pitchResetCoroutine;

        private void OnEnable()
        {
            _onTeleport = animator => PlayTeleport();
            PlayerController.OnEndMap += _onTeleport;

            _onPopEffect = (i, i1, arg3) => PlayPopEffect();
            InventoryManager.OnUpdateRewardCount += _onPopEffect;

            _onDiceRoll = (int i, List<int> ints) => PlayDiceRoll();
            UIManager.OnDiceRoll += _onDiceRoll;

            _onAppleCollect = (i, vector3) => PlayCollectReward();
            _onPearCollect = (i, vector3) => PlayCollectReward();
            _onStrawberryCollect = (i, vector3) => PlayCollectReward();

            AppleBlock.OnAppleCollect += _onAppleCollect;
            PearBlock.OnPearCollect += _onPearCollect;
            StrawberryBlock.OnStrawberryCollect += _onStrawberryCollect;

            EmptyBlock.OnEmptyCollect += PlayCollectEmpty;
            PeonMovement.OnPeonMovement += PlayPeonMovement;
            CarMovement.OnCarMovement += PlayCarMovement;
            CharacterSelection.OnGameStart += PlayGameStart;
            CharacterSelection.OnButtonClick += PlayButtonClick;
        }

        private void OnDisable()
        {
            PlayerController.OnEndMap -= _onTeleport;
            InventoryManager.OnUpdateRewardCount -= _onPopEffect;
            UIManager.OnDiceRoll -= _onDiceRoll;
            AppleBlock.OnAppleCollect -= _onAppleCollect;
            PearBlock.OnPearCollect -= _onPearCollect;
            StrawberryBlock.OnStrawberryCollect -= _onStrawberryCollect;
            EmptyBlock.OnEmptyCollect -= PlayCollectEmpty;
            PeonMovement.OnPeonMovement -= PlayPeonMovement;
            CarMovement.OnCarMovement -= PlayCarMovement;
            CharacterSelection.OnGameStart -= PlayGameStart;
            CharacterSelection.OnButtonClick -= PlayButtonClick;
        }

        private void PlayPeonMovement()
        {
            _effectAudioSource.PlayOneShot(_peonMovement);
        }

        private void PlayCarMovement()
        {
            _effectAudioSource.PlayOneShot(_carMovement);
        }

        private void PlayButtonClick()
        {
            _effectAudioSource.PlayOneShot(_buttonClick);
        }

        private void PlayGameStart()
        {
            _effectAudioSource.PlayOneShot(_gameStart);
        }

        private void PlayDiceRoll()
        {
            _effectAudioSource.PlayOneShot(_diceRoll);
        }

        private void PlayCollectReward()
        {
            _effectAudioSource.PlayOneShot(_collectReward);
        }

        private void PlayCollectEmpty()
        {
            _effectAudioSource.PlayOneShot(_collectEmpty);
        }

        private void PlayPopEffect()
        {
            _effectAudioSource.pitch =
                Mathf.Clamp(_effectAudioSource.pitch + _pitchIncrement, _defaultPitch, _maxPitch);

            _effectAudioSource.PlayOneShot(_popEffect);

            if (_pitchResetCoroutine != null)
            {
                StopCoroutine(_pitchResetCoroutine);
            }

            _pitchResetCoroutine = StartCoroutine(ResetPitchAfterDelay());
        }

        private void PlayTeleport()
        {
            _effectAudioSource.PlayOneShot(_tpEffect);
        }

        private IEnumerator ResetPitchAfterDelay()
        {
            yield return new WaitForSeconds(_pitchResetDelay);

            _effectAudioSource.pitch = _defaultPitch;
        }
    }
}