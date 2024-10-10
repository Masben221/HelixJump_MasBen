using System;
using UnityEngine;

namespace HelixJump
{
    public class ScoreCollector : BallEvent
    {
        [SerializeField] private LevelProgress levelProgress;

        [SerializeField] private int scores;
        public int Scores => scores;

        [SerializeField] private int maxScore;
        public int MaxScore => maxScore;

        //—сылка на текст очков.
        [SerializeField] private ScoreStats m_ScoreStatsPrefab;

        private bool m_IsDoubleShoot = false;
        private int m_NumberSuperPower;

        protected override void Awake()
        {
            base.Awake();
            LoadMaxScores();
        }
        private void Start()
        {
            if (Player.Instance != null)
            {
                var player = Player.Instance;

                player.OnDie += ReduceScoreDeath;
                player.OnDamage += ReduceScoreDamage;
                player.OnSuperPower += ResetNumberSuperPower;
            }
        }

        protected override void OnBallCollisionSegment(SegmentType type, bool isKillZone)
        {
            if (type != SegmentType.Empty)
            {
                m_IsDoubleShoot = false;
                ResetNumberSuperPower();
            }

            if (type == SegmentType.Empty)
            {                
                if (m_IsDoubleShoot == true) AddScore(2 * levelProgress.CurrentLevel);
                else AddScore(levelProgress.CurrentLevel);
                m_IsDoubleShoot = true;
                m_NumberSuperPower ++;

                if (m_NumberSuperPower >= 3)
                {
                    Player.Instance.AddSuperPower(1);                    
                }
            }

            if (type == SegmentType.Finish)
            {
                if (scores > maxScore)
                {
                    maxScore = scores;
                    SaveMaxScores();
                }
            }
        }
        private void ResetNumberSuperPower()
        {
            m_NumberSuperPower = 0;
        }

        public void AddScore(int score)
        {
            if (m_ScoreStatsPrefab != null)
            {                
                var scoreStats = Instantiate(m_ScoreStatsPrefab, Player.Instance.transform.position, Quaternion.identity);
                scoreStats.ScoreStatistic = score;
            }
            scores = Mathf.Clamp(scores + score, 0, int.MaxValue); 
        }
        public void ReduceScoreDamage()
        {
            if (Player.Instance.CurrentHitPoint > 0) AddScore( - levelProgress.CurrentLevel); 
        }
        public void ReduceScoreDeath()
        {
           AddScore( - levelProgress.CurrentLevel * 5); 
        }

        private void SaveMaxScores()
        {
            PlayerPrefs.SetInt("ScoreCollector:MaxScore", maxScore);
        }

        private void LoadMaxScores()
        {
            maxScore = PlayerPrefs.GetInt("ScoreCollector:MaxScore", 0);
        }
    }

}
