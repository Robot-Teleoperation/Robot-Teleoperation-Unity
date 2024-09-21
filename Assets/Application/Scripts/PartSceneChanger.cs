using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.SceneManagement; 
using TMPro;

public class PartSceneChanger : MonoBehaviour
{
    public Interactable mrtkActionButton;

    void Start()
    {
        if (mrtkActionButton != null)
        {
            mrtkActionButton.OnClick.AddListener(TaskOnClick);
        }
    }

    public void TaskOnClick()
    {
        Debug.Log("You have clicked the scene changer button!");
        string buttonScene = mrtkActionButton.GetComponentInChildren<TextMeshPro>().text;
        SceneManager.LoadScene(buttonScene);
    }

}