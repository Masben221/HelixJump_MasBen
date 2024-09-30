using UnityEngine;
using UnityEngine.SceneManagement;


public class KillZone : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player") == true)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

}
