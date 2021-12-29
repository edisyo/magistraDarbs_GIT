using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor;

public class buttonManager_3D : MonoBehaviour
{
    private humanBodyTrackerSelfmade humanBodyTrackerSelfmade;

    VisualElement root;
    Button btn_back3D;
    Button btn_openSettings;
    Button btn_closeSettings;
    VisualElement settingsPanel;
    Button toggle1;
    Button toggle2;
    Button toggle3;
    Button toggle4;
    Button toggle5;

    public Texture2D checkedImage;
    public Texture2D uncheckedImage;


    private void Awake() 
    {
        humanBodyTrackerSelfmade = FindObjectOfType<humanBodyTrackerSelfmade>();

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
            toggle1 = root.Q<UnityEngine.UIElements.Button>("toggle1");
            toggle2 = root.Q<UnityEngine.UIElements.Button>("toggle2");
            toggle3 = root.Q<UnityEngine.UIElements.Button>("toggle3");
            toggle4 = root.Q<UnityEngine.UIElements.Button>("toggle4");
            toggle5 = root.Q<UnityEngine.UIElements.Button>("toggle5");
        }
        
        if(btn_back3D != null)
            btn_back3D.clicked += toMainMenuButtonPressed;
        if(btn_openSettings != null)
            btn_openSettings.clicked += openSettings;
        if(btn_closeSettings != null)
            btn_closeSettings.clicked += closeSettings;

        if(toggle1 != null)
            toggle1.clicked += headTogglePressed;
        if(toggle2 != null)
            toggle2.clicked += shoulderTogglePressed;
        if(toggle3 != null)
            toggle3.clicked += hipTogglePressed;
        if(toggle4 != null)
            toggle4.clicked += kneesTogglePressed;
        if(toggle5 != null)
            toggle5.clicked += anklesTogglePressed;
        
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

    //FOR TOGGLE BUTTONS
    bool headToggleIsOn = false;
    bool shoulderToggleIsOn = false;
    bool hipsToggleIsOn = false;
    bool kneesToggleIsOn = false;
    bool anklesToggleIsOn = false;

    void headTogglePressed()//toggle1
    {
        headToggleIsOn = !headToggleIsOn;
        humanBodyTrackerSelfmade.changeTrackingStatus("head");

        if(headToggleIsOn)//CHECKED
        {
            toggle1.style.backgroundImage = checkedImage;
            humanBodyTrackerSelfmade.isHeadTracked = true;
            humanBodyTrackerSelfmade.head.GetComponent<Renderer>().material.color = Color.green;
        }
        else//NOT CHECKED
        {
            toggle1.style.backgroundImage = uncheckedImage;
            humanBodyTrackerSelfmade.isHeadTracked = false;
            humanBodyTrackerSelfmade.head.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    
    void shoulderTogglePressed()//toggle2
    {
        shoulderToggleIsOn = !shoulderToggleIsOn;
        humanBodyTrackerSelfmade.changeTrackingStatus("shoulders");

        if(shoulderToggleIsOn)//CHECKED
        {
            toggle2.style.backgroundImage = checkedImage;
            humanBodyTrackerSelfmade.isShouldersTracked = true;
            humanBodyTrackerSelfmade.rightShoulder.GetComponent<Renderer>().material.color = Color.green;
            humanBodyTrackerSelfmade.leftShoulder.GetComponent<Renderer>().material.color = Color.green;
        }
        else//NOT CHECKED
        {
            toggle2.style.backgroundImage = uncheckedImage;
            humanBodyTrackerSelfmade.isShouldersTracked = false;
            humanBodyTrackerSelfmade.rightShoulder.GetComponent<Renderer>().material.color = Color.white;
            humanBodyTrackerSelfmade.leftShoulder.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    void hipTogglePressed()//toggle3
    {
        hipsToggleIsOn = !hipsToggleIsOn;

        if(hipsToggleIsOn)//CHECKED
        {
            toggle3.style.backgroundImage = checkedImage;
            humanBodyTrackerSelfmade.isHipsTracked = true;
            humanBodyTrackerSelfmade.rightHip.GetComponent<Renderer>().material.color = Color.green;
            humanBodyTrackerSelfmade.leftHip.GetComponent<Renderer>().material.color = Color.green;
        }
        else//NOT CHECKED
        {
            toggle3.style.backgroundImage = uncheckedImage;
            humanBodyTrackerSelfmade.isHipsTracked = false;
            humanBodyTrackerSelfmade.rightHip.GetComponent<Renderer>().material.color = Color.white;
            humanBodyTrackerSelfmade.leftHip.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    void kneesTogglePressed()//toggle4
    {
        kneesToggleIsOn = !kneesToggleIsOn;

        if(kneesToggleIsOn)//CHECKED
        {
            toggle4.style.backgroundImage = checkedImage;
            humanBodyTrackerSelfmade.isKneesTracked = true;
            humanBodyTrackerSelfmade.rightKnee.GetComponent<Renderer>().material.color = Color.green;
            humanBodyTrackerSelfmade.leftKnee.GetComponent<Renderer>().material.color = Color.green;
        }
        else//NOT CHECKED
        {
            toggle4.style.backgroundImage = uncheckedImage;
            humanBodyTrackerSelfmade.isKneesTracked = false;
            humanBodyTrackerSelfmade.rightKnee.GetComponent<Renderer>().material.color = Color.white;
            humanBodyTrackerSelfmade.leftKnee.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    void anklesTogglePressed()//toggle5
    {
        anklesToggleIsOn = !anklesToggleIsOn;

        if(anklesToggleIsOn)//CHECKED
        {
            toggle5.style.backgroundImage = checkedImage;
            humanBodyTrackerSelfmade.isAnklesTracked = true;
            humanBodyTrackerSelfmade.rightAnkle.GetComponent<Renderer>().material.color = Color.green;
            humanBodyTrackerSelfmade.leftAnkle.GetComponent<Renderer>().material.color = Color.green;
        }
        else//NOT CHECKED
        {
            toggle5.style.backgroundImage = uncheckedImage;
            humanBodyTrackerSelfmade.isAnklesTracked = false;
            humanBodyTrackerSelfmade.rightAnkle.GetComponent<Renderer>().material.color = Color.white;
            humanBodyTrackerSelfmade.leftAnkle.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
