using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
	public class RotateAround : MonoBehaviour
	{
		[SerializeField] private Transform m_TargetTransform;
		public Transform TargetTransform { get => m_TargetTransform; set => m_TargetTransform = value; }

		[SerializeField] private Vector3 m_Axis;
		[SerializeField] private float m_Speed;

		void Update()
		{
			if (m_TargetTransform != null) transform.RotateAround(m_TargetTransform.position, m_Axis, m_Speed * Time.deltaTime);
		}
	}
}