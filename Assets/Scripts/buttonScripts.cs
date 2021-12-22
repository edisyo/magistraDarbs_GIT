using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR.ARSubsystems;

public class buttonScripts : MonoBehaviour
{
    Button btn_2d;
    Button btn_3d;
    Button btn_exit;
    Button btn_takePhoto;
    IMGUIContainer img_tookedPhoto;

    private VisualElement root;

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
            btn_2d = root.Q<Button>("2dButton");
            btn_3d = root.Q<Button>("3dButton");
            btn_exit = root.Q<Button>("exitButton");
            btn_takePhoto = root.Q<Button>("takePhoto_button");
            btn_takePhoto = root.Q<Button>("takePhoto_button");
            img_tookedPhoto = root.Q<IMGUIContainer>("TookedPhoto");
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
        PhotoLogic photoLogic = FindObjectOfType<PhotoLogic>();
        //XRCpuImage image = photoLogic.takePhoto();
    }

    public void ButtonExitPressed()
    {
        Application.Quit();
    }
}