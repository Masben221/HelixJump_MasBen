using UnityEngine;
using UnityEngine.UI;

namespace HelixJump
{
    public class UILevelDrawProgress : BallEvent
    {
        [SerializeField] private LevelProgress levelProgress;
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private Text currentLevelText;
        [SerializeField] private Text nextLevelText;
        [SerializeField] private Image progressBar;

        private float fillAmountStep;

        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            currentLevelText.text = levelProgress.CurrentLevel.ToString();
            nextLevelText.text = (levelProgress.CurrentLevel + 1).ToString();
            progressBar.fillAmount = 0;

            fillAmountStep = 1 / levelGenerator.FloorAmount;
        }
        protected override void OnBallCollisionSegment(SegmentType type, bool isKillZone)
        {
            if (type == SegmentType.Empty || type == SegmentType.Finish)
            {
                fillAmountStep = 1 / levelGenerator.FloorAmount;
                progressBar.fillAmount += fillAmountStep;
            }
        }
    }

}
