using UnityEngine;
using UnityEngine.UI;

namespace HelixJump
{
    public class DamageStats : MonoBehaviour
    {
        [SerializeField] private Text m_Text;
        
        // скорость текста
        [SerializeField] private float m_Velocity;

        private int m_DamageStats;
        private int m_LastDamage; 

        private void Start()
        {            
            m_DamageStats = 0;

            if (Player.Instance != null)
            {
                m_DamageStats = (int)Player.Instance.CurrentDamage;                
            }            
        }
        private void Update()
        {
            UpdateDamage();
        }
        private void UpdateDamage()
        {
            if (m_DamageStats != m_LastDamage && m_DamageStats != 0)
            {
                m_LastDamage = m_DamageStats;
                m_Text.text = "-" + m_LastDamage.ToString();
            }

            float stepLenght = Time.deltaTime * m_Velocity; // шаг текста за один кадр
            Vector2 step = transform.up * stepLenght; // превращение в вектор по направлению
            transform.position += new Vector3(step.x, step.y, 0);// перемещение в заданном направлении}           
        }
    }
}

