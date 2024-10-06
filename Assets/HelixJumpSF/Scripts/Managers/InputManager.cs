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

        private void Start()
        {
            if (Player.Instance != null)
            {
                var player = Player.Instance;

                player.OnStart += StartRotate;
                player.OnFinish += StopRotate;
                player.OnDie += StopRotate;                
            }           
        }
        /*protected override void OnBallCollisionSegment(SegmentType type)
        {
            if (type == SegmentType.Finish || type == SegmentType.Spike || type == SegmentType.Piston)
            {
                InputRotator.enabled = false;
            }
        }*/
        private void StartRotate()
        {
            InputRotator.enabled = true;
        } 
        private void StopRotate()
        {
            InputRotator.enabled = false;
        }
    }
}

