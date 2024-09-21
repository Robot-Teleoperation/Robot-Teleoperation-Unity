using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.SceneManagement; 
using System;

public class SatelliteSelector : MonoBehaviour
{
    public Interactable mrtkActionButton;
    public Canvas canvas; 

    void Start()
    {
        if (mrtkActionButton != null)
        {
            mrtkActionButton.OnClick.AddListener(TaskOnClick);
        }
        else
        {
            Debug.LogError("MRTK Action Button is not attached to the ButtonHandler script.");
        }
    }

    public void TaskOnClick()
    {
        Debug.Log("You have clicked the MRTK action button!");
        SceneManager.LoadScene("Scenes/ACRIM_3");
    }

}