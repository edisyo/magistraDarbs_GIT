using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletLineRenderer : MonoBehaviour
{
    LineRenderer line;

    public string LineRendererName;

    public List<Transform> LineRendererObjects = null;

    public float lineStartWidth;
    public float lineEndWidth;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = LineRendererObjects.Count;
        line.startWidth = lineStartWidth;
        line.endWidth = lineEndWidth;
    }

    // Update is called once per frame
    void Update()
    {
        if(line != null)
        {
            for(int i= 0; i < LineRendererObjects.Count; i++)
            {
                line.SetPosition(i, LineRendererObjects[i].position);
            }

        }else
        {
            Debug.Log("Line renderer missing");
        }
        
    }
}
