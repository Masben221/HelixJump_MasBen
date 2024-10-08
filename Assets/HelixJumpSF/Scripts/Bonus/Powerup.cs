using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    [RequireComponent(typeof(SphereCollider))] 
    public abstract class Powerup : MonoBehaviour
    {
        // префаб звука при подборе
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        private void OnTriggerEnter(Collider collision)
        {
            var player = collision.GetComponentInParent<Player>();

            if (player != null)
            {
                OnPickedUp(player);
                if (m_ImpactEffectPrefab != null) Instantiate(m_ImpactEffectPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        protected abstract void OnPickedUp(Player player);
    }
}

