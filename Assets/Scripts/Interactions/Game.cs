using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private global::Player _bird;
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private EndGameScreen _endGameScreen;

        public event Action Finished;
        public event Action Started;

        private void OnEnable()
        {
            _startScreen.PlayButtonClicked += OnPlayButtonClick;
            _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
            _bird.GameOver += EndGame;
        }

        private void OnDisable()
        {
            _startScreen.PlayButtonClicked -= OnPlayButtonClick;
            _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
            _bird.GameOver -= EndGame;
        }

        private void Start()
        {
            Time.timeScale = 0;
            _endGameScreen.Close();
            _startScreen.Open();
        }

        private void OnRestartButtonClick()
        {
            _endGameScreen.Close();
            StartGame();
        }

        private void OnPlayButtonClick()
        {
            _startScreen.Close();
            StartGame();
        }

        private void EndGame()
        {
            Time.timeScale = 0;
            _endGameScreen.Open();
            Finished?.Invoke();
        }

        private void StartGame()
        {
            Time.timeScale = 1;
            _bird.Reset();
            Started?.Invoke();
        }
    }
}