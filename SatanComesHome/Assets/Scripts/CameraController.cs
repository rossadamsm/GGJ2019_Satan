using UnityEngine;

public class CameraController : MonoBehaviour
{
    private new Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }
}
