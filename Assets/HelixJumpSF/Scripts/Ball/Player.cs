using System;
using UnityEngine.Events;
using UnityEngine;

namespace HelixJump
{
    public class Player : MonoSingleton<Player>
    {
        [SerializeField] private BallController m_BallController;
        public BallController BallController => m_BallController;

        #region Properties
        /// <summary>
        /// ������ ���������� ����������� (��������������).
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructibe { get => m_Indestructible; set => m_Indestructible = value; }
        
        [SerializeField] private int m_MaxLives; //������������ ����������� ������.

        [SerializeField] private int m_NumLives = 2; // ������� ���������� ������.
        private int m_DefaultNumLives;
        public int NumLives => m_NumLives; //������ �� ������� ���������� ������.

        [SerializeField] private LivesUI m_LivesUI; //������ �� UI ����������� ����������� ������.
        
        /// <summary>
        /// ��������� ���������� ����������.
        /// </summary>
        [SerializeField] private float m_HitPoints = 5;
        public float HitPoints { get => m_HitPoints; set => m_HitPoints = value; }

        /// <summary>
        /// ������� ����������.
        /// </summary>
        [SerializeField] private float m_CurrentHitPoints = 3;
        public float CurrentHitPoint { get => m_CurrentHitPoints; set => m_CurrentHitPoints = value; }
        private float m_DefaultHitPoints;
        /// <summary>
        /// ��������� ���������� �����.
        /// </summary>
        [SerializeField] private float m_MaxShield = 5;
        public float MaxShield { get => m_MaxShield; set => m_MaxShield = value; }

        /// <summary>
        /// ������� �����.
        /// </summary>
        [SerializeField] private float m_CurrentShild = 1;
        public float CurrentShield { get => m_CurrentShild; set => m_CurrentShild = value; }
        private float m_DefaultShield;

        /// <summary>
        /// ������������ ����� ���� �������.
        /// </summary>
        [SerializeField] private int m_MaxSuperPower = 5;
        public int MaxSuperPower { get => m_MaxSuperPower; set => m_MaxSuperPower = value; }
        /// <summary>
        /// ����� ���� �������.
        /// </summary>
        [SerializeField] private int m_SuperPower = 0;
        public int SuperPower { get => m_SuperPower; set => m_SuperPower = value; }        
        
        private int m_DefaultSuperPower;
        /// <summary>
        /// ������� �����.
        /// </summary>
        public float CurrentDamage;

        /// <summary>
        /// ������e ������.
        /// </summary>
        public event Action OnDie;       

        /// <summary>
        /// ������e ������ ����� ������.
        /// </summary>
        public event Action OnStart;  
        
        /// <summary>
        /// ������e �����.
        /// </summary>
        public event Action OnPause;        
        
        /// <summary>
        /// ������e ������������� ���������.
        /// </summary>
        public event Action OnSuperPower;        
        
        /// <summary>
        /// ������e ������������� ����.
        /// </summary>
        public event Action OnShield;        
        
        /// <summary>
        /// ������e ���������.
        /// </summary>
        //public event Action OnLose;
        
        /// <summary>
        /// ������e ��������.
        /// </summary>
        public event Action OnFinish;

        /// <summary>
        /// ������� ���������� UI HP.
        /// </summary>
        [SerializeField] private UnityEvent<float> m_EventOnUpdateHP;
        public UnityEvent<float> EventOnUpdateHP => m_EventOnUpdateHP;

        /// <summary>
        /// ������� ���������� UI Armor.
        /// </summary>
        [SerializeField] private UnityEvent<float> m_EventOnUpdateShield;
        public UnityEvent<float> EventOnUpdateShield => m_EventOnUpdateShield;
        
        /// <summary>
        /// ������� ���������� UI Armor.
        /// </summary>
        [SerializeField] private UnityEvent<int> m_EventOnUpdateSuperPower;
        public UnityEvent<int> EventOnUpdateSuperPower => m_EventOnUpdateSuperPower;
        
        /// <summary>
        /// ������� ���������� UI ������.
        /// </summary>
        [SerializeField] private UnityEvent<int> m_EventOnUpdateNumLives;
        public UnityEvent<int> EventOnUpdateNumLives => m_EventOnUpdateNumLives;

        /// <summary>
        /// ������� �����������.
        /// </summary>        
        public event Action OnDamage;

        //������ �� ������� ����� �����������.
        [SerializeField] private GameObject m_ImpactEffect;

        //������ �� ����� �����.
        [SerializeField] private DamageStats m_DamageStatsPrefab;

        [SerializeField] private GameObject m_ShieldParticle;
        public GameObject ShieldParticle => m_ShieldParticle;

