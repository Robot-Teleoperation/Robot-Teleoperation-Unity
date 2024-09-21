using UnityEngine;
using UnityEngine.UI;

public class CameraFeedDisplay : MonoBehaviour
{
    public RawImage rawImage;

    [SerializeField]
    private Vector2Int requestedCameraSize = new(896, 504);

    [SerializeField]
    private int cameraFPS;

    private WebCamTexture webCamTexture;    

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        foreach (var device in WebCamTexture.devices)
        {
            Debug.Log("Available camera: " + device.name);
        }
        webCamTexture = new WebCamTexture(devices[1].name, requestedCameraSize.x, requestedCameraSize.y, cameraFPS);
        webCamTexture.Play();

        rawImage.texture = webCamTexture;

    }   

}