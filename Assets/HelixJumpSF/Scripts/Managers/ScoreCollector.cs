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

        private bool doubleShoot = false;

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
            }
        }

        protected override void OnBallCollisionSegment(SegmentType type, bool isKillZone)
        {
            if (type != SegmentType.Empty)
            {
                doubleShoot = false;
            }

            if (type == SegmentType.Empty)
            {
                AddScore(levelProgress.CurrentLevel);
                if (doubleShoot == true) AddScore(levelProgress.CurrentLevel);
                doubleShoot = true;
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


        public void AddScore(int score)
        {
            scores = Mathf.Clamp(scores + score, 0, int.MaxValue); 
        }
        public void ReduceScoreDamage()
        {
            AddScore(-levelProgress.CurrentLevel); 
        }
        public void ReduceScoreDeath()
        {
            AddScore(- levelProgress.CurrentLevel ^ 2); 
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
