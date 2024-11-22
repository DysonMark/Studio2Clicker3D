using System;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float cameraSpeed;
    private Vector3 currentVelocity = Vector3.zero;
    private void Awake()
    {
        offset = transform.position - playerPosition.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = playerPosition.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, playerPosition.position, ref currentVelocity, cameraSpeed);
    }
}
