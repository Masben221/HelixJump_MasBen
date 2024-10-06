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
        /// Объект игнорирует повреждения (неуничтожаемый).
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructibe { get => m_Indestructible; set => m_Indestructible = value; }

        /// <summary>
        /// Стартовое количество хитпоинтов.
        /// </summary>
        [SerializeField] private float m_HitPoints;
        public float HitPoints { get => m_HitPoints; set => m_HitPoints = value; }

        /// <summary>
        /// Текущие хиитпоинты.
        /// </summary>
        private float m_CurrentHitPoints;
        public float CurrentHitPoint { get => m_CurrentHitPoints; set => m_CurrentHitPoints = value; }

        /// <summary>
        /// Стартовое количество брони.
        /// </summary>
        [SerializeField] private float m_Shild;
        public float Shild { get => m_Shild; set => m_Shild = value; }

        /// <summary>
        /// Текущая броня.
        /// </summary>
        private float m_CurrentShild;
        public float CurrentShild { get => m_CurrentShild; set => m_CurrentShild = value; }

        /// <summary>
        /// Супер сила какашки.
        /// </summary>
        [SerializeField] private int m_SuperPower;
        public int SuperPower { get => m_SuperPower; set => m_SuperPower = value; }

        /// <summary>
        /// Текущий дамаг.
        /// </summary>
        public float CurrentDamage;

        /// <summary>
        /// Событиe смерти объекта.
        /// </summary>
        public event Action OnDie;       

        /// <summary>
        /// Событиe старта после смерти.
        /// </summary>
        public event Action OnStart;        
        
        /// <summary>
        /// Событиe меню продолжения после смерти.
        /// </summary>
        public event Action OnContinueMenu;        
        
        /// <summary>
        /// Событиe проигрыша.
        /// </summary>
        public event Action OnLose;
        
        /// <summary>
        /// Событиe выигрыша.
        /// </summary>
        public event Action OnFinish;

        /// <summary>
        /// Событие обновления UI HP.
        /// </summary>
        [SerializeField] private UnityEvent<float> m_EventOnUpdateHP;
        public UnityEvent<float> EventOnUpdateHP => m_EventOnUpdateHP;

        /// <summary>
        /// Событие обновления UI Armor.
        /// </summary>
        [SerializeField] private UnityEvent<float> m_EventOnUpdateShild;
        public UnityEvent<float> EventOnUpdateShild => m_EventOnUpdateShild;
        
        /// <summary>
        /// Событие обновления UI Armor.
        /// </summary>
        [SerializeField] private UnityEvent<int> m_EventOnUpdateSuperPower;
        public UnityEvent<int> EventOnUpdateSuperPower => m_EventOnUpdateSuperPower;

        /// <summary>
        /// Событие повреждения.
        /// </summary>        
        public event Action OnDamage;

        //Ссылка на эффекты после уничтожения.
        [SerializeField] private GameObject m_ImpactEffect;

        //Ссылка на текст урона.
        [SerializeField] private DamageStats m_DamageStatsPrefab;

        [SerializeField] private GameObject m_ShieldParticle;
        public GameObject ShieldParticle => m_ShieldParticle;

        [SerializeField] private GameObject m_SuperPowerParticle;
        public GameObject SuperPowerParticle => m_SuperPowerParticle;

        [SerializeField] private int m_MaxLives; //Максимальное колличество жизней.

        private int m_NumLives; // текущее количество жизней.
        public int NumLives => m_NumLives; //Ссылка на текущее количество жизней.

        [SerializeField] private LivesUI m_LivesUI; //Ссылка на UI отображения колличества жизней.

        //[SerializeField] private UIHPProgress m_UIHPProgress; //UI уровня HP.
        //[SerializeField] private UIShieldProgress m_UIShieldProgress;//UI уровня щита.
        //[SerializeField] private UISuperPowerProgress m_UISuperPowerProgress;//UI уровня супер силы.

        #endregion
        #region Unity Events
        protected override void Awake()
        {
            base.Awake();
            m_NumLives = m_MaxLives;
            m_LivesUI.Setup(m_NumLives);
            KakaStart();
        }

        private void Start()
        {
            
            
            
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
            m_CurrentHitPoints = m_HitPoints;
            m_CurrentShild = m_Shild;
            m_SuperPower = 0;
            EventOnUpdateHP?.Invoke(m_CurrentHitPoints);
            EventOnUpdateShild?.Invoke(m_CurrentShild);
            EventOnUpdateSuperPower?.Invoke(m_SuperPower);
            
            OnStart?.Invoke();
            Invoke(nameof(SetIndestructible), 2.0f);
        }
        private void SetIndestructible()
        {
            m_Indestructible = false;
        }
        public void KakaFinish()
        {            
            OnFinish?.Invoke();           
        }

        /// <summary>
        /// Применение дамага к объекту.
        /// </summary>
        /// <param name="damage"> Урон наносимый объекту </param>

        public void ApplyDamage(float damage)
        {
            if (m_Indestructible) return;

            if (damage <= m_CurrentShild) m_CurrentShild -= damage;
            else if (damage > m_CurrentShild)
            {
                m_CurrentShild = 0;
                m_CurrentHitPoints -= damage - m_CurrentShild;
            }

            EventOnUpdateHP?.Invoke(m_CurrentHitPoints);
            EventOnUpdateShild?.Invoke(m_CurrentShild);
            OnDamage?.Invoke();

            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
                return; // чтобы 0 не показывало
            }

            if (m_DamageStatsPrefab != null)
            {
                CurrentDamage = damage;
                Instantiate(m_DamageStatsPrefab, transform.position, Quaternion.identity);                
            }
        }

        /// <summary>
        /// Переопределяемое событие уничтожения объекта, когда хитпоинты ниже нуля
        /// </summary>
        public void OnDeath()
        {
            if (m_ImpactEffect != null) Instantiate(m_ImpactEffect, transform.position, Quaternion.identity);

            m_Indestructible = true;

            var pos = gameObject.transform.position;

            OnDie?.Invoke();

            m_NumLives--;

            m_LivesUI.UpdateLivesUI(m_NumLives);

            if (m_NumLives > 0)
            {
                OnContinueMenu?.Invoke();
            }
            else
            {
                OnLose?.Invoke();
            }
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