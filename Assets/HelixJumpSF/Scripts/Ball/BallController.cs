using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HelixJump
{
    [RequireComponent(typeof(BallMovement))]

    public class BallController : OnColliderTrigger
    {
        [SerializeField] private BallMovement movement;

        [HideInInspector] public UnityEvent<SegmentType> CollisionSegment;

        private void Start()
        {
            if (movement == null) movement = GetComponent<BallMovement>();
        }

        protected override void OnOneTriggerEnter(Collider other)
        {
            Segment segment = other.GetComponent<Segment>();

            if (segment == null) segment = other.GetComponentInParent<Segment>();

            if (segment != null)
            {
                if (segment.Type == SegmentType.Empty)
                {
                    movement.enabled = true;
                    movement.Fall(other.transform.position.y);
                    segment.GetComponent<MeshCollider>().enabled = false;
                }

                if (segment.Type == SegmentType.Default)
                {
                    movement.Jump();
                }

                if (segment.Type == SegmentType.Fan)
                {
                    movement.Fly();
                }

                if (segment.Type == SegmentType.Spike || segment.Type == SegmentType.Piston)
                {
                    movement.Death();
                    segment.GetComponent<MeshCollider>().enabled = false;
                }

                if (segment.Type == SegmentType.Finish)
                {
                    movement.Stop();
                }

                CollisionSegment.Invoke(segment.Type);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            Segment segment = other.GetComponent<Segment>();

            if (segment == null) segment = other.GetComponentInParent<Segment>();           

            if (segment != null)
            {
                if (segment.Type == SegmentType.Fan)
                {
                    movement.Fly();                    
                }               
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);

            Segment segment = other.GetComponent<Segment>();

            if (segment == null) segment = other.GetComponentInParent<Segment>();            

            if (segment != null)
            {
                if (segment.Type == SegmentType.Fan)
                {
                    movement.Jump();
                    //movement.FlyStop();
                }
                if (segment.Type == SegmentType.Default)
                {
                    // movement.JumpStop();
                }
            }
        }
    }
}
