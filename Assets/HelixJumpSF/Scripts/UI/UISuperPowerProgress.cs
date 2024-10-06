using UnityEngine;
using UnityEngine.UI;

namespace HelixJump
{
    public class UISuperPowerProgress : MonoBehaviour
    {
        [SerializeField] private Text m_ShildProgressText; 
        
        private void Start()
        {    
            var player = Player.Instance;

            if (player != null)           
            {                
                player.EventOnUpdateSuperPower?.AddListener(UpdateSuperPowerProgress);
            }
        }

        public void UpdateSuperPowerProgress(int t)
        {
            var player = Player.Instance;
            m_ShildProgressText.text =  player.SuperPower.ToString();            
        } 
    }
}