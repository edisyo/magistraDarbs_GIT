using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementUI : MonoBehaviour
{
    public Color passiveColor;
    public Color defaultColor;
    public Color activeColor;
    [HideInInspector] public bool isTouched;
    [HideInInspector] public bool isActive = false;

    Image image;
    
    // Start is called before the first frame update
    
    private void Awake() 
    {
        image = transform.GetComponent<Image>();
        
        if(!isActive)
            image.color = passiveColor;
    }

    void Start()
    {
        isTouched = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            if(isTouched)
            {
                image.color = activeColor;
            }else
            {
                image.color = defaultColor;
            }
        }
        else
        {
            image.color = passiveColor;
        }
        
    }

}
