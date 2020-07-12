using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followedObject;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float interpolationPerSecond;

    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        float z = position.z;
        position = Vector2.Lerp(position, (Vector2)followedObject.position + offset, interpolationPerSecond * Time.fixedDeltaTime);
        position.z = z;
        transform.position = position;
    }
}
