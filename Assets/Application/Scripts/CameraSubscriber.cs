using System.Net.Mime;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Hololens;
using UnityEngine.UI;

public class CameraSubscriber : MonoBehaviour
{
    public string topicName = "camera_feed";
    private ROSConnection ros;
    private ImageMsg imageMsg;
    public RawImage display;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.Subscribe<ImageMsg>(topicName, CameraFeed);
    }

    void CameraFeed(ImageMsg msg)
    {
        //Debug.Log("Received camera feed");
        Texture2D texture = new Texture2D((int)msg.width, (int)msg.height, TextureFormat.RGB24, false);
        texture.LoadImage(msg.data);
        texture.Apply();
        display.texture = texture;
    }
}