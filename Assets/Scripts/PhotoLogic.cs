using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UIElements;
using TMPro;
using System.IO;

public class PhotoLogic : MonoBehaviour
{
    ARCameraManager cameraManager;
    ARCameraBackground m_ARCameraBackground;
    Texture2D m_CameraTexture;


    private Image screenshotImage;
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
        
        //screenShotGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takePhoto()
    {
        // var tex2D = ScreenCapture.CaptureScreenshotAsTexture();
        // Rect screenSize = new Rect(0,0, Screen.width, Screen.height);
        // Sprite newSprite = Sprite.Create(tex2D, screenSize, Vector2.zero);

        // screenShotGameObject.GetComponent<Image>().sprite = newSprite;
        // screenShotGameObject.SetActive(true);
        // debugText.text += "tex2d: " + tex2D + "\n newSprite: " + newSprite;

        // //cleanup
        // Object.Destroy(tex2D);
        
        // Dispose the XRCpuImage to avoid resource leaks.
        //image.Dispose();

        StartCoroutine("Screenshot");
    }

    private IEnumerator Screenshot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        texture.ReadPixels(new Rect(0,0,Screen.width, Screen.height), 0, 0);
        texture.Apply();

        string name = "Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";

        //PC
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/ScreenshotFolder/" + name, bytes);

        debugText.text = "Saved a pic: " + texture;
        
        Destroy(texture);

        
    }
}
