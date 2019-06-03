using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ChunkLoad : NetworkBehaviour {

    public float centre;
    public Transform local;
    public float distance;
    public bool last = true;
    public bool lastX = true;
    public int cullD = 500;
    public int cullF = 10000;
    //Renderer r;
    // Use this for initialization
    void Start () {
        transform.parent = GameObject.Find("Terrain").transform;
        //r = GetComponent<Renderer>();
        centre = transform.GetChild(0).position.x + transform.GetChild(0).gameObject.GetComponent<ChunkData>().width / 2;
    }
	
	// Update is called once per frame
	void Update () {
        if (local)
        {
            distance = Mathf.Abs(centre - local.position.x);
            /*if (distance > cullF && lastX)
            {
                if (false)
                {
                    Debug.Log("wh");
                    transform.GetChild(0).gameObject.SetActive(false);
                    lastX = false;
                }
            }
            else
            {*/
                if (distance < cullF && !lastX)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    lastX = true;
                }
                if (distance > cullD && last)
                {
                    foreach (Transform child in transform.GetChild(0).GetComponentsInChildren<Transform>())
                    {
                        if (!child.gameObject.name.Contains("Chunk"))
                        {
                            if (child.gameObject.GetComponent<SpriteRenderer>())
                            {
                                child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                            }
                            if (child.gameObject.GetComponent<Movable>())
                            {
                                child.gameObject.SetActive(true);
                            }
                            else
                            {
                                child.gameObject.SetActive(false);
                            }
                        }
                    }


                    last = false;
                }
                else if (distance < cullD && !last)
                {
                    foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
                    {
                        if (child.gameObject.GetComponent<SpriteRenderer>())
                        {
                            child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                        }
                        child.gameObject.SetActive(true);
                    }
                    last = true;
                    transform.GetChild(0).gameObject.GetComponent<BiomeHolder>().GetBiome();
                }
            //}
        }
    }
}
