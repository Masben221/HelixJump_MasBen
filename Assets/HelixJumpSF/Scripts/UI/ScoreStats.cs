using UnityEngine;
using UnityEngine.UI;

namespace HelixJump
{
    public class ScoreStats : MonoBehaviour
    {
        [SerializeField] private Text m_Text;
        
        // скорость текста
        [SerializeField] private float m_Velocity;

        private int m_ScoreStats;
        public int ScoreStatistic { get => m_ScoreStats; set => m_ScoreStats = value; }

        private void Start()
        {
            InitScore();
        }
        private void Update()
        {
            UpdateScore();
        }
        private void InitScore()
        {
            if (m_ScoreStats != 0)
            {
                if (m_ScoreStats > 0)
                {
                    m_Text.text = "+" + m_ScoreStats.ToString();
                    m_Text.color = Color.green;
                }
                else
                {
                    m_Text.text = m_ScoreStats.ToString();
                    m_Text.color = Color.yellow;
                    transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                }
            }                
        }

        private void UpdateScore()
        {            
            float stepLenght = Time.deltaTime * m_Velocity; // шаг текста за один кадр
            Vector2 step = - transform.right * stepLenght; // превращение в вектор по направлению
            transform.position += new Vector3(step.x, step.y, 0);// перемещение в заданном направлении}           
        }
    }
}

