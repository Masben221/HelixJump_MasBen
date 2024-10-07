using UnityEngine;
using UnityEngine.UI;

namespace HelixJump
{
    public class UIShieldProgress : MonoBehaviour
    {
        [SerializeField] private Text m_ShieldProgressText;        
        [SerializeField] private GameObject m_Shield;        

        private void Start()
        {            
            var player = Player.Instance;
            
            if (player != null )
            {
                if (player.CurrentShield > 0) m_Shield.SetActive(true);
                else m_Shield.SetActive(false);
                m_ShieldProgressText.text = player.CurrentShield.ToString();
                player.EventOnUpdateShield?.AddListener(UpdateShildProgress);
            }            
        }

        public void UpdateShildProgress(float ShildCount)
        {
            var player = Player.Instance;
            m_ShieldProgressText.text = player.CurrentShield.ToString();
            if (player.CurrentShield > 0) m_Shield.SetActive(true);
            else m_Shield.SetActive(false);
        } 
    }
}