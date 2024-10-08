using System.Collections.Generic;
using UnityEngine;

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

        [SerializeField] private float m_Value;
        
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