using System;
using System.Collections.Generic;
using com.hellion.tilesystem;
using UnityEngine;

namespace com.hellion.packman
{
    public class GameManager : MonoBehaviour
    {

        public static Action OnPelletCollected;
        [SerializeField] private Charecter _player;
        [SerializeField] public List<Charecter> _enemies;
        public static GameManager Instance;
        [HideInInspector] public int totalPelletCount = 0;
        [SerializeField] private TMPro.TMP_Text _lifeCount;
        public bool isGamePaused { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public Charecter GetPlayer()
        {
            return _player;
        }

        public void ResetGame()
        {
            GameManager.Instance.isGamePaused = false;
            totalPelletCount = 0;
            _lifeCount.text = "3";
            foreach (Charecter enemy in _enemies)
            {
                enemy.ResetCharecter();
            }
            _player.ResetCharecter();
            TileGenerator.ResetGrid();
        }

        public void PauseGame()
        {
            isGamePaused = true;
        }

        public void ResumeGame()
        {
            isGamePaused = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.ShowPauseMenu();
            }
        }

        public void AddScore(int v)
        {
            totalPelletCount -= 1;
            OnPelletCollected?.Invoke();
            if (totalPelletCount <= 0)
            {
                totalPelletCount = 0;
                GameWon();
            }
        }

        public void SetEnemiesVulnarable()
        {
            foreach (Charecter enemy in _enemies)
            {
                enemy.stateMachine.ChangeState(StateCharecterVulnarable.Instance);
            }
        }

        private void GameWon()
        {
            UIManager.ShowGameWonMenu();
        }

        public void PlayerDead()
        {
            _lifeCount.text = (int.Parse(_lifeCount.text) - 1).ToString();
            if (int.Parse(_lifeCount.text) <= 0)
            {
                UIManager.ShowGameOverMenu();
            }
            else
            {
                isGamePaused = false;
                foreach (Charecter enemy in _enemies)
                {
                    enemy.ResetCharecter();
                }
                _player.ResetCharecter();
            }
        }

    }
}
