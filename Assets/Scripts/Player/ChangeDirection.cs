using UnityEngine;

public class ChangeDirection : MonoBehaviour
{
    private Animator animator;
    private GameObject lights;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lights = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        RotateLights();
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        float lightsRotation = lights.transform.eulerAngles.z;

        if (lightsRotation <= 45 || lightsRotation > 315)
            SetDirection("Left");
        else if (lightsRotation > 45 && lightsRotation <= 135)
            SetDirection("Down");
        else if (lightsRotation > 135 && lightsRotation <= 225)
            SetDirection("Right");
        else if (lightsRotation > 225 && lightsRotation <= 315)
            SetDirection("Up");
    }

    private void SetDirection(string direction)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.SetBool(parameter.name, false);
        }
        animator.SetBool(direction, true);
    }

    private void RotateLights()
    {
        float newAngle;
        Vector3 newRotation = new Vector3(0, 0);

        newAngle = GetNewAngle();
        newRotation.z = newAngle - 180; // Allows to face mouse position instead of being at the opposite
        lights.transform.eulerAngles = new Vector3(0, 0, newAngle - 180);
    }

    private float GetNewAngle()
    {
        Vector3 object_pos;
        Vector3 mouse_pos;

        mouse_pos = Input.mousePosition;
        object_pos = Camera.main.WorldToScreenPoint(lights.transform.position);
        mouse_pos.x -= object_pos.x;
        mouse_pos.y -= object_pos.y;
        return Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
    }
}
