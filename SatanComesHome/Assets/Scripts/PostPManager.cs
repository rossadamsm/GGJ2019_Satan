using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostPManager : MonoBehaviour
{
    public Camera view;
    public PostProcessProfile earthprofile;
    public PostProcessProfile hellprofile;

    // Start is called before the first frame update
    void Start()
    {
        satan = FindObjectOfType<CharacterController>();
        view = Camera.main;
    }

    public void gotoEarth()
    {
        PostProcessVolume effects = view.GetComponent<PostProcessVolume>();
        effects.profile = earthprofile;
    }

    public void gotoHell()
    {
        PostProcessVolume effects = view.GetComponent<PostProcessVolume>();
        effects.profile = hellprofile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
