using UnityEngine;

namespace HelixJump
{
    public abstract class BallEvent : MonoBehaviour
    {
         [SerializeField] private BallController m_BallController;

        protected virtual void Awake()
        {            
            if (Player.Instance != null && m_BallController == null) m_BallController = Player.Instance.BallController;
            m_BallController.CollisionSegment.AddListener(OnBallCollisionSegment);
        }

        private void OnDestroy()
        {
            m_BallController.CollisionSegment.RemoveListener(OnBallCollisionSegment);
        }

        protected virtual void OnBallCollisionSegment(SegmentType type, bool isKillZone)
        { }

    }
}

