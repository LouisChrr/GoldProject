using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEffectController : MonoBehaviour
{
    public static ImageEffectController Instance;

    public Material[] Materials;
    public Material Skybox;
    public float ColorShiftFactor, SkyboxShiftFactor;

    private Color HSVColor = Color.clear;
    public Color PreviousColor;

    private float ColorBalance, SkyboxRotation;
    private GameManager gm;

    [Range(0, 1)]
    public float hue;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("ImageEffectController is missing");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        PreviousColor = Color.green;
        gm = GameManager.Instance;
    }

    void Update()
    {
     //   UpdateColor(ColorShiftFactor);
     if(gm.HasGameStarted) RotateSkybox(SkyboxShiftFactor);

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

    public void ChangePreviousColor(Color newPreviousColor)
    {
        PreviousColor = newPreviousColor;
    }

    public Color ShiftedColor(float shift)
    {
        Color shiftedColor = new Color();

        hue += shift;

        if (hue >= 1)
            hue -= 1;

        shiftedColor = Color.HSVToRGB(hue, 1, 1);

        return shiftedColor;
    }

    public Color GetOppositeColor()
    {
        Color opposite = new Color();

        hue += 0.5f;

        if (hue >= 1)
            hue -= 1;

        opposite = Color.HSVToRGB(hue, 1, 1);

        return opposite;
    }

    public Color GetShiftedColorent(int dehkanceIndex)
    {
        Color shifted = new Color();

        float tmpHue = hue;

        for (int i = 0; i < dehkanceIndex; i++)
        {
            tmpHue += Time.deltaTime;

            if (tmpHue >= 1)
                tmpHue -= 1;
        }

        shifted = Color.HSVToRGB(tmpHue, 1, 1);

        return shifted;
    }
}