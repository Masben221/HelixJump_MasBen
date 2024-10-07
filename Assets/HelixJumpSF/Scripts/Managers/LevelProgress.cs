using UnityEngine;
using UnityEngine.SceneManagement;

namespace HelixJump
{
    public class LevelProgress : BallEvent
    {
        [SerializeField] private ScoreCollector scoreCollector;
        private int currentLevel = 1;
        public int CurrentLevel => currentLevel;
        private bool m_IsFinish;

        protected override void Awake()
        {
            base.Awake();
            Load();
            m_IsFinish = false;
        }

        //#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1) == true)
            {
                Reset();
            }

            if (Input.GetKeyDown(KeyCode.F2) == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (Input.GetKeyDown(KeyCode.Escape) == true)
            {
                Application.Quit();
            }
        }
        //#endif

        protected override void OnBallCollisionSegment(SegmentType type, bool isKillZone)
        {
            if (type == SegmentType.Finish)
            {
                if (m_IsFinish == false)
                {
                    currentLevel++;
                    Save();
                    Player.Instance.EventFinish();
                    m_IsFinish = true;
                }                    
            }
        }

        private void Save()
        {
            PlayerPrefs.SetInt("LevelProgress:CurrentLevel", currentLevel);
        }

        private void Load()
        {
            currentLevel = PlayerPrefs.GetInt("LevelProgress:CurrentLevel", 1);
        }
        

        //#if UNITY_EDITOR
        private void Reset()
        {
            PlayerPrefs.DeleteAll();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        //#endif
    }

}
