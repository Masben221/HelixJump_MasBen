using UnityEngine;
using UnityEngine.SceneManagement;

namespace HelixJump
{
    public class SceneHelper : MonoBehaviour
    {
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadLevel(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
        public void ScenePause(int buildIndex)
        {
            SceneManager.LoadScene(buildIndex);
        }
    }
}

