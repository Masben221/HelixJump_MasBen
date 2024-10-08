using UnityEngine;
using UnityEngine.Events;

namespace HelixJump
{
    public class SuperPowerTrigger : OnColliderTrigger
    {                  
        protected override void OnOneTriggerEnter(Collider other, bool isKillZone)
        {
            Segment segment = other.GetComponent<Segment>();            

            if (segment != null)
            {
                if (segment.Type == SegmentType.Piston || segment.Type == SegmentType.Default || segment.Type == SegmentType.Fan || segment.Type == SegmentType.Spike)
                {
                    segment.Type = SegmentType.Empty;
                    Player.Instance.AddSuperPower(-1);
                    Player.Instance.EventSuperPower();
                }
                if (segment.Type == SegmentType.Finish)
                {
                    Player.Instance.AddSuperPower(-100);
                }
            }           
        }       
    }
}
