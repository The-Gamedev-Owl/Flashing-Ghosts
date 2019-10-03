using UnityEngine;

public class QuitOnEscape : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey ("escape"))
        {
            Application.Quit();
        }
    }
}
