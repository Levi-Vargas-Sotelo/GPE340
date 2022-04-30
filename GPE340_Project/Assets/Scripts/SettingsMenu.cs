using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private Dropdown resolutionDD;
    [SerializeField]
    private Dropdown qualityDD;
    [SerializeField]
    private Slider sFXVolumeSlider;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Toggle isFullScreen;
    [SerializeField]
    private Button applyButton;
    private string resWidth, resHeight;

    // Start is called before the first frame update
    void Start()
    {
        ChangeMusic();
        ChangeSFX();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        
        // get the resolutions into the drop menu 
        resolutionDD.ClearOptions();
        List<string> resolutionsList = new List<string>();
        foreach (Resolution resolution in Screen.resolutions)
        {
            resolutionsList.Add(resolution.width + " x " + resolution.height + " - " + resolution.refreshRate + "Hz");
        }
        resolutionDD.AddOptions (resolutionsList);
        

        // get the graphics quality into the drop menu
        qualityDD.ClearOptions();
        List<string> qualityNames = new List<string>();
        foreach (string Qualities in QualitySettings.names)
        {
            qualityNames.Add(Qualities);
        }
        qualityDD.AddOptions(qualityNames);
    }

    public void ChangeMusic()
    {
        GameManager.Instance.musicVolume = musicVolumeSlider.value;
        GameManager.Instance.SetMusic(musicVolumeSlider.value);
    }

    public void ChangeSFX()
    {
        GameManager.Instance.sFXVolume = sFXVolumeSlider.value;
        GameManager.Instance.SetSFX(sFXVolumeSlider.value);
    }

    public void GoFullscreen()
    {
        Screen.fullScreen = true;
    }

    public void ExitFullscreen()
    {
        Screen.fullScreen = false;
    }

    public void ChangeResolution()
    {
        Resolution resolutionToChangeTo = Screen.resolutions[resolutionDD.value];
        Screen.SetResolution(resolutionToChangeTo.width, resolutionToChangeTo.height, false, resolutionToChangeTo.refreshRate);
    }

    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(qualityDD.value);
    }

    public void Apply()
    {
        ChangeMusic();
        ChangeSFX();
        if(isFullScreen)
        {
            GoFullscreen();
        }
        else
        {
            ExitFullscreen();
        }
        
        ChangeResolution();
        ChangeQuality();
    }
}
