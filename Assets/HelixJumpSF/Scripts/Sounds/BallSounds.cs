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
        [SerializeField] private Sound m_DamageSound = Sound.Damage;
        [SerializeField] private Sound m_ShieldSound = Sound.Shield;
        [SerializeField] private Sound m_SuperPowerSound = Sound.SuperPower;

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
                player.OnDamage += SoundDamage;                
                player.OnShield += SoundShield;                
                player.OnSuperPower += SoundSuperPower;                
            }
        }
        protected override void OnBallCollisionSegment(SegmentType type, bool isKillZone)
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

            if (type == SegmentType.Trap && isKillZone)
            {
                m_SpikeSound.Play();                
            }

            if (type == SegmentType.Bonus && isKillZone)
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
        private void SoundDamage() { m_DamageSound.Play(); }
        private void SoundShield() { m_ShieldSound.Play(); }
        private void SoundSuperPower() { m_SuperPowerSound.Play(); }
    }
}

