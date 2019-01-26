using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    private new Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        throw new NotImplementedException();
    }
}
