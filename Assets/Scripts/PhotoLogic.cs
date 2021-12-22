using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UIElements;
using TMPro;

public class PhotoLogic : MonoBehaviour
{
    ARCameraManager cameraManager;
    ARCameraBackground m_ARCameraBackground;
    Texture2D m_CameraTexture;


    public Image screenshotImage;
    public GameObject screenShotGameObject;
    public TextMeshProUGUI debugText;
    


    void Start()
    {
        cameraManager = FindObjectOfType<ARCameraManager>();
        m_ARCameraBackground = FindObjectOfType<ARCameraBackground>();

        if(cameraManager != null)
            Debug.Log("Camera Manager Found");
        
        if(m_ARCameraBackground != null)
            Debug.Log("Camera Background Found");
        
        screenShotGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //screenshotImage = screenshotGameObject.GetComponent<Image>();
        if(screenshotImage == null)
        {
            debugText.text = "Not found yet...";
        }else
        {
            debugText.text = "" + screenshotImage.image;
        }
        
    }

    public void takePhoto()
    {
        Debug.Log("Taking a photo...");
        if(!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            return;
        
        
        // Consider each image plane.
        for (int planeIndex = 0; planeIndex < image.planeCount; ++planeIndex)
        {
            // Log information about the image plane.
            var plane = image.GetPlane(planeIndex);
            Debug.LogFormat("Plane {0}:\n\tsize: {1}\n\trowStride: {2}\n\tpixelStride: {3}",
            planeIndex, plane.data.Length, plane.rowStride, plane.pixelStride);

            //debugText.text = "Plane " + planeIndex + "\n tsize: " +plane.data.Length + " \n\trowStride: " + 
                //plane.rowStride + "\n\tpixelStride: {3}" + plane.pixelStride;
            // Do something with the data.
            //MyComputerVisionAlgorithm(plane.data);
            //screenshotImage = image;

        }

        var format = TextureFormat.RGBA32;
        Rect screenSize = new Rect(0,0, image.width, image.height);
        m_CameraTexture = new Texture2D(image.width, image.height, format, false);
        //screenshotImage = screenshotGameObject.GetComponent<Image>();
        //Sprite.Create(m_CameraTexture, screenSize, new Vector2(0.5f, 0.5f));
        screenshotImage.sprite = Sprite.Create(m_CameraTexture, new Rect(0f, 0f, image.width, image.height), new Vector2(0.5f,0.5f));
        screenShotGameObject.GetComponent<Image>().sprite = screenshotImage.sprite;
        screenShotGameObject.SetActive(true);
        
        
        // Dispose the XRCpuImage to avoid resource leaks.
        //image.Dispose();
    }
}
