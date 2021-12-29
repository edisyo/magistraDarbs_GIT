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
    public TextMeshProUGUI debugText;

    
    //BONES
    [HideInInspector]public GameObject head;
    [HideInInspector]public GameObject leftShoulder;
    [HideInInspector]public GameObject rightShoulder;
    [HideInInspector]public GameObject rightHip;
    [HideInInspector]public GameObject leftHip;
    [HideInInspector]public GameObject rightKnee;
    [HideInInspector]public GameObject leftKnee;
    [HideInInspector]public GameObject rightAnkle;
    [HideInInspector]public GameObject leftAnkle;
    

    public LineRenderer line;

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
    }

    void OnHumanBodiesChanged(ARHumanBodiesChangedEventArgs eventArgs)
    {
        BoneController boneController;

        foreach (var humanBody in eventArgs.added)
        {
            if (!m_SkeletonTracker.TryGetValue(humanBody.trackableId, out boneController))
            {
                //Debug.Log($"Adding a new skeleton [{humanBody.trackableId}].");
                var newSkeletonGO = Instantiate(skeletonPrefab, humanBody.transform);

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

        foreach (var humanBody in eventArgs.updated)
        {
            if (m_SkeletonTracker.TryGetValue(humanBody.trackableId, out boneController))
            {
                boneController.ApplyBodyPose(humanBody);
                getTrackedBones(bonesToTrack);
            }

            if (bonesToTrack != null)
            {
                //bonesToTrack = boneController.GetComponentInChildren<boneToTrack>();
                //debugText.text = "" + bonesToTrack.gameObject.name;

                bonesToTrack = boneController.skeletonRoot.GetComponentsInChildren<boneToTrack>();

                if(isShouldersTracked)
                {
                    checkShoulderAngles(bonesToTrack);
                }else
                {
                    debugText.text = $"Meklē cilvēka ķermeni... \n";
                }
                
                //foreach (boneToTrack boneToTrack in bonesToTrack)
                //{
                //    debugText.text = $"Updated Bone: {boneToTrack.transform.parent.name} \n";
                //}
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

    private void checkShoulderAngles(boneToTrack[] bones)
    {
        //debugText.text = $" Bone 1: {leftArm} and Bone 2: {rightArm} \n";
        // rightShoulder.GetComponent<Renderer>().material.color = Color.green;
        // leftShoulder.GetComponent<Renderer>().material.color = Color.green;

        var direction = rightShoulder.transform.position - leftShoulder.transform.position;
        var up = transform.up;
        var angle = Vector3.Angle(up,direction);

        line.gameObject.SetActive(true);
        line.SetPosition(0, leftShoulder.transform.position);
        line.SetPosition(1, rightShoulder.transform.position);

        debugText.text = "ANGLE: " + (angle - 90).ToString("F2");
    }

    public bool isHeadTracked = false;
    public bool isShouldersTracked = false;
    public bool isHipsTracked = false;
    public bool isKneesTracked = false;
    public bool isAnklesTracked = false;


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