using UnityEngine;
using UnityEngine.SceneManagement;

namespace HelixJump
{
	public class KillZone : MonoBehaviour
	{
		[SerializeField] private float m_Damage = 1;
		public float Damage => m_Damage;

		// префаб взрыва при касании
		[SerializeField] private ImpactEffect m_ImpactEffectPrefab;

		void OnTriggerEnter(Collider col)
		{
			var player = col.GetComponentInParent<Player>();

			if (player != null && player.CurrentHitPoint > 0)
			{
				if (player.SuperPower > 0) 
				{ 
					Destroy(gameObject);
					return;
				}
				if (TryGetComponent(out SoundHook soundHit)) soundHit.Play();
				if (m_ImpactEffectPrefab != null) Instantiate(m_ImpactEffectPrefab, transform.position, Quaternion.identity);
			}
		}
	}
}
