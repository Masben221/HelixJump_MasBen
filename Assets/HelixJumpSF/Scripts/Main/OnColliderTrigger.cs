using UnityEngine;

namespace HelixJump
{
    public abstract class OnColliderTrigger : MonoBehaviour
    {
        private Collider lastCollider;

        protected virtual void OnOneTriggerEnter(Collider other, bool isKillZone) { }       

        private void OnTriggerEnter(Collider other)
        {
            if (lastCollider != null && lastCollider != other) return;

            lastCollider = other;
            bool isKillZone = other.TryGetComponent(out KillZone _);

            OnOneTriggerEnter(other, isKillZone);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (lastCollider == other)
                lastCollider = null;
        }
    }

}
