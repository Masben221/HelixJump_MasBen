using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public class BallTrail : BallEvent
    {
        [SerializeField] private Transform parentTransform;
        [SerializeField] private MeshRenderer visualMeshRenderer;
        [SerializeField] private Blot blotPrefab;
        [SerializeField] private GameObject m_SplashParticle;

        private List<Blot> blots = new List<Blot>();
        public List<Blot> Blots => blots;
        protected override void Awake()
        {
            base.Awake();
        }
      
        protected override void OnBallCollisionSegment(SegmentType type, bool isKillZone)
        {
            if ((type == SegmentType.Default || type == SegmentType.Trap || type == SegmentType.Bonus) && isKillZone == false)
            {
                Blot blot = Instantiate(blotPrefab, parentTransform);
                blot.Init(new Vector3(visualMeshRenderer.transform.position.x, transform.position.y, visualMeshRenderer.transform.position.z), visualMeshRenderer.material.color);
                blots.Add(blot);
            }

            if (type == SegmentType.Finish)
            {
                m_SplashParticle.SetActive(true);

                var particles = m_SplashParticle.GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < particles.Length; i++)
                {
                    ParticleSystem.MainModule main = particles[i].main;
                    main.startColor = visualMeshRenderer.material.color;
                }
            }
        }
    }
}
    
