using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.XR.ARFoundation.Samples;

public class humanBodyTrackerSelfmade : MonoBehaviour
{
    public boneToTrack[] bonesToTrack;
    public List<Transform> leftShoulder_Positions;
    public List<Transform> rightShoulder_Positions;
    public TextMeshProUGUI debugText;

    
    //BONES
    public GameObject head;
    public GameObject leftShoulder;
    public GameObject rightShoulder;
    public GameObject rightHip;
    public GameObject leftHip;
    public GameObject rightKnee;
    public GameObject leftKnee;
    public GameObject rightAnkle;
    public GameObject leftAnkle;
    

    public LineRenderer line;

    buttonManager_3D buttonManager_3D;

    [SerializeField]
    [Tooltip("The Skeleton prefab to be controlled.")]
    GameObject m_SkeletonPrefab;

    [SerializeField]
    [Tooltip("The ARHumanBodyManager which will produce body tracking events.")]
    ARHumanBodyManager m_HumanBodyManager;

    /// <summary>
    /// Get/Set the <c>ARHumanBodyManager</c>.
    /// </summary>
    public ARHumanBodyManager humanBodyManager
    {
        get { return m_HumanBodyManager; }
        set { m_HumanBodyManager = value; }
    }

    /// <summary>
    /// Get/Set the skeleton prefab.
    /// </summary>
    public GameObject skeletonPrefab
    {
        get { return m_SkeletonPrefab; }
        set { m_SkeletonPrefab = value; }
    }

    Dictionary<TrackableId, BoneController> m_SkeletonTracker = new Dictionary<TrackableId, BoneController>();

    float lx,ly,lz,rx,ry,rz;
    float sumLx, sumLy, sumLz, sumRx, sumRy, sumRz;

    void OnEnable()
    {

        humanBodyManager.humanBodiesChanged += OnHumanBodiesChanged;
        
    }

    void OnDisable()
    {
        if (humanBodyManager != null)
            humanBodyManager.humanBodiesChanged -= OnHumanBodiesChanged;
        
    }

    private void Start()
    {
        debugText.text = $"Meklē cilvēka ķermeni... \n";
        line.gameObject.SetActive(false);
        leftShoulder_Positions = new List<Transform>();
        rightShoulder_Positions = new List<Transform>();

        buttonManager_3D = FindObjectOfType<buttonManager_3D>();

        sumLx = sumLy = sumLz = sumRx = sumRy = sumRz = 0;
    }

    private void Update() 
    {
        testingAngles();    
    }

    void OnHumanBodiesChanged(ARHumanBodiesChangedEventArgs eventArgs)
    {
        BoneController boneController;

        foreach (var humanBody in eventArgs.added)
        {
            if (!m_SkeletonTracker.TryGetValue(humanBody.trackableId, out boneController))
            {
                //Debug.Log($"Adding a new skeleton [{humanBody.trackableId}].");
                var newSkeletonGO = Instantiate(m_SkeletonPrefab, humanBody.transform);

                if (bonesToTrack == null)
                {
                    //bonesToTrack = boneController.GetComponentInChildren<boneToTrack>();
                    //debugText.text = "" + bonesToTrack.gameObject.name;

                    bonesToTrack = boneController.skeletonRoot.GetComponentsInChildren<boneToTrack>();
                    // foreach (boneToTrack boneToTrack in bonesToTrack)
                    // {
                    //     //debugText.text += "Added Bone: " + boneToTrack.transform.parent.name;
                    // }
                }

                boneController = newSkeletonGO.GetComponent<BoneController>();
                m_SkeletonTracker.Add(humanBody.trackableId, boneController);
            }

            boneController.InitializeSkeletonJoints();
            boneController.ApplyBodyPose(humanBody);
        }

        foreach (var humanBody in eventArgs.updated)//BASICALLY LIKE UPDATE FUNCTION
        {
            if (m_SkeletonTracker.TryGetValue(humanBody.trackableId, out boneController))
            {
                boneController.ApplyBodyPose(humanBody);
                getTrackedBones(bonesToTrack);
            }

            if (bonesToTrack != null)
            {
                bonesToTrack = boneController.skeletonRoot.GetComponentsInChildren<boneToTrack>();

                if(isShouldersTracked)
                {
                    leftShoulder_Positions.Add(leftShoulder.transform);//GET teansforms for future calculations
                    rightShoulder_Positions.Add(rightShoulder.transform); 

                    //checkShoulderAngles();               
                    
                }else
                {
                    line.gameObject.SetActive(false);
                    debugText.text = $"Meklē cilvēka ķermeni... \n";
                }
                
            }

                    
        }

        foreach (var humanBody in eventArgs.removed)
        {
            //Debug.Log($"Removing a skeleton [{humanBody.trackableId}].");
            if (m_SkeletonTracker.TryGetValue(humanBody.trackableId, out boneController))
            {
                Destroy(boneController.gameObject);
                m_SkeletonTracker.Remove(humanBody.trackableId);
            }

            rightShoulder.GetComponent<Renderer>().material.color = Color.white;
            leftShoulder.GetComponent<Renderer>().material.color = Color.white;
            line.gameObject.SetActive(false);
        }
    }

