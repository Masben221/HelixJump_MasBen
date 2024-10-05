using UnityEngine;

namespace HelixJump
{
    public class UIGamePanel : BallEvent
    {
        [SerializeField] private GameObject passedPanel;
        [SerializeField] private GameObject defeatPanel;
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            passedPanel.SetActive(false);
            defeatPanel.SetActive(false);
        }
        protected override void OnBallCollisionSegment(SegmentType type)
        {
            if (type == SegmentType.Spike || type == SegmentType.Piston)
            {
                defeatPanel.SetActive(true);
            }

            if (type == SegmentType.Finish)
            {
                passedPanel.SetActive(true);
            }
        }
    }

}
