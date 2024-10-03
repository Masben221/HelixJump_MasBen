using UnityEngine;

namespace HelixJump
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoSingleton<SoundPlayer>

    {
        [SerializeField] private Sounds m_Sounds;
        [SerializeField] private AudioClip m_BGM;
        private AudioSource m_AS;

        private new void Awake()
        {
            base.Awake();
            m_AS = GetComponent<AudioSource>();
            m_AS.clip = m_BGM;
            m_AS.Play();
        }      
        public void Play(Sound sound)
        {
            m_AS.PlayOneShot(m_Sounds[sound]);
        }       

        public void Stop(Sound sound)
        {
            m_AS.Stop();
        }
    }
}