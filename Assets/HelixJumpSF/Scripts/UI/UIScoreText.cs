using UnityEngine;
using UnityEngine.UI;

namespace HelixJump
{
    public class UIScoreText : BallEvent
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Text maxScoreText;
        [SerializeField] private ScoreCollector scoreCollector;
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            maxScoreText.text = scoreCollector.MaxScore.ToString();
        }
        protected override void OnBallCollisionSegment(SegmentType type)
        {
            if (type != SegmentType.Spike)
            {
                scoreText.text = scoreCollector.Scores.ToString();
            }
            if (type == SegmentType.Finish)
            {
                maxScoreText.text = scoreCollector.MaxScore.ToString();
            }

        }        
    }

}
