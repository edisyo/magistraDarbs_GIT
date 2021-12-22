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
    // Start is called before the first frame update
    public Image screenshotImage;
    public TextMeshProUGUI debugText;


    void Start()
    {
        cameraManager = FindObjectOfType<ARCameraManager>();

        if(cameraManager != null)
            Debug.Log("Camera Manager Found");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public XRCpuImage takePhoto()
    {
        Debug.Log("Taking a photo...");
        if(!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            return image;
        
        // Consider each image plane.
        for (int planeIndex = 0; planeIndex < image.planeCount; ++planeIndex)
        {
            // Log information about the image plane.
            var plane = image.GetPlane(planeIndex);
            Debug.LogFormat("Plane {0}:\n\tsize: {1}\n\trowStride: {2}\n\tpixelStride: {3}",
            planeIndex, plane.data.Length, plane.rowStride, plane.pixelStride);

            debugText.text += "Plane " + planeIndex + "\n tsize: " +plane.data.Length + " \n\trowStride: " + 
                plane.rowStride + "\n\tpixelStride: {3}" + plane.pixelStride;
            // Do something with the data.
            //MyComputerVisionAlgorithm(plane.data);
            //screenshotImage = image;

        }

        return image;
        // Dispose the XRCpuImage to avoid resource leaks.
        //image.Dispose();
    }
}
