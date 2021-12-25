using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class phoneCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;
    private float ratio;
    private float scaleY;
    private int orient;

    public RawImage background;
    public AspectRatioFitter fit;
    public TextMeshProUGUI debugText;
    public bool CameraIsFrontFacing;

    
    // Start is called before the first frame update
    void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        for(int i = 0; i < devices.Length; i++)
        {

            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if(backCam == null)
        {
            Debug.Log("No back facing cam");
            return;
        }

        backCam.Play();
        background.texture = backCam;

        camAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!camAvailable)
            return;
        
        ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        //debugText.text = "Orient: " + orient;

        //debugText.text = "backCam: " + backCam;
        
    }

    public WebCamTexture getBackCam()
    {
        return backCam;
    }

    public float getRatio()
    {
        return ratio;
    }

    public float getScaleY()
    {
        return scaleY;
    }

    public int getOrient()
    {
        return orient;
    }
}
