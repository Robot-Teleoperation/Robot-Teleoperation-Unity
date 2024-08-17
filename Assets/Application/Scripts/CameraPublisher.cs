using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Hololens;

public class CameraPublisher : MonoBehaviour
{
    public Camera cam;
    public string topicName = "camera_feed";
    public int frameRate = 60;

    [SerializeField]
    private int cameraFPS = 60;

    [SerializeField]
    private Vector2Int requestedCameraSize = new(896, 504);

    private ROSConnection ros;
    //private Texture2D texture;
    private byte[] image;
    private int width;
    private int height;

    private WebCamTexture webCamTexture;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<ImageMsg>(topicName);

        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("Device: " + devices[i].name);
        }

        webCamTexture = new WebCamTexture(devices[0].name, requestedCameraSize.x, requestedCameraSize.y, cameraFPS);
        webCamTexture.Play();

        InvokeRepeating("PublishImage", 0, 1.0f / frameRate);
    }

    void PublishImage()
    {
        //actualCameraSize = new Vector2Int(webCamTexture.width, webCamTexture.height);
        var renderTexture = new RenderTexture(webCamTexture.width, webCamTexture.height, 24);
        
        //var cameraTransform = Camera.main.CopyCameraTransForm();
        Graphics.Blit(webCamTexture, renderTexture);

        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        var oldRt = RenderTexture.active;
        RenderTexture.active = renderTexture;

        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        RenderTexture.active = oldRt;

        byte[] imageBytes = texture.EncodeToPNG(); // Convert texture to bytes

        //byte[] bytes = new byte[width * height * 3];

        ImageMsg msg = new ImageMsg
        {
            data = imageBytes,
            width = (uint)requestedCameraSize.x,
            height = (uint)requestedCameraSize.y,
        };

        ros.Publish(topicName, msg);
        //Debug.Log("Published image");

        Destroy(texture);
    }

}