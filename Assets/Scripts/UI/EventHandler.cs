using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EventHandler : MonoBehaviour
    {
        [SerializeField]
        private Animator transitionAnimator = null;
        public void OnPlayButtonClicked()
        {
            StartCoroutine(LoadSceneAfterTransition());
        }

        private IEnumerator LoadSceneAfterTransition()
        {
            transitionAnimator.SetTrigger("AnimateOut");
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Scenes/Level");
        }

        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
