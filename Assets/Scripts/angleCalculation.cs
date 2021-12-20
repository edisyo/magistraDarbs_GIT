using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARKit;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation.Samples;

public class angleCalculation : MonoBehaviour
{
    public humanBodyTrackerSelfmade humanBodyTrackerSelfmade;
    //public HumanBodyTracker humanBodyTracker;

    public string object1Name;
    public string object2Name;
    public Transform pos1;
    public Transform pos2;
    public TextMeshProUGUI debugText;

    public LineRenderer line;

    GameObject skeletonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        debugText.gameObject.SetActive(true);
        debugText.text = "Looking for a BODY..";

        //Nestrādā uz prefab
        //pos1 = GameObject.Find(object1Name).transform;
        //pos2 = GameObject.Find(object2Name).transform;


        //pameginat caur šo atrast prefab un tad atrast punktus - ari nestradaja
        //humanBodyTracker = FindObjectOfType<HumanBodyTracker>();
        humanBodyTrackerSelfmade = FindObjectOfType<humanBodyTrackerSelfmade>();
    }

    // Update is called once per frame
    void Update()
    {

        //skeletonPrefab = humanBodyTracker.newSkeletonGO;

        var direction = pos1.position - pos2.position;
        var up = transform.up;
        var angle = Vector3.Angle(up, direction);

        //debugText.text = "found prefab: " + skeletonPrefab.name;

        if (pos1 != null)
        {
            debugText.text = "ANGLE: " + (angle - 90).ToString("F1") + " dir: " + direction;
            debugText.gameObject.SetActive(true);
        }
        else
        {
            debugText.text = "not found";
            debugText.gameObject.SetActive(false);
        }

        Debug.DrawLine(pos1.position, pos2.position, Color.green, 0.5f);
        Debug.DrawLine(up, direction, Color.cyan, 0.5f);
        line.SetPosition(0, pos1.transform.position);
        line.SetPosition(1, pos2.transform.position);
    }
}
