using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class buttonScripts : MonoBehaviour
{
    private Button btn_2d;
    private Button btn_3d;
    private Button btn_exit;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        btn_2d = root.Q<Button>("2dButton");
        btn_3d = root.Q<Button>("3dButton");
        btn_exit = root.Q<Button>("exitButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneNameToGoBack)
    {
        SceneManager.LoadScene(sceneNameToGoBack, LoadSceneMode.Single);
    }

    public void Button2DPressed()
    {
        SceneManager.LoadScene("2D_bodyTracking", LoadSceneMode.Single);
    }

    public void Button3DPressed()
    {
        SceneManager.LoadScene("3D_bodyTracking", LoadSceneMode.Single);
    }

    public void ButtonExitPressed()
    {
        Application.Quit();
    }
}