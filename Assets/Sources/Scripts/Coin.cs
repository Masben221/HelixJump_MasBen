using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{
	[SerializeField]
	private float RotationMovement = 0;

	void Update()
	{
		transform.Rotate(0, RotationMovement * Time.deltaTime, 0);
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player") == true)
		{
			Destroy(gameObject);
		}
	}

}
