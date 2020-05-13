using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEffectController : MonoBehaviour
{

    public Material[] Materials;
    public Material Skybox, Player;
    public float ColorShiftFactor, SkyboxShiftFactor;

    private Color HSVColor = Color.clear;
    private float ColorBalance, SkyboxRotation;

    void Update()
    {
        UpdateColor(ColorShiftFactor);
        RotateSkybox(SkyboxShiftFactor);
    }

    private void UpdateColor(float shift)
    {
        if (ColorBalance <= 1)
            ColorBalance += Time.deltaTime * shift;
        else
            ColorBalance = 0;

        HSVColor = Color.HSVToRGB(ColorBalance, 0.8f, 4.0f, true);

        foreach (Material mat in Materials)
        {
            mat.SetColor("_Color", HSVColor);
        }

    }

    private void RotateSkybox(float shift)
    {
        if (SkyboxRotation <= 360)
            SkyboxRotation += Time.deltaTime * shift;
        else
            SkyboxRotation = 0;

        Skybox.SetFloat("_Rotation", SkyboxRotation);
    }
}