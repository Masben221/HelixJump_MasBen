using UnityEngine;
using UnityEngine.UI;

namespace HelixJump
{
    public class UIShieldProgress : MonoBehaviour
    {
        [SerializeField] private Image m_ShildProgressBar;        

        private float m_FillAmountStep;
        public float FillAmountStep { get => m_FillAmountStep; set => m_FillAmountStep = value; }

        private void Start()
        {
            m_ShildProgressBar.fillAmount = 1;

            var player = Player.Instance;

            if (player != null)
            {
                FillAmountStep = 1f / (float)player.Shild;
                player.EventOnUpdateShild?.AddListener(UpdateShildProgress);
            }
        }

        public void UpdateShildProgress(float ShildCount)
        {
            m_ShildProgressBar.fillAmount = ShildCount * m_FillAmountStep;            
        } 
    }
}