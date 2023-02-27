using UnityEngine;
using UnityEngine.UI;

public class UIScoreText : BallEvent
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text maxScoreText;
    [SerializeField] private ScoreCollector scoreCollector;

    private void Start()
    {
        maxScoreText.text = scoreCollector.MaxScore.ToString();
    }
    protected override void OnBallCollisionSegment(SegmentType type)
    {
        if (type != SegmentType.Trap)
        {
            scoreText.text = scoreCollector.Scores.ToString();
        }
        if (type == SegmentType.Finish)
        {
            maxScoreText.text = scoreCollector.MaxScore.ToString();
        }

    }
    protected override void Awake()
    {
        base.Awake();
    }
}
