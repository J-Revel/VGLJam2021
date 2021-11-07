using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    public static PostProcessController instance;
    public Volume postProcessVolume;
    private LensDistortion distortionFX;

    private float _distortion = 0;
    public float distortion { set {
        if(value != _distortion)
        {
            _distortion = value;
            distortionFX.intensity.SetValue(new FloatParameter(value));
        }
    }}

    void Awake()
    {
        instance = this;
        postProcessVolume = GetComponent<Volume>();
        postProcessVolume.profile.TryGet<LensDistortion>(out distortionFX);
    }

}
