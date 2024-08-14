using UnityEngine;
using UnityEngine.EventSystems;
//using Microsoft.MixedReality.Toolkit.Input;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Hololens;
using UnityEngine.SceneManagement;


public class CanvasPointer : MonoBehaviour, IPointerClickHandler
{
    private RectTransform canvasRectTransform;
    private ROSConnection rosConnection;

    void Start()
    {
        // Get the RectTransform of the canvas
        canvasRectTransform = GetComponent<RectTransform>();
        rosConnection = ROSConnection.GetOrCreateInstance();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pressPosition != null)
        {
            // Convert the screen point to a point on the canvas
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.pressPosition, eventData.pressEventCamera, out localPoint);

            // Optionally, convert to pixel coordinates based on the canvas size
            Vector2 canvasSize = canvasRectTransform.sizeDelta;
            Vector2 normalizedPoint = new Vector2((localPoint.x / canvasSize.x) + 0.5f, (localPoint.y / canvasSize.y) + 0.5f);
            Vector2 pixelCoordinates = new Vector2(normalizedPoint.x * canvasSize.x, normalizedPoint.y * canvasSize.y);

            Debug.Log($"Touch detected at canvas coordinates: {localPoint} or pixel coordinates: {pixelCoordinates}");
            CallGetAIDataService((int)pixelCoordinates.x, (int)pixelCoordinates.y);
        }
    }

    void CallGetAIDataService(int pointX, int pointY)
    {   
        GetAIDataRequest request = new GetAIDataRequest();
        request.point_x = pointX;
        request.point_y = pointY;

        rosConnection.SendServiceMessage<GetAIDataResponse>("get_ai_data", request, ServiceResponseHandler);
    }

    void ServiceResponseHandler(GetAIDataResponse response)
    {
        // Handle the response
        Debug.Log("Received scene name: " + response.model);

        // Switch scene based on the response
        SceneManager.LoadScene(response.model);
    }
}
