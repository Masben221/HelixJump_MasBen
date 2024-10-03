using System.Collections;
using System.Collections.Generic;
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

        protected override void OnBallCollisionSegment(SegmentType type)
        {
            if (type != SegmentType.Empty)
            {
                doubleShoot = false;
            }

            if (type == SegmentType.Empty)
            {
                scores += levelProgress.CurrentLevel;
                if (doubleShoot == true) scores += levelProgress.CurrentLevel;
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
