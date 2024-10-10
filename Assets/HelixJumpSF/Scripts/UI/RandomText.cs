using UnityEngine;
using UnityEngine.UI;

namespace HelixJump
{
    public class RandomText : MonoBehaviour
    {
        private Text[] m_Texts;

        private void Start()
        {            
            if (Player.Instance != null)
            {
                var player = Player.Instance;
                player.OnStart += NewText;                
            }

            m_Texts = GetComponentsInChildren<Text>();
            NewText();
        } 
        
        private void NewText()
        {           
            foreach (var text in m_Texts)
            {
                text.gameObject.SetActive(false);
            }

            int index = Random.Range(0, m_Texts.Length);
            m_Texts[index].gameObject.SetActive(true);
        }
    }
}

