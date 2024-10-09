using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public class LivesUI : MonoBehaviour
    {
        [SerializeField] private GameObject m_LivesUIPrefabe;

        private List<GameObject> LivesIcons = new List<GameObject>();

        public void Setup(int maxLives)
        {
            for (int i = 0; i < maxLives; i++)
            {
                GameObject newIcon = Instantiate(m_LivesUIPrefabe, transform);

                LivesIcons.Add(newIcon);               
            }
            var lives = Player.Instance.NumLives;
            UpdateLivesUI(lives);
            Player.Instance.EventOnUpdateNumLives?.AddListener(UpdateLivesUI);
        }

        public void UpdateLivesUI(int lives)
        {
            for (int i = 0; i < LivesIcons.Count; i++)
            {
                if (i < lives)
                {
                    LivesIcons[i].SetActive(true);
                }
                else
                {
                    LivesIcons[i].SetActive(false);
                }
            }
        }
    }
}

