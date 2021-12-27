using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class createLine : MonoBehaviour
{
    public float height = 1f;
    public float textHeight = 30f;

    public RectTransform LrectTransform;
    public RectTransform RrectTransform;
    public RectTransform textTransform;
    float distZ = -0.02f;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    Mesh mesh;
    TextMeshProUGUI anglesText;
    float angle;

    Vector3 v1;
    Vector3 v2;
    Vector3 v3;
    Vector3 v4;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
        meshFilter = gameObject.AddComponent<MeshFilter>();
        mesh = new Mesh();

        anglesText = textTransform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        v1 = new Vector3(RrectTransform.anchoredPosition.x, RrectTransform.anchoredPosition.y - height, distZ);
        v2 = new Vector3(LrectTransform.anchoredPosition.x, LrectTransform.anchoredPosition.y - height, distZ);
        v3 = new Vector3(RrectTransform.anchoredPosition.x, RrectTransform.anchoredPosition.y + height, distZ);
        v4 = new Vector3(LrectTransform.anchoredPosition.x, LrectTransform.anchoredPosition.y + height, distZ);

        createQuad();
        calculateAngle();
        placeText();
    }

    void createQuad()
    {
        Vector3[] vertices = new Vector3[4]
        {
            v1,
            v2,
            v3,
            v4
        };
        mesh.vertices = vertices;
        int[] tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        mesh.uv = uv;
        meshFilter.mesh = mesh;
    }
    void calculateAngle()
    {
        Vector2 dir = LrectTransform.anchoredPosition - RrectTransform.anchoredPosition;
        Vector2 right = Vector2.right;

        angle = Vector3.SignedAngle(dir, right, Vector3.up); 

        angle = Mathf.Round(angle);
    }

    void placeText()//Makes the text be at the middle of the line
    {
        Vector2 getPos = new Vector2((v2.x + v1.x) / 2, (v2.y + v1.y) /2 + textHeight);//sum up left and right vertice's x and y positions, and get the middle
        textTransform.anchoredPosition = getPos;

        if(RrectTransform.anchoredPosition.y > LrectTransform.anchoredPosition.y)
        {
            //R higher
            textTransform.rotation = Quaternion.Euler(0f, 0f, -angle);
            anglesText.text = $"{-angle}";
        }else
        {
            //L higher
            textTransform.rotation = Quaternion.Euler(0f, 0f, angle);
            anglesText.text = $"{angle}";
        }
    }


}
