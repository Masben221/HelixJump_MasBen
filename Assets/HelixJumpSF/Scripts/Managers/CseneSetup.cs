using UnityEngine;

namespace HelixJump
{
    public class CseneSetup : MonoBehaviour
    {
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private BallController m_BallController;
        [SerializeField] private LevelProgress levelProgress;
       
        private void Awake()
        {
            if (Player.Instance != null && m_BallController == null) m_BallController = Player.Instance.BallController;            
        }
        void Start()
        {
            LevelGenerate();
        }

        public void LevelGenerate()
        {
            levelGenerator.Generate(levelProgress.CurrentLevel);
            m_BallController.transform.position = new Vector3(m_BallController.transform.position.x, levelGenerator.LastFloorY, m_BallController.transform.position.z);
        }

    }
}

