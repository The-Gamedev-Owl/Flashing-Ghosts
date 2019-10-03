using UnityEngine;

public class QuitOnEscape : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("Escape"))
        {
            Application.Quit();
        }
    }
}
