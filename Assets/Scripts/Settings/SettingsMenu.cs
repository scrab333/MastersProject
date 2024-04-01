using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;

    public ResItem[] resoultions;

    public int selectedResolution;

    public TextMeshProUGUI resolutionLabel;

    public Slider mastSlider, musicSlider, sfxSlider;
    public TextMeshProUGUI mastLabel, musicLabel, sfxLabel;


    // Start is called before the first frame update
    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        bool foundRes = false;
        for (int i = 0; i < resoultions.Length; i++)
        {
            if(Screen.width == resoultions[i].horizontal && Screen.height == resoultions[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;
                UpdateResLabel();
            }
        }

        if (!foundRes)
        {
            resolutionLabel.text = Screen.width.ToString() + "x" + Screen.height.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        UpdateResLabel();

    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resoultions.Length -1)
        {
            selectedResolution = resoultions.Length -1;
        }

        UpdateResLabel();

    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resoultions[selectedResolution].horizontal.ToString() + "x" + resoultions[selectedResolution].vertical.ToString();
    }

    public void ApplyGraphics()
    {
        Screen.fullScreen = fullscreenTog.isOn;

        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;

        }

        Screen.SetResolution(resoultions[selectedResolution].horizontal, resoultions[selectedResolution].vertical, fullscreenTog.isOn);


    }

}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
