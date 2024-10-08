using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HelixJump
{
    public enum EffectType
    {
        AddSuperPower,
        AddLife,
        AddShiled,
        AddHP
    }
    public class PowerupStats : Powerup
    {
        [SerializeField] private EffectType m_EffectType;
        public EffectType EffectType => m_EffectType;
        
        [SerializeField] private int m_MinValue;
        [SerializeField] private int m_MaxValue;
        
        private float m_Value;

        [SerializeField] private Text m_ValueText;


        private void Start()
        {
            m_Value = Random.Range(m_MinValue, m_MaxValue);
            m_ValueText.text = m_Value.ToString();
        }
        //ƒобавл€ет бонусы в зависимоти от их типа.
        protected override void OnPickedUp(Player player)
        {
            if (m_EffectType == EffectType.AddSuperPower)
                player.AddSuperPower((int)m_Value);
            if (m_EffectType == EffectType.AddLife)
                player.AddLifes((int)m_Value);
            if (m_EffectType == EffectType.AddShiled)
                player.AddShield((int)m_Value);
            if (m_EffectType == EffectType.AddHP)
                player.AddHP((int)m_Value);            
        }        
    }
}