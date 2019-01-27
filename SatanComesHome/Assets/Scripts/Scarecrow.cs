﻿using System.Collections;
using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    private float lifeTime = 3f;

    private PriestAI[] priests;

    private void Start()
    {
        priests = FindObjectsOfType<PriestAI>();

        for (int i = 0; i < priests.Length; i++)
        {
            priests[i].scarecrows.Add(this);
        }

        StartCoroutine(DisableAfterLifeTime());
    }

    private IEnumerator DisableAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        enabled = false;
    }

    private void OnDisable()
    {
        for (int i = 0; i < priests.Length; i++)
        {
            priests[i].scarecrows.Remove(this);
        }
    }

}
