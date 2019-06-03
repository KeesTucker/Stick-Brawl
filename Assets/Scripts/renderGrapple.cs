using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderGrapple : MonoBehaviour {
    public GameObject LHT;
    public LineRenderer lineRenderer;
    public bool onLocal;
    public Color color;
    void Start()
    {   
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
    }

    public void Setup(Color syncedC)
    {
        color = syncedC;
        lineRenderer.material.SetColor("_Color", syncedC);
    }

    void Update()
    {
        if (lineRenderer.material.color != color)
        {
            lineRenderer.material.SetColor("_Color", color);
        }
        
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.SetPosition(0, LHT.GetComponent<Transform>().position);
        lineRenderer.startWidth = 0.4f;
        lineRenderer.endWidth = 0.4f;
    }
}
