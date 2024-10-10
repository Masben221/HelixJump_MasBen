using UnityEngine;
using UnityEngine.UI;

namespace HelixJump
{
    public class UIHPProgress : MonoBehaviour
    {
        [SerializeField] private Image m_HPProgressBar;
        [SerializeField] private Text m_HPProgressText;

        private float m_FillAmountStep;
        public float FillAmountStep { get => m_FillAmountStep; set => m_FillAmountStep = value; }

        private void Start()
        {
            m_HPProgressBar.fillAmount = 1;

            var player = Player.Instance;

            if (player != null)
            {
                FillAmountStep = 1f / (float)player.DefaultHitPoints;
                m_HPProgressText.text = player.CurrentHitPoint.ToString();
                player.EventOnUpdateHP?.AddListener(UpdateHPProgress);
            }
        }

        public void UpdateHPProgress(float HPCount)
        {
            m_HPProgressBar.fillAmount = HPCount * m_FillAmountStep;
            m_HPProgressText.text = Player.Instance.CurrentHitPoint.ToString();
        } 
    }
}