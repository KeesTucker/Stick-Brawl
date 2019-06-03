using UnityEngine;
using Mirror;

public class IDKeeper : MonoBehaviour {

    //[SyncVar]
    public int ID = 1; //this will be changed in multiplayer to identify players;

    public int count = 0;

    public GameObject[] body = new GameObject[13];

    void Start()
    {
        ID = GameObject.Find("_NetworkManager").GetComponent<LocalId>().currentID;
        GameObject.Find("_NetworkManager").GetComponent<LocalId>().currentID++;
        AddList(transform);
        GetComponent<ID>().IDPlayer = ID;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<ID>().IDPlayer = ID;
            AddList(transform.GetChild(i).GetChild(0));
            if (transform.GetChild(i).childCount == 2)
            {
                transform.GetChild(i).GetChild(1).GetComponent<ID>().IDPlayer = ID;
                AddList(transform.GetChild(i).GetChild(1).GetChild(0));
            }
        }
        foreach (GameObject playerPart in body)
        {
            foreach (GameObject playerPart2 in body)
            {
                Physics.IgnoreCollision(playerPart2.GetComponent<Collider>(), playerPart.GetComponent<Collider>());
            }
        }
    }

    public void AddList(Transform t)
    {
        body[count] = t.gameObject;
        count++;
    }
}
