using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncJetState : MonoBehaviour {

    public bool jetState;
    [SerializeField]
    public GameObject jetFlashL;
    [SerializeField]
    public Material jetMaterialL;
    [SerializeField]
    public GameObject jetFlashR;
    [SerializeField]
    public Material jetMaterialR;

    [SerializeField]
    public GameObject parent;
	// Use this for initialization
	void Start () {
        jetMaterialL = jetFlashL.GetComponent<Renderer>().material;
        jetMaterialL.SetFloat("Vector1_B173D9FB", 0);
        jetMaterialR = jetFlashR.GetComponent<Renderer>().material;
        jetMaterialR.SetFloat("Vector1_B173D9FB", 0);
    }
	
	// Update is called once per frame
	void Update () {
        jetState = parent.GetComponent<PlayerSetup>().jetOn;
        if (jetState)
        {
            jetMaterialL.SetFloat("Vector1_B173D9FB", 1);
            jetMaterialR.SetFloat("Vector1_B173D9FB", 1);
        }
        else
        {
            jetMaterialL.SetFloat("Vector1_B173D9FB", 0);
            jetMaterialR.SetFloat("Vector1_B173D9FB", 0);
        }
	}
}
