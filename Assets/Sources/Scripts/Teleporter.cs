using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
	[SerializeField]
	private Teleporter Target = null;

	[HideInInspector]
	public bool IsReceive = false;

	void OnTriggerEnter(Collider col)
	{
		if (IsReceive == true)
		{
			return;
		}

		if (col.CompareTag("Player") == true)
		{
			Target.IsReceive = true;

			var cc = col.GetComponent<CharacterController>();
			cc.enabled = false;
			col.transform.position = Target.transform.position;
			cc.enabled = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Player") == true)
		{
			IsReceive = false;
		}
	}


}
