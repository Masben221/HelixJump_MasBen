using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

namespace HelixJump
{
    public class UIGamePanel : BallEvent
    {
        [SerializeField] private GameObject m_MainMenuPanel;
        [SerializeField] private GameObject m_PassedPanel;
        [SerializeField] private GameObject m_LossPanel;                
        [SerializeField] private GameObject m_PausePanel;
        [SerializeField] private GameObject m_LevelPanel;

        public static bool IsActivMainMenu = true;
        [SerializeField] private Button m_ContinueButton;

        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {           
            //”бирает шкалу прогресса если главное меню активно
            if (IsActivMainMenu == true)
            {
                MainMenu();
            }
            else
            {
                m_MainMenuPanel.SetActive(false);
                m_LevelPanel.SetActive(true);
                m_PassedPanel.SetActive(false);
                m_LossPanel.SetActive(false);
                m_PausePanel.SetActive(false);
            }

            IsActivMainMenu = false;

            if (Player.Instance != null)
            {
                var player = Player.Instance;

                player.OnDie += LossPanel;
                player.OnDie += CheckContinueButton;
                player.OnFinish += PassedPanel;                
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
           
        }

        /*protected override void OnBallCollisionSegment(SegmentType type)
        {
            if (type == SegmentType.Spike || type == SegmentType.Piston)
            {
                m_LossPanel.SetActive(true);
            }

            if (type == SegmentType.Finish)
            {
                m_PassedPanel.SetActive(true);
            }
        }*/

        public void Play() //ѕродолжает игру и скрывает главное меню
        {
            RestartLevel();
        }
        public void Continue() //ѕродолжает игру и показывает рекламу
        {
            // показ рекламы

            Time.timeScale = 1f;

            m_MainMenuPanel.SetActive(false);
            m_LevelPanel.SetActive(true);
            m_PassedPanel.SetActive(false);
            m_LossPanel.SetActive(false);

            if (Player.Instance != null)
            {
                var player = Player.Instance;
                player.KakaStart();
            }
        }

        public void LossPanel()
        {
            m_LossPanel.SetActive(true);
            //Invoke(nameof(StopTimeScale), 1.0f);
        }
        
        public void PassedPanel()
        {
            m_PassedPanel.SetActive(true);            
            Invoke(nameof(StopTimeScale), 1.5f);
        }
        
        private void StopTimeScale()
        {
            Time.timeScale = 0;
        }
        private void CheckContinueButton()
        {            
            if (Player.Instance.NumLives < 1) m_ContinueButton.interactable = false;
            else m_ContinueButton.interactable = true;
        }
        public void NextLevel()//—ледующий уровень и показывает рекламу
        {
            // показ рекламы

            IsActivMainMenu = false;
            RestartLevel();
        }

        public void Pause()
        {
            if (m_MainMenuPanel.activeSelf) return;
            
            if (m_PausePanel.activeSelf == false)
            {
                Time.timeScale = 0;
                m_PausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                m_PausePanel.SetActive(false); 
            }
        }
        public void MainMenu()
        {            
            m_MainMenuPanel.SetActive(true);
            m_LevelPanel.SetActive(false);
            m_PassedPanel.SetActive(false);
            m_LossPanel.SetActive(false);
            m_PausePanel.SetActive(false);
            Time.timeScale = 0f;
        }

        public void Reset() //—брос игры
        {
            PlayerPrefs.DeleteAll();

            m_MainMenuPanel.SetActive(false);
            m_LevelPanel.SetActive(true);            

            RestartLevel();
        }
        public void RestartLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadLevel(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
