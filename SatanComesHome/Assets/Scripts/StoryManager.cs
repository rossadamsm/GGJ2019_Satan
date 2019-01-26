﻿using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cinemachineVirtualCameras;

    private AudioSource pageTurnSource;

    private int frameIndex = 0;
    private int previousPriority;
    public AudioClip clip;

    private void Awake()
    {
        previousPriority = cinemachineVirtualCameras[0].Priority;
        pageTurnSource = GetComponent<AudioSource>();
        SoundManager.instance.PlayLoop(clip);
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
            SceneManager.LoadScene("Main");
            //frameIndex = 0;

        previousPriority *= 2;
        cinemachineVirtualCameras[frameIndex].Priority = previousPriority;
    }
}
