using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveElementUI : MonoBehaviour
{
    public Color activeColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "shoulderR" || hit.collider.tag == "shoulderL")
                {
                    Debug.Log("Shoulder touched");
                }
            }
        }

        #if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 5f);
            if(Physics.Raycast(ray, out hit))
            {
                //If Touched object has ElementUI script
                if(hit.collider.GetComponent<ElementUI>() != null)
                {
                    Debug.Log("Hello there");
                    hit.collider.GetComponent<ElementUI>().isTouched = true;
                }
            }
        }
        #endif
    }
}
