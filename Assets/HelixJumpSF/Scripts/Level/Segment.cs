using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SegmentType
{
    Default,
    Spike,
    Empty,
    Finish,
    Piston,
    Fan
}

[RequireComponent(typeof(MeshRenderer))]
public class Segment : MonoBehaviour
{
    [SerializeField] private Material m_SpikeMaterial;
    [SerializeField] private Material m_FinishMaterial;
    [SerializeField] private Material m_FanMaterial;
    [SerializeField] private Material m_PistonMaterial;

    [SerializeField] private SegmentType m_Type;
    
    [SerializeField] private GameObject m_Fan;
    [SerializeField] private GameObject m_Spike;
    [SerializeField] private GameObject m_Piston;    

    public SegmentType Type => m_Type;

    private MeshRenderer meshRender;

    private void Awake()
    {
        meshRender = GetComponent<MeshRenderer>();
        m_Fan.SetActive(false);
        m_Spike.SetActive(false);
        m_Piston.SetActive(false);
    }

    public void SetSpike()
    {
        meshRender.enabled = true;
        meshRender.material = m_SpikeMaterial;
        m_Spike.SetActive(true);
        m_Type = SegmentType.Spike;
    }
    public void SetPiston()
    {
        meshRender.enabled = true;
        meshRender.material = m_PistonMaterial;
        m_Piston.SetActive(true);
        m_Type = SegmentType.Piston;
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


}
