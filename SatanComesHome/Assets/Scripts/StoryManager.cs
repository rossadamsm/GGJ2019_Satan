using Cinemachine;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cinemachineVirtualCameras;

    private AudioSource pageTurnSource;

    private int frameIndex = 0;
    private int previousPriority;

    private void Awake()
    {
        previousPriority = cinemachineVirtualCameras[0].Priority;
        pageTurnSource = GetComponent<AudioSource>();
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

        pageTurnSource.Play();

        if (frameIndex >= cinemachineVirtualCameras.Length)
            frameIndex = 0;

        previousPriority *= 2;
        cinemachineVirtualCameras[frameIndex].Priority = previousPriority;
    }
}
