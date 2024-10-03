using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public class BallTrail : BallEvent
    {
        [SerializeField] private Transform parentTransform;
        [SerializeField] private MeshRenderer visualMeshRenderer;
        [SerializeField] private Blot blotPrefab;

        private List<Blot> blots = new List<Blot>();
        public List<Blot> Blots => blots;

        protected override void OnBallCollisionSegment(SegmentType type)
        {
            if (type == SegmentType.Default)
            {
                Blot blot = Instantiate(blotPrefab, parentTransform);
                blot.Init(new Vector3(visualMeshRenderer.transform.position.x, transform.position.y, visualMeshRenderer.transform.position.z), visualMeshRenderer.material.color);
                blots.Add(blot);
            }
        }
    }
}
    