        [SerializeField] private GameObject m_SuperPowerParticle;
        public GameObject SuperPowerParticle => m_SuperPowerParticle;        

        #endregion
        #region Unity Events
        protected override void Awake()
        {
            base.Awake();

            m_DefaultNumLives = m_NumLives;
            m_DefaultHitPoints = m_CurrentHitPoints;
            m_DefaultShield = m_CurrentShild;
            m_DefaultSuperPower = m_SuperPower;

            m_LivesUI.Setup(m_MaxLives);

            //KakaStart();

            OnSuperPower += UpdateIndestructible;
        }       

        private void Update()
        {
            ActionShield();
            ActionSuperPower();
        }

        #endregion

        #region Public API

        public void KakaStart()
        {
            m_NumLives = m_DefaultNumLives;            
            m_CurrentHitPoints = m_DefaultHitPoints;
            m_CurrentShild = m_DefaultShield;
            m_SuperPower = m_DefaultSuperPower;

            EventOnUpdateHP?.Invoke(m_CurrentHitPoints);
            EventOnUpdateShield?.Invoke(m_CurrentShild);
            EventOnUpdateSuperPower?.Invoke(m_SuperPower);
            
            OnStart?.Invoke();
            Invoke(nameof(SetIndestructibleFalse), 3.0f);            
        }
        private void SetIndestructibleFalse()
        {
            m_Indestructible = false;
        }
        public void UpdateIndestructible()
        {
            if (m_SuperPower > 0) m_Indestructible = true;
            else m_Indestructible = false;
        }
        public void EventPause()
        {
           OnPause?.Invoke();
        } 

        public void EventSuperPower()
        {
            OnSuperPower?.Invoke();
            m_Indestructible = true;
        }
        public void EventFinish()
        {            
            OnFinish?.Invoke();           
        }

        public void AddShield(float shield)
        {
            m_CurrentShild = Mathf.Clamp(m_CurrentShild + shield, 0, m_MaxShield);
            m_EventOnUpdateShield?.Invoke(m_CurrentShild);

        }
        public void AddHP(float hp)
        {
            CurrentHitPoint = Mathf.Clamp(CurrentHitPoint + hp, 0, m_HitPoints);
            EventOnUpdateHP?.Invoke(m_CurrentHitPoints);
        }
        public void AddSuperPower(int sup)
        {
            SuperPower = Mathf.Clamp(SuperPower + sup, 0, m_MaxSuperPower);            
            EventOnUpdateSuperPower?.Invoke(m_SuperPower);
            if (m_SuperPower <= 0) SetIndestructibleFalse();
        }
         public void AddLifes(int life)
        {
            m_NumLives = Mathf.Clamp(m_NumLives + life, 0, m_MaxLives);
            EventOnUpdateNumLives?.Invoke(m_NumLives);  
        }

        /// <summary>
        /// ���������� ������ � �������.
        /// </summary>
        /// <param name="damage"> ���� ��������� ������� </param>

        public void ApplyDamage(float damage)
        {
            if (m_Indestructible) return;

            if (damage <= m_CurrentShild) 
            { 
                AddShield (-damage);
                OnShield?.Invoke();
            }
            else if (damage > m_CurrentShild)
            {                
                AddHP(- damage + m_CurrentShild);
                OnDamage?.Invoke();
            }

            //EventOnUpdateHP?.Invoke(m_CurrentHitPoints);
            //EventOnUpdateShield?.Invoke(m_CurrentShild);            

            if (m_CurrentHitPoints <= 0)
            {
                m_Indestructible = true;
                OnDeath();
                return; // ����� 0 �� ����������
            }

            if (m_DamageStatsPrefab != null)
            {
                CurrentDamage = damage;
                Instantiate(m_DamageStatsPrefab, transform.position, Quaternion.identity);                
            }
        }

        /// <summary>
        /// ���������������� ������� ����������� �������, ����� ��������� ���� ����
        /// </summary>
        public void OnDeath()
        {
            if (m_ImpactEffect != null) Instantiate(m_ImpactEffect, transform.position, Quaternion.identity);                        

            //var pos = gameObject.transform.position;

            OnDie?.Invoke();

            AddLifes(-1);            
        }

        private void ActionShield()
        {
            if (m_CurrentShild > 0)
            {
                m_ShieldParticle.SetActive(true);
            }

            else
            {
                m_ShieldParticle.SetActive(false);            
            }
        }
        
        private void ActionSuperPower()
        {
            if (m_SuperPower > 0)
            {
                m_SuperPowerParticle.SetActive(true);                
            }

            else
            {
                m_SuperPowerParticle.SetActive(false);                
            }
        }

        #endregion
    }
}