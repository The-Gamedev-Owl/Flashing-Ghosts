using System;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Strength of the floating effect")]
    private float floatStrength = 1.0f;
    [SerializeField]
    [Tooltip("Time offset of the effect")]
    private float offset = 0f;
    private float originalY;
    void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time + offset) * floatStrength),
            transform.position.z);
    }
}
