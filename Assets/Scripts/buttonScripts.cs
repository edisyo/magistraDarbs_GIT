using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class buttonScripts : MonoBehaviour
{
    public Button btn_2d;
    public Button btn_3d;
    public Button btn_exit;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        btn_2d = root.Q<Button>("2dButton");
        btn_3d = root.Q<Button>("3dButton");
        btn_exit = root.Q<Button>("exitButton");

        //assign functions when buttons are clicked
        if(btn_2d != null && btn_3d != null && btn_exit != null)
        {
            btn_2d.clicked += Button2DPressed;
            btn_3d.clicked += Button3DPressed;
            btn_exit.clicked += ButtonExitPressed;
        }else
        {
            Debug.LogError("Missing UI Buttons, check if naming is correct..");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneNameToGoBack)
    {
        SceneManager.LoadScene(sceneNameToGoBack, LoadSceneMode.Single);
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

    public void ButtonExitPressed()
    {
        Application.Quit();
    }
}