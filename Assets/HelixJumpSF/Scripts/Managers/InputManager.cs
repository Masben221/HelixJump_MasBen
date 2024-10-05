using UnityEngine;

namespace HelixJump
{
    public class InputManager : BallEvent
    {
        [SerializeField] private MouseRotator InputRotator;
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void OnBallCollisionSegment(SegmentType type)
        {
            if (type == SegmentType.Finish || type == SegmentType.Spike || type == SegmentType.Piston)
            {
                InputRotator.enabled = false;
            }
        }
    }
}

