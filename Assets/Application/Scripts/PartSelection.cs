using UnityEngine;
using UnityEngine.UI; // For UI elements
using UnityEngine.SceneManagement; // For scene management
// Assuming you have a namespace for ROS communication
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Hololens;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using TMPro;

public class PartSelection : MonoBehaviour
{
    public Interactable[] pressableButtons;

    private ROSConnection ros;

    void Start()
    {
        // Initialize ROS connection and subscribe to the topic
        ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<AIDataMsg>("ai_data", HandleRosResponse);
    }

    void HandleRosResponse(AIDataMsg response)
    {
        Debug.Log($"Received AI data: {response.scene_names.Length} scenes");
        CreateButtonsBasedOnRosResponse(response.scene_names);
    }

   
    void CreateButtonsBasedOnRosResponse(string[] scenes)
    {   
        Debug.Log($"Scene 0: {scenes[0]}");
        int count = Mathf.Min(scenes.Length, pressableButtons.Length); // Ensure we don't exceed the bounds of either array

        for (int i = 0; i < count; i++)
        {
            string sceneName = scenes[i];
            Interactable interactableButton = pressableButtons[i];

            // Find the Text or TextMeshPro component and set its text
            TextMeshPro textComponent = interactableButton.GetComponentInChildren<TextMeshPro>();
            if (textComponent != null)
            {
                textComponent.text = sceneName;
            }
            else
            {
                // Optionally handle the case where no text component is found
                Debug.LogWarning("No TextMeshPro component found on Interactable button.");
            }

            interactableButton.OnClick.AddListener(() => LoadSceneByName(sceneName));
        }
    }

    void LoadSceneByName(string sceneName)
    {
        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}