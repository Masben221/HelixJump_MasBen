using UnityEngine;

namespace HelixJump
{
    public enum SegmentType
    {
        Default,
        Trap,
        Empty,
        Finish,
        Bonus,
        Fan
    }

    [RequireComponent(typeof(MeshRenderer))]
    public class Segment : MonoBehaviour
    {
        [SerializeField] private Material m_TrapMaterial;
        [SerializeField] private Material m_FinishMaterial;
        [SerializeField] private Material m_FanMaterial;
        [SerializeField] private Material m_BonusMaterial;

        [SerializeField] private SegmentType m_Type;

        [SerializeField] private GameObject[] m_Bonus;

        [SerializeField] private GameObject[] m_Traps;
        
        [SerializeField] private GameObject m_Default;

        [SerializeField] private GameObject m_Fan;

        public SegmentType Type { get => m_Type; set => m_Type = value; }

        private MeshRenderer meshRender;

        private void Awake()
        {
            meshRender = GetComponent<MeshRenderer>();

            foreach (var bonus in m_Bonus)
            { 
                bonus.SetActive(false);
            }
            
            foreach (var trap in m_Traps)
            { 
                trap.SetActive(false);
            }            

            m_Fan.SetActive(false);
            m_Default.SetActive(false);
        }
        
        public void SetTrap()
        {            
            meshRender.enabled = true;
            meshRender.material = m_TrapMaterial;

            int index = Random.Range(0, m_Traps.Length);
            m_Traps[index].SetActive(true);

            m_Type = SegmentType.Trap;
        }
        public void SetBonus()
        {            
            meshRender.enabled = true;
            meshRender.material = m_BonusMaterial;

            int index = Random.Range(0, m_Bonus.Length);            
            m_Bonus[index].SetActive(true);
            
            m_Type = SegmentType.Bonus;
        }
        public void SetFan()
        {
            meshRender.enabled = true;
            meshRender.material = m_FanMaterial;
            m_Fan.SetActive(true);
            m_Type = SegmentType.Fan;
        }

        public void SetEmpty()
        {
            meshRender.enabled = false;
            m_Type = SegmentType.Empty;
        }
        public void SetFinish()
        {
            meshRender.enabled = true;
            meshRender.material = m_FinishMaterial;

            m_Type = SegmentType.Finish;
        }
        public void SetDefault()
        {
            meshRender.enabled = true;
            m_Type = SegmentType.Default;
            m_Default.SetActive(true);
        }
    }
}
