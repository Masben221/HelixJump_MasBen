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
			if (col.GetComponentInParent<Player>() == true)
			{
				//col.GetComponentInParent<Player>().ApplyDamage(m_Damage);
				//Debug.Log("damage!");
			}
		}

	}

}
