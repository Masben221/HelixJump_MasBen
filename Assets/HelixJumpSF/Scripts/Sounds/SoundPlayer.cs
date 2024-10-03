using UnityEngine;

namespace HelixJump
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoSingleton<SoundPlayer>

    {
        [SerializeField] private Sounds m_Sounds;        
        private AudioSource m_AS;

        private new void Awake()
        {
            base.Awake();
            m_AS = GetComponent<AudioSource>();            
        }       
        public void Play(Sound sound)
        {
            m_AS.PlayOneShot(m_Sounds[sound]);
        }

        public void PlayLoop(Sound sound)
        {
            m_AS.loop = true;
            m_AS.PlayOneShot(m_Sounds[sound]);
        }

        public void Stop(Sound sound)
        {
            m_AS.Stop();
        }
    }
}