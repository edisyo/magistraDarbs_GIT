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
    public float lineOffset = 0.01f;

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
        if(LineRendererName == "SHOULDER LINE")
        {
            if(line != null)
            {
                for(int i= 0; i < LineRendererObjects.Count; i++)
                {
                    Debug.Log("heeeelow");
                    Vector3 v3 = new Vector3(LineRendererObjects[i].position.x, LineRendererObjects[i].position.y, LineRendererObjects[i].position.z + lineOffset);
                    line.SetPosition(i, v3);
                }
            }
        }

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
