using Cinemachine;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cinemachineVirtualCameras;

    private int frameIndex = 0;

    private int previousPriority;

    private void Awake()
    {
        previousPriority = cinemachineVirtualCameras[0].Priority;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveToNextFrame();
        }
    }

    public void MoveToNextFrame()
    {
        frameIndex++;

        if (frameIndex >= cinemachineVirtualCameras.Length)
            frameIndex = 0;

        previousPriority *= 2;
        cinemachineVirtualCameras[frameIndex].Priority = previousPriority;
    }
}
