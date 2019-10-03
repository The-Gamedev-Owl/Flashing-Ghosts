using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EventHandler : MonoBehaviour
    {
        public void OnPlayButtonClicked()
        {
            // TODO: Update with the actual scene level
            // SceneManager.LoadScene("Level");
        }

        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
