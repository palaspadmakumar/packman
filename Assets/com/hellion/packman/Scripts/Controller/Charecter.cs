using System;
using com.hellion.statemachine;
using com.hellion.tilesystem;
using com.hellion.tilesystem.utilities;
using UnityEngine;
using UnityEngine.UI;

namespace com.hellion.packaman
{
    public class Charecter : AbstractCharecter
    {
        #region Serialized Fields
        [SerializeField] private int _x, _y;
        [SerializeField] private ECharecterState _charecterState = ECharecterState.ALIVE;
        [SerializeField] private ECharecterType _charecterType;
        [SerializeField] private TileObject tileObject;
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private Image _charecterbody;
        [SerializeField] private float _vulanaibilityTime = 3f, _deathTime = 3f, _blinkTime = 1f, _blinkFrequece = 0.2f;
        [SerializeField] private Color _vulnColor = Color.red;
        #endregion

        #region Private
        public StateMachine<Charecter> stateMachine;
        private ICharecter charecter;
        private Color _color;
        private TileObject target = null;
        private Vector3 _moveDir = Vector3.zero;
        public static Action<Charecter, TileObject> CurrentTile;
        #endregion

        #region Enums
        public enum ELookDirection
        {
            LEFT,
            RIGHT,
            UP,
            DOWN
        }

        public enum ECharecterType
        {
            PACMAN,
            BLINKY,
            PINKY,
            INKY,
            CLYDE
        }

        public enum ECharecterState
        {
            ALIVE,
            VULNARABLE,
            DEAD
        }
        #endregion

        private void Start()
        {
            switch (_charecterType)
            {
                case ECharecterType.PACMAN:
                    charecter = new Pacman();
                    break;
                case ECharecterType.BLINKY:
                    charecter = new Blinky();
                    break;
                case ECharecterType.PINKY:
                    charecter = new Pinky();
                    break;
                case ECharecterType.INKY:
                    charecter = new Inky();
                    break;
                case ECharecterType.CLYDE:
                    charecter = new Clyde();
                    break;
            }

            stateMachine = new StateMachine<Charecter>(this);
            ResetCharecter();
            if (_charecterType != ECharecterType.PACMAN)
            {
                _color = _charecterbody.color;
            }
            else
            {
                CurrentTile += CurrentTileUpdate;
            }
        }

        private void CurrentTileUpdate(Charecter charecter, TileObject currentTile)
        {
            if (charecter.GetCharecterState() != ECharecterState.DEAD && currentTile.GetInstanceID() == tileObject.GetInstanceID())
            {
                if (charecter.GetCharecterState() == ECharecterState.VULNARABLE)
                {
                    charecter.stateMachine.ChangeState(StateCharecterDead.Instance);
                }
                else
                {
                    PackmanDead();
                }
            }
        }

        private void PackmanDead()
        {
            GameManager.Instance.ResetGame();
        }

        public void ResetCharecter()
        {
            target = null;
            tileObject = TileGenerator.GetTile(_y, _x);
            transform.position = tileObject.transform.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.ResumeGame();
            }

            if (GameManager.Instance.isGamePaused)
            {
                return;
            }

            stateMachine?.Update(this);

            if (target != null)
            {
                MoveToTarget();
            }

            if (_charecterType != ECharecterType.PACMAN)
            {
                if (tileObject)
                {
                    CurrentTile?.Invoke(this, tileObject);
                }
            }

            if (_charecterType == ECharecterType.PACMAN && !target)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    target = TileGenerator.GetLeftTile(tileObject);
                    ReCalculateMoveDirection();
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    target = TileGenerator.GetRightTile(tileObject);
                    ReCalculateMoveDirection();
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    target = TileGenerator.GetUpTile(tileObject);
                    ReCalculateMoveDirection();
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    target = TileGenerator.GetDownTile(tileObject);
                    ReCalculateMoveDirection();
                }
                if (target != null)
                {
                    if (target.GetPelletType() != EPelletType.None)
                    {
                        target.SetPellet(EPelletType.None);
                        GameManager.Instance.AddScore(1);
                    }
                }
            }
            else if (target == null)
            {
                target = charecter.GetNextTile(tileObject, this);
                ReCalculateMoveDirection();
            }
        }

        private void ReCalculateMoveDirection()
        {
            if (target == null)
            {
                _moveDir = Vector3.zero;
                return;
            }
            if (target.GetIndex().x > tileObject.GetIndex().x)
            {
                if (_charecterType != ECharecterType.PACMAN)
                {
                    _eye.sprite = look_right;
                }
                SetELookDirection(ELookDirection.RIGHT);
                _moveDir = Vector3.right;
            }
            else if (target.GetIndex().x < tileObject.GetIndex().x)
            {
                if (_charecterType != ECharecterType.PACMAN)
                {
                    _eye.sprite = look_left;
                }
                SetELookDirection(ELookDirection.LEFT);
                _moveDir = Vector3.left;
            }
            else if (target.GetIndex().y > tileObject.GetIndex().y)
            {
                if (_charecterType != ECharecterType.PACMAN)
                {
                    _eye.sprite = look_up;
                }
                SetELookDirection(ELookDirection.UP);
                _moveDir = Vector3.up;
            }
            else if (target.GetIndex().y < tileObject.GetIndex().y)
            {
                if (_charecterType != ECharecterType.PACMAN)
                {
                    _eye.sprite = look_down;
                }
                SetELookDirection(ELookDirection.DOWN);
                _moveDir = Vector3.down;
            }
            else
            {
                target = null;
                _moveDir = Vector3.zero;
            }
        }

        public void SetCharecterToDeadState()
        {
            Color tempColor = _charecterbody.color;
            tempColor.a = 0f;
            _charecterbody.color = tempColor;
        }

        public void SetCharecterToAliveState()
        {
            _charecterbody.color = _color;
        }

        public void SetCharecterToVulnarable()
        {
            _charecterbody.color = _vulnColor;
        }

        public void SetCharecterState(ECharecterState charecterState)
        {
            _charecterState = charecterState;
        }

        public ECharecterState GetCharecterState()
        {
            return _charecterState;
        }

        public float GetVulnTime()
        {
            return _vulanaibilityTime;
        }

        public float GetDeathTime()
        {
            return _deathTime;
        }

        public float GetBlinkTime()
        {
            return _blinkTime;
        }

        public float GetBlinkFrequece()
        {
            return _blinkFrequece;
        }

        private void MoveToTarget()
        {
            transform.position += _moveDir * moveSpeed * 0.001f;
            if (Vector3.Distance(transform.position, target.transform.position) < 0.01f
            || Math.Abs(tileObject.GetIndex().x - target.GetIndex().x) > 1
            || Math.Abs(tileObject.GetIndex().y - target.GetIndex().y) > 1)
            {
                tileObject = target;
                transform.position = target.transform.position;
                target = null;
            }
        }

        private ELookDirection _lookDirection;

        public ELookDirection GetELookDirection()
        {
            return _lookDirection;
        }

        public void SetELookDirection(ELookDirection lookDirection)
        {
            _lookDirection = lookDirection;
        }

        public TileObject GetCurrentTie()
        {
            return tileObject;
        }
    }
}