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
            if (Player.Instance != null)
            {
                var player = Player.Instance;

                player.OnDie += UpdateUIScore;
                player.OnFinish += UpdateUIScore;
                player.OnStart += UpdateUIScore;
                player.OnDamage += UpdateUIScore;
            }
        }
        protected override void OnBallCollisionSegment(SegmentType type, bool isKillZone)
        {
            if (type != SegmentType.Trap || type == SegmentType.Finish || type == SegmentType.Empty)
            {
                UpdateUIScore();
            }           
        }

        private void UpdateUIScore()
        {
            scoreText.text = scoreCollector.Scores.ToString();
            maxScoreText.text = scoreCollector.MaxScore.ToString();
        }
    }

}
