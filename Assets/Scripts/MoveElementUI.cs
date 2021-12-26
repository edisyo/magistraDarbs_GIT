using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveElementUI : MonoBehaviour
{
    public Color activeColor;
    
    private float dist;
    private bool isDragging = false;
    private Vector3 offset;
    private Transform objToDrag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 5f);
            if(Physics.Raycast(ray, out hit))
            {
                //If Touched object has ElementUI script
                if(hit.collider.GetComponent<ElementUI>() != null)
                {
                    Debug.Log("Hello there");
                    hit.collider.GetComponent<ElementUI>().isTouched = true;

                    objToDrag = hit.transform;
                    dist = hit.transform.position.z - Camera.main.transform.position.z;
                    Vector3 v3 = new Vector3(mousePos.x, mousePos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    offset = objToDrag.position - v3;
                    isDragging = true;
                }
            }
        }

        //Dragging the object
        if(isDragging && Input.GetMouseButtonDown(0))
        {
            Vector3 v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            objToDrag.position = v3 + offset;
        }

        //If touch is ended or canceled
        if(isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        #endif

        #if PLATFORM_IPHONE

        if(Input.touchCount != 1)
        {
            isDragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        //Touched the object
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                //If Touched object has ElementUI script
                if(hit.collider.GetComponent<ElementUI>() != null)
                {
                    Debug.Log("Hello there");
                    hit.collider.GetComponent<ElementUI>().isTouched = true;
                    
                    objToDrag = hit.transform;
                    dist = hit.transform.position.z - Camera.main.transform.position.z;
                    Vector3 v3 = new Vector3(pos.x, pos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    offset = objToDrag.position - v3;
                    isDragging = true;
                }
            }
        }

        //Dragging the object
        if(isDragging && Input.touches[0].phase == TouchPhase.Moved)
        {
            Vector3 v3 = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            objToDrag.position = v3 + offset;
        }

        //If touch is ended or canceled
        if(isDragging && (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled))
        {
            isDragging = false;
        }
        #endif
    }
}
