using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingMusic : MonoBehaviour
{
    [SerializeField] private Slider volumeslider = null;

    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if(musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("mastervolume", AudioListener.volume);
    }

   
}
