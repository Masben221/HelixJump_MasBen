using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SegmentType
{
    Default,
    Trap,
    Empty,
    Finish
}

[RequireComponent(typeof(MeshRenderer))]
public class Segment : MonoBehaviour
{
    [SerializeField] private Material trapMaterial;
    [SerializeField] private Material finishMaterial;

    [SerializeField] private SegmentType _type;

    public SegmentType Type => _type;

    private MeshRenderer meshRender;

    private void Awake()
    {
        meshRender = GetComponent<MeshRenderer>();
    }

    public void SetTrap()
    {
        meshRender.enabled = true;
        meshRender.material = trapMaterial;

        _type = SegmentType.Trap;
    }
    public void SetEmpty()
    {
        meshRender.enabled = false;
        _type = SegmentType.Empty;

    }
    public void SetFinish()
    {
        meshRender.enabled = true;
        meshRender.material = finishMaterial;

        _type = SegmentType.Finish;
    }
}
