using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour 
{
	[SerializeField]
	private UnityEvent OnTrigger = null;

	void OnTriggerEnter(Collider col)
	{

		if(col.CompareTag("Player") == true)
		{
			OnTrigger.Invoke();
		}
	}
}
