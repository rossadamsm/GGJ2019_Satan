using Cinemachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShakeManager : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1;
    [SerializeField] private NoiseSettings shakeProfile;

    CinemachineVirtualCamera virtualCamera;

    private Coroutine shakeRoutine;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera()
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        StartCoroutine(ShakeIt());
    }

    private IEnumerator ShakeIt()
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = shakeProfile;

        yield return new WaitForSeconds(shakeDuration);

        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = null;
    }
}
