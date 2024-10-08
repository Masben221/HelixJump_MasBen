using UnityEngine;
using UnityEngine.SceneManagement;

namespace HelixJump
{
	public class KillZone : MonoBehaviour
	{
		[SerializeField] private float m_Damage = 1;
		public float Damage => m_Damage;
       
        void OnTriggerEnter(Collider col)
		{
			var player = col.GetComponentInParent<Player>();

			if (  player != null && player.CurrentHitPoint > 0 && TryGetComponent(out SoundHook soundHit))
			{
				soundHit.Play();
			}
		}
	}
}
