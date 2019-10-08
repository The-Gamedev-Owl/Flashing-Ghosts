using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EventHandler : MonoBehaviour
    {
        public void OnPlayButtonClicked()
        {
            SceneManager.LoadScene("Scenes/Level");
        }

        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
