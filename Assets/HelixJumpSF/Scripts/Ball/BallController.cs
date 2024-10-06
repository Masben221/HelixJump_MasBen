using UnityEngine;
using UnityEngine.Events;

namespace HelixJump
{
    [RequireComponent(typeof(BallMovement))]

    public class BallController : OnColliderTrigger
    {     
        [SerializeField] private BallMovement movement;

        [HideInInspector] public UnityEvent<SegmentType, bool> CollisionSegment;
        
        protected override void OnOneTriggerEnter(Collider other, bool isKillZone)
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

                /*if (segment.Type == SegmentType.Spike || segment.Type == SegmentType.Piston)
                {
                    movement.Death();
                    segment.GetComponent<MeshCollider>().enabled = false;
                    var childs = segment.GetComponentsInChildren<BoxCollider>();
                    if (childs != null)
                    {
                        for (int i = 0; i < childs.Length; i++)
                        {
                            childs[i].enabled = false;
                        }
                    }
                }*/

                if (segment.Type == SegmentType.Finish)
                {
                    movement.Stop();
                }

                CollisionSegment.Invoke(segment.Type, isKillZone);
            }

            var killZone = other.GetComponent<KillZone>();

            if (killZone == null) killZone = other.GetComponentInParent<KillZone>();

            if (killZone != null)
            {
                if (Player.Instance != null)
                {
                    var player = Player.Instance;
                    player.ApplyDamage(killZone.Damage);                    
                }
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
                }               
            }
        }
    }
}
