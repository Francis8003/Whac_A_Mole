using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace WhacA_mole
{
    public class GameOver : MonoBehaviour
    {
        public Text text_Content;

        public void Init(string texts)
        {
            text_Content.text = texts;
        }

        public void OnCloseBtnClick()
        {
            SceneManager.LoadScene("Main");
        }

        public void OnReloadBtnClick()
        {
            SceneManager.LoadScene("Main");
        }
    }
}
