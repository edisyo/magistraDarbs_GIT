using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARSubsystems;

public class buttonScripts : MonoBehaviour
{
    UnityEngine.UIElements.Button btn_2d;
    UnityEngine.UIElements.Button btn_3d;
    UnityEngine.UIElements.Button btn_exit;
    UnityEngine.UIElements.Button btn_takePhoto;
    public phoneCamera m_phoneCamera;
    private VisualElement root;

    public RawImage tookedPhoto;

    // Start is called before the first frame update
    void Start()
    {
        //GET UIDocument component
        if(transform.GetComponent<UIDocument>() != null)
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            Debug.Log("Found Root gameobject from GetComponent");
        }else
        {
            root = FindObjectOfType<UIDocument>().rootVisualElement;
            Debug.Log("Found Root gameobject from FindObject");
        }
            
        //GET UI elements from UIDOcument
        if(root != null)
        {
            btn_2d = root.Q<UnityEngine.UIElements.Button>("2dButton");
            btn_3d = root.Q<UnityEngine.UIElements.Button>("3dButton");
            btn_exit = root.Q<UnityEngine.UIElements.Button>("exitButton");
            btn_takePhoto = root.Q<UnityEngine.UIElements.Button>("takePhoto_button");
            btn_takePhoto = root.Q<UnityEngine.UIElements.Button>("takePhoto_button");
        }
        

        //assign functions when buttons are clicked
        if(btn_2d != null)
            btn_2d.clicked += Button2DPressed;
        if(btn_3d != null)
            btn_3d.clicked += Button3DPressed;
        if(btn_exit != null)
            btn_exit.clicked += ButtonExitPressed;
        if(btn_takePhoto != null)
            btn_takePhoto.clicked += takePhoto;

        if(btn_2d == null ||btn_3d == null || btn_exit == null ||btn_takePhoto == null)
            Debug.LogWarning("Missing some UI Buttons..");
            

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneNameToGoTo)
    {
        SceneManager.LoadScene(sceneNameToGoTo, LoadSceneMode.Single);
        Debug.Log("Pressed Exit");
    }

    public void Button2DPressed()
    {
        SceneManager.LoadScene("2D_bodyTracking", LoadSceneMode.Single);
        Debug.Log("Loading 2d body tracking...");
    }

    public void Button3DPressed()
    {
        SceneManager.LoadScene("3D_bodyTracking", LoadSceneMode.Single);
        Debug.Log("Loading 3d body tracking...");
    }


    public void takePhoto()
    {
        //PhotoLogic photoLogic = FindObjectOfType<PhotoLogic>();
        //photoLogic.takePhoto();
        //XRCpuImage image = photoLogic.XRtakePhoto();
        WebCamTexture backCameraStream;

        backCameraStream = m_phoneCamera.getBackCam();
        Color32[] data = new Color32[backCameraStream.width * backCameraStream.height];
        Texture2D texture2D = new Texture2D(backCameraStream.width, backCameraStream.height);
        //tookedPhoto.texture = tookedPhoto_webCamTexture;

        //tookedPhoto.texture = backCameraStream.GetPixels32(data);

        texture2D.SetPixels32(backCameraStream.GetPixels32(data));
        texture2D.Apply();

        tookedPhoto.texture = texture2D;

    }

    public void ButtonExitPressed()
    {
        Application.Quit();
    }

}