    private void getTrackedBones(boneToTrack[] bones)
    {
        foreach (boneToTrack bone in bones)
        {
            if (bone.transform.parent.name == "Head")
                head = bone.gameObject;

            if (bone.transform.parent.name == "RightArm")
                rightShoulder = bone.gameObject;
            if (bone.transform.parent.name == "RightUpLeg")
                rightHip = bone.gameObject;
            if (bone.transform.parent.name == "RightLeg")
                rightKnee = bone.gameObject;
            if (bone.transform.parent.name == "RightFoot")
                rightAnkle = bone.gameObject;

            if (bone.transform.parent.name == "LeftArm")
                leftShoulder = bone.gameObject;
            if (bone.transform.parent.name == "LeftUpLeg")
                leftHip = bone.gameObject;
            if (bone.transform.parent.name == "LeftLeg")
                leftKnee = bone.gameObject;
            if (bone.transform.parent.name == "LeftFoot")
                leftAnkle = bone.gameObject;
        }
    }

    public void checkShoulderAngles()
    {
        
        foreach(var transformList in leftShoulder_Positions)//LEFT SHOULDER MEAN POSITION
        {
            lx = transformList.position.x;
            ly = transformList.position.y;
            lz = transformList.position.z;

            sumLx += lx;
            sumLy += ly;
            sumLz += lz;
        }

        foreach(var transformList in rightShoulder_Positions)//LEFT SHOULDER MEAN POSITION
        {
            rx = transformList.position.x;
            ry = transformList.position.y;
            rz = transformList.position.z;

            sumRx = rx;
            sumRy = ry;
            sumRz = rz;
        }

        float meanLx = sumLx /  leftShoulder_Positions.Count;
        float meanLy = sumLy / leftShoulder_Positions.Count;
        float meanLz = sumLz / leftShoulder_Positions.Count;

        float meanRx = sumRx /  leftShoulder_Positions.Count;
        float meanRy = sumRy /  leftShoulder_Positions.Count;
        float meanRz = sumRz /  leftShoulder_Positions.Count;

        Debug.Log($"meanLx = {meanLx} | {sumLx} / {leftShoulder_Positions.Count}");
        Debug.Log($"meanLy = {meanLy} | {sumLy} / {leftShoulder_Positions.Count}");
        Debug.Log($"meanLz = {meanLz} | {sumLz} / {leftShoulder_Positions.Count}");

        Vector3 leftShoulderMeanPosition = new Vector3(meanLx,meanLy,meanLz);
        Vector3 rightShoulderMeanPosition = new Vector3(meanRx,meanRy,meanRz);

        //debugText.text = $"{leftShoulder_Positions.Count} |vidējais position [{meanPositionX}, {meanPositionY}, {meanPositionZ}]";

        var direction = rightShoulderMeanPosition - leftShoulderMeanPosition;
        // var right = Vector3.right;
        // var angle = Vector3.SignedAngle(direction,right, Vector3.up);//not working correctly

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // angle = 180 - angle;

        // if(angle > 180)
        //     angle = 360 - angle;

        line.gameObject.SetActive(true);
        line.SetPosition(0, leftShoulder.transform.position);
        line.SetPosition(1, rightShoulder.transform.position);

        buttonManager_3D.timer_label.text = $"Shoulder angle is: \n {angle}";

        debugText.text = "R: " +rightShoulder_Positions.Count + " |L: " + leftShoulder_Positions.Count+ "|  ANGLE: " + angle.ToString("F2");
    }

    public void testingAngles()
    {
        var direction = rightShoulder.transform.position - leftShoulder.transform.position;
        Debug.DrawRay(leftShoulder.transform.position, direction, Color.red);

        //var angle = Vector3.SignedAngle(direction,right, Vector3.up);// works good if x axis is aligned
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = 180 - angle;

        if(angle > 180)
            angle = 360 - angle;

        line.gameObject.SetActive(true);
        line.SetPosition(0, leftShoulder.transform.position);
        line.SetPosition(1, rightShoulder.transform.position);

        debugText.text = "test angle: " + angle.ToString("F2");
    }

    public void resetMeanValues()
    {
        var initCount = leftShoulder_Positions.Count;
        leftShoulder_Positions.Clear();
        rightShoulder_Positions.Clear();

        line.gameObject.SetActive(false);

        sumLx = sumLy = sumLz = sumRx = sumRy = sumRz = 0;

        Debug.Log($"RESETTING MEAN VALUES - Before {initCount} | Now {leftShoulder_Positions.Count}");
    }

    [HideInInspector]public bool isHeadTracked = false;
    [HideInInspector]public bool isShouldersTracked = false;
    [HideInInspector]public bool isHipsTracked = false;
    [HideInInspector]public bool isKneesTracked = false;
    [HideInInspector]public bool isAnklesTracked = false;


    public void changeTrackingStatus(string boneName)//TRACKING OF BONES ON|OFF
    {
        switch (boneName)
        {
            case "head":
                print("Pressed head");
                isHeadTracked = !isHeadTracked;
                break;
            case "shoulders":
                print("Pressed shoulders");
                isShouldersTracked = !isShouldersTracked;
                break;
            case "hips":
                print("Pressed hips");
                isHipsTracked = !isHipsTracked;
                break;
            case "knees":
                print("Pressed knees");
                isKneesTracked = !isKneesTracked;
                break;
            case "ankles":
                print("Pressed ankles");
                isAnklesTracked = !isKneesTracked;
                break;
            default:
                print("Incorrect name of bones");
                break;
        }
    }

}