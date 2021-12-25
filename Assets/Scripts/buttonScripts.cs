using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARSubsystems;

public class buttonScripts : MonoBehaviour
{
    //ELEMENTS FROM UI BUILDER
    UnityEngine.UIElements.Button btn_2d;
    UnityEngine.UIElements.Button btn_3d;
    UnityEngine.UIElements.Button btn_exit;
    UnityEngine.UIElements.Button btn_takePhoto;
    UnityEngine.UIElements.Button btn_reTakePhoto;

    //OLD UNITY UI SYSTEM
    public UnityEngine.UI.Button btn_back;

    public phoneCamera m_phoneCamera;
    private VisualElement root;

    private WebCamTexture backCameraStream;
    public RawImage tookedPhoto;

    // Start is called before the first frame update
    void Start()
    {
        if (tookedPhoto != null)
            tookedPhoto.gameObject.SetActive(false);

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
            btn_reTakePhoto = root.Q<UnityEngine.UIElements.Button>("reTakePhoto_button");
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
        if (btn_reTakePhoto != null)
            btn_reTakePhoto.clicked += reTakePhotoPressed;

        if(btn_2d == null ||btn_3d == null || btn_exit == null ||btn_takePhoto == null)
            Debug.LogWarning("Missing some UI Buttons..");

        if(btn_reTakePhoto != null)
            btn_reTakePhoto.style.display = DisplayStyle.None;

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
        //Get webcamtexture method 1
        backCameraStream = m_phoneCamera.getBackCam();

        //Get the same camera stream setup as in phoneCamera script
        tookedPhoto.GetComponent<AspectRatioFitter>().aspectRatio = m_phoneCamera.getRatio();
        //tookedPhoto.rectTransform.localScale = new Vector3(1f, m_phoneCamera.getScaleY(), 1f);
        tookedPhoto.rectTransform.localEulerAngles = new Vector3(0, 0, m_phoneCamera.getOrient());

        //Take color data from one frame = take screenshot from video (WORKING METHOD!!!)
        Color32[] data = new Color32[backCameraStream.width * backCameraStream.height];
        Texture2D screenshot = new Texture2D(backCameraStream.width, backCameraStream.height);
        screenshot.SetPixels32(backCameraStream.GetPixels32(data));
        screenshot.Apply();

        //Assign tooked screenshot to UI Raw Image
        tookedPhoto.texture = screenshot;

        //Pause camera stream, cant see it anyway
        backCameraStream.Pause();
        btn_takePhoto.style.display = DisplayStyle.None;
        btn_reTakePhoto.style.display = DisplayStyle.Flex;

        tookedPhoto.gameObject.SetActive(true);
        turnOffUI();

    }

    public void ButtonExitPressed()
    {
        Application.Quit();
    }

    public void reTakePhotoPressed()
    {
        backCameraStream.Play();
        btn_reTakePhoto.style.display = DisplayStyle.None;
        tookedPhoto.gameObject.SetActive(false);
        turnONUI();
    }

    public void turnOffUI()
    {
        btn_takePhoto.style.display = DisplayStyle.None;
        btn_back.gameObject.SetActive(false);
    }

    public void turnONUI()
    {
        btn_takePhoto.style.display = DisplayStyle.Flex;
        btn_back.gameObject.SetActive(true);
    }

}