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
    Button btn_saveSettings;
    Button btn_startCalculating;
    Button btn_stopCalculating;
    Label timer_label;
    VisualElement settingsPanel;
    Button toggle1;
    Button toggle2;
    Button toggle3;
    Button toggle4;
    Button toggle5;

    bool startCountdown = false;

    public Texture2D checkedImage;
    public Texture2D uncheckedImage;
    public int timerDuration;


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
            btn_saveSettings = root.Q<UnityEngine.UIElements.Button>("SaveSettings_button");
            btn_startCalculating = root.Q<UnityEngine.UIElements.Button>("startCalculate_button");
            btn_stopCalculating =  root.Q<UnityEngine.UIElements.Button>("stopCalculate_button");
            timer_label =   root.Q<UnityEngine.UIElements.Label>("countdown_text");
            settingsPanel = root.Q<UnityEngine.UIElements.VisualElement>("settings_panel");
            toggle1 = root.Q<UnityEngine.UIElements.Button>("toggle1");
            toggle2 = root.Q<UnityEngine.UIElements.Button>("toggle2");
            toggle3 = root.Q<UnityEngine.UIElements.Button>("toggle3");
            toggle4 = root.Q<UnityEngine.UIElements.Button>("toggle4");
            toggle5 = root.Q<UnityEngine.UIElements.Button>("toggle5");
        }
        
        if(btn_back3D != null)
            btn_back3D.clicked += ToMainMenuButtonPressed;
        if(btn_openSettings != null)
            btn_openSettings.clicked += OpenSettings;
        if(btn_saveSettings != null)
            btn_saveSettings.clicked += SaveSettings;
        if(btn_startCalculating != null)
            btn_startCalculating.clicked += StartCalculating;
        if(btn_stopCalculating != null)
            btn_stopCalculating.clicked += StopCalculating;

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

        if (settingsPanel != null)
            settingsPanel.style.display = DisplayStyle.None;//TURN OFF
        if (btn_startCalculating != null)
            btn_startCalculating.style.display = DisplayStyle.None;
        if (btn_stopCalculating != null)
            btn_stopCalculating.style.display = DisplayStyle.None;
        if (timer_label != null)
            timer_label.style.display = DisplayStyle.None;
            

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    void ToMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void OpenSettings()
    {
        settingsPanel.style.display = DisplayStyle.Flex;
        btn_openSettings.style.display = DisplayStyle.None;
    }

    void SaveSettings()
    {
        settingsPanel.style.display = DisplayStyle.None;
        btn_openSettings.style.display = DisplayStyle.Flex;
    }

    void StartCalculating()
    {
        humanBodyTrackerSelfmade.changeTrackingStatus("shoulders");
        startCountdown = true;

        btn_openSettings.style.display = DisplayStyle.None;
        btn_startCalculating.style.display = DisplayStyle.None;
        
        timer_label.style.display = DisplayStyle.Flex;
            

        StartCoroutine(TimerForCalculating());
    }

    IEnumerator TimerForCalculating()
    {
        while (timerDuration > 0)
        {
            timer_label.text = $" Saglabāt stāju \n" + timerDuration.ToString();
            
            yield return new WaitForSeconds(1f);

            timerDuration--;
        }

        //END OF TIMER
        timer_label.text = $" Proccessing results... \n ";

        btn_stopCalculating.style.display = DisplayStyle.Flex;
    }

    void StopCalculating()
    {
        humanBodyTrackerSelfmade.isShouldersTracked = false;
        btn_openSettings.style.display = DisplayStyle.Flex;
        btn_stopCalculating.style.display = DisplayStyle.None;
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

        if(shoulderToggleIsOn)//CHECKED
        {
            toggle2.style.backgroundImage = checkedImage;
            
            humanBodyTrackerSelfmade.rightShoulder.GetComponent<Renderer>().material.color = Color.green;//just to see what is being tracked
            humanBodyTrackerSelfmade.leftShoulder.GetComponent<Renderer>().material.color = Color.green;

            btn_startCalculating.style.display = DisplayStyle.Flex;
        }
        else//NOT CHECKED
        {
            toggle2.style.backgroundImage = uncheckedImage;

            humanBodyTrackerSelfmade.rightShoulder.GetComponent<Renderer>().material.color = Color.white;
            humanBodyTrackerSelfmade.leftShoulder.GetComponent<Renderer>().material.color = Color.white;

            btn_startCalculating.style.display = DisplayStyle.None;
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
