using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    public GameObject escape;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("esc"))
        {
            GameObject esc = Instantiate(escape, GameObject.Find("Canvas").transform);
            esc.GetComponent<Animator>().SetTrigger("Entry");
        }
    }

    public void Esc()
    {
        GameObject esc = Instantiate(escape, GameObject.Find("PlayerUI").transform);
        esc.GetComponent<Animator>().SetTrigger("Entry");
    }
}
