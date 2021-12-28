using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class buttonManager_3D : MonoBehaviour
{
    VisualElement root;
    Button btn_back3D;
    Button btn_openSettings;
    Button btn_closeSettings;
    VisualElement settingsPanel;

    private void Awake() 
    {
        //GET UIDocument component
        if(transform.GetComponent<UIDocument>() != null)
        {
            root = GetComponent<UIDocument>().rootVisualElement;
        }else
        {
            root = FindObjectOfType<UIDocument>().rootVisualElement;
        }

        //GET UI elements from UIDOcument
        if(root != null)
        {
            btn_back3D = root.Q<UnityEngine.UIElements.Button>("Back_button");
            btn_openSettings = root.Q<UnityEngine.UIElements.Button>("OpenSettings_button");
            btn_closeSettings = root.Q<UnityEngine.UIElements.Button>("CloseSettings_button");
            settingsPanel = root.Q<UnityEngine.UIElements.VisualElement>("settings_panel");
        }
        
        //3D
        if(btn_back3D != null)
            btn_back3D.clicked += toMainMenuButtonPressed;
        if(btn_openSettings != null)
            btn_openSettings.clicked += openSettings;
        if(btn_closeSettings != null)
            btn_closeSettings.clicked += closeSettings;
        
        //Turn off elements which are not needed at first
        if(btn_back3D != null)
            btn_back3D.style.display = DisplayStyle.Flex;//TURN ON
        if (btn_openSettings != null)
            btn_openSettings.style.display = DisplayStyle.Flex;
        if (btn_closeSettings != null)
            btn_closeSettings.style.display = DisplayStyle.None;//TURN OFF
        if (settingsPanel != null)
            settingsPanel.style.display = DisplayStyle.None;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void toMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void openSettings()
    {
        settingsPanel.style.display = DisplayStyle.Flex;
        btn_openSettings.style.display = DisplayStyle.None;
        btn_closeSettings.style.display = DisplayStyle.Flex;
    }

    void closeSettings()
    {
        settingsPanel.style.display = DisplayStyle.None;
        btn_openSettings.style.display = DisplayStyle.Flex;
        btn_closeSettings.style.display = DisplayStyle.None;
    }
}
