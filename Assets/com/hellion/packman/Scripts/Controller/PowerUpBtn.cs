using System;
using com.hellion.tilesystem;
using com.hellion.tilesystem.utilities;
using UnityEngine;
using UnityEngine.UI;
using static com.hellion.packaman.Charecter;

namespace com.hellion.packaman
{
    public class PowerUpBtn : MonoBehaviour
    {
        [SerializeField] private float _hideDelay = 5, _blinkSpeed = 1f, _moveSpeed = 1f;

        [SerializeField] private Slider _poserupSlider;
        [SerializeField] private GameObject _moveObject;
        [SerializeField] private Button _button;
        private TileObject _targetTile;
        private Vector3 _moveDir = Vector3.zero;

        private ELookDirection _lookDirection = ELookDirection.RIGHT;
        private float _timer = 0;


        private void Start()
        {
            _button.onClick.AddListener(ShootFireBall);
            GameManager.OnPelletCollected += OnPelletCollected;
            CurrentTile += CurrentTileUpdate;
            ResetPowerupButton();
            _moveObject.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ShootFireBall);
            GameManager.OnPelletCollected -= OnPelletCollected;
            CurrentTile -= CurrentTileUpdate;
        }

        private void OnPelletCollected()
        {
            _poserupSlider.value += 0.01f;
            if (_poserupSlider.value >= 1f)
            {
                _timer = 0;
                _poserupSlider.value = 0f;
                gameObject.SetActive(true);
            }
        }

        private void ResetPowerupButton()
        {
            _timer = 0;
            gameObject.SetActive(false);
        }

        private void CurrentTileUpdate(Charecter charecter, TileObject currentTile)
        {
            if (_targetTile != null)
            {
                if (currentTile.GetInstanceID() == _targetTile.GetInstanceID())
                {
                    _targetTile = null;
                    charecter.stateMachine.ChangeState(StateCharecterDead.Instance);
                    _moveObject.SetActive(false);
                }
            }
        }
        private void Update()
        {
            if (GameManager.Instance.isGamePaused)
            {
                return;
            }

            _timer += Time.deltaTime;
            if (_timer >= _hideDelay)
            {
                ResetPowerupButton();
            }

            transform.localScale = Vector2.one * Mathf.Clamp(Mathf.PingPong(Time.time * _blinkSpeed, 1f), 0.8f, 1f);

            if (_targetTile != null)
            {
                MoveToTarget();
            }
        }

        private void ShootFireBall()
        {
            _moveObject.SetActive(true);
            _moveObject.transform.position = GameManager.Instance.GetPlayer().GetCurrentTie().transform.position;
            _lookDirection = GameManager.Instance.GetPlayer().GetELookDirection();
            SetTarget(_lookDirection, GameManager.Instance.GetPlayer().GetCurrentTie());
        }

        private void MoveToTarget()
        {
            if (_targetTile != null)
            {
                if (Vector3.Distance(_moveObject.transform.position, _targetTile.transform.position) > 0.01f)
                {
                    _moveObject.transform.position += _moveDir * _moveSpeed * Time.deltaTime;
                }
                else
                {
                    SetTarget(_lookDirection, _targetTile);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Debug.LogError("Collision with tile");
            }
        }

        private void SetTarget(Charecter.ELookDirection direction, TileObject currentTile)
        {
            switch (direction)
            {
                case Charecter.ELookDirection.RIGHT:
                    _targetTile = TileGenerator.GetRightTile(currentTile);
                    _moveDir = Vector3.right;
                    break;
                case Charecter.ELookDirection.LEFT:
                    _targetTile = TileGenerator.GetLeftTile(currentTile);
                    _moveDir = Vector3.left;
                    break;
                case Charecter.ELookDirection.UP:
                    _targetTile = TileGenerator.GetUpTile(currentTile);
                    _moveDir = Vector3.up;
                    break;
                case Charecter.ELookDirection.DOWN:
                    _targetTile = TileGenerator.GetDownTile(currentTile);
                    _moveDir = Vector3.down;
                    break;
            }

            if (_targetTile == null)
            {
                _moveObject.SetActive(false);
            }
        }
    }
}
