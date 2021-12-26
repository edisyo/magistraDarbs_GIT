using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementUI : MonoBehaviour
{
    public Color defaultColor;
    public Color activeColor;
    [HideInInspector] public bool isTouched;

    Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        isTouched = false;
        image = transform.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouched)
        {
            image.color = activeColor;
        }else
        {
            image.color = defaultColor;
        }
    }

}
