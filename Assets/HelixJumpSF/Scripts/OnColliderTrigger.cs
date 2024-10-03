using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public abstract class OnColliderTrigger : MonoBehaviour
    {
        private Collider lastCollider;

        protected virtual void OnOneTriggerEnter(Collider other) { }

        private void OnTriggerEnter(Collider other)
        {
            if (lastCollider != null && lastCollider != other) return;

            lastCollider = other;

            OnOneTriggerEnter(other);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (lastCollider == other)
                lastCollider = null;
        }
    }

}
