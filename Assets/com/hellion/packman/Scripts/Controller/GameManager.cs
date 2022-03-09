using System;
using System.Collections;
using System.Collections.Generic;
using com.hellion.tilesystem;
using UnityEngine;

namespace com.hellion.packaman
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private Charecter _player;
        [SerializeField] public List<Charecter> _enemies;
        public static GameManager Instance;
        [HideInInspector] public int totalPelletCount = 0;
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
            isGamePaused = true;
            totalPelletCount = 0;
            foreach (Charecter enemy in _enemies)
            {
                enemy.ResetCharecter();
            }
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

        public void AddScore(int v)
        {
            totalPelletCount -= 1;
            if (totalPelletCount <= 0)
            {
                totalPelletCount = 0;
                GameWon();
            }
        }

        private void GameWon()
        {

        }

    }
}
