using UnityEngine;
using UnityEngine.UI;

namespace HelixJump
{
    public class UISuperPowerProgress : MonoBehaviour
    {
        [SerializeField] private Text m_SuperPowerText;
        [SerializeField] private GameObject m_SuperPower;

        private void Start()
        {    
            var player = Player.Instance;

            if (player != null)           
            {
                if (player.SuperPower > 0) m_SuperPower.SetActive(true);
                else m_SuperPower.SetActive(false);
                m_SuperPowerText.text = player.SuperPower.ToString();
                player.EventOnUpdateSuperPower?.AddListener(UpdateSuperPowerProgress);
            }
        }

        public void UpdateSuperPowerProgress(int t)
        {
            var player = Player.Instance;
            m_SuperPowerText.text =  player.SuperPower.ToString();
            if (player.SuperPower > 0) m_SuperPower.SetActive(true);
            else m_SuperPower.SetActive(false);
        } 
    }
}