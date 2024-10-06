using UnityEngine;

namespace HelixJump
{
    public class BallSounds : BallEvent
    {
        [SerializeField] private Sound m_FloorDestroySound = Sound.FloorDestroy;
        [SerializeField] private Sound m_FinishSound = Sound.Finish;
        [SerializeField] private Sound m_ToiletSound = Sound.Toilet;
        [SerializeField] private Sound m_SpikeSound = Sound.SpikeHit;
        [SerializeField] private Sound m_PistonSound = Sound.PistonHit;
        [SerializeField] private Sound m_BoltSound = Sound.Bolt;
        [SerializeField] private Sound m_JumpSound = Sound.Jump;
        [SerializeField] private Sound m_FlySound = Sound.Fly;
        [SerializeField] private Sound m_DieSound = Sound.Die;
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            if (Player.Instance != null)
            {
                var player = Player.Instance;

                player.OnDie += SoundDeath;                
            }
        }
        protected override void OnBallCollisionSegment(SegmentType type)
        {
            if (type == SegmentType.Default)
            {
                m_JumpSound.Play();
                m_BoltSound.Play();
            } 
            
            if (type == SegmentType.Finish)
            {
                m_ToiletSound.Play();
                m_FinishSound.Play();                
            } 

            if (type == SegmentType.Spike)
            {
                m_SpikeSound.Play();                
            }

            if (type == SegmentType.Piston)
            {
                m_PistonSound.Play();                
            }
            
            if (type == SegmentType.Fan)
            {
                m_FlySound.Play();
            }
            
            if (type == SegmentType.Empty)
            {
                m_FloorDestroySound.Play();
            }            
        } 
        private void SoundDeath () { m_DieSound.Play(); }
    }
}

