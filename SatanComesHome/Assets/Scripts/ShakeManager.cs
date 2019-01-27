using Cinemachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShakeManager : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1, yDown = 1;
    [SerializeField] private NoiseSettings shakeProfile;

    CinemachineVirtualCamera virtualCamera;
    [SerializeField] CinemachineVirtualCamera droppedVCam;
    [SerializeField] Transform emptyLookAtTransform;

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

    public void ShakeCinemachineLerp(Collectable droppedObject)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        StartCoroutine(ShakeVC(droppedObject));
    }

    private IEnumerator ShakeVC(Collectable droppedObject)
    {
        emptyLookAtTransform.position = Vector3.Scale(droppedObject.transform.position,new Vector3(1,1,0)) - new Vector3(0, yDown, 0);

        //droppedVCam.Follow = droppedObject.transform;
        //droppedVCam.LookAt = droppedObject.transform;
        droppedVCam.Priority = 100;

        yield return new WaitForSeconds(shakeDuration);

        droppedVCam.Priority = 0;
    }


    private IEnumerator ShakeIt()
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = shakeProfile;

        yield return new WaitForSeconds(shakeDuration);

        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = null;
    }
}
