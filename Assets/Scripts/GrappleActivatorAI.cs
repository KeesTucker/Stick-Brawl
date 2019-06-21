using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleActivatorAI : MonoBehaviour {

    public Rigidbody rb;
    public GameObject grapple;
    public GameObject hand;
    public bool Collided;
    public float angle;
    public float angleTarg;
    public GameObject[] playerParts;
    public Vector3 hitCoords;
    public GameObject hitTarg;
    public bool backTime = false;
    public Vector3 direction;
    public Quaternion rotation;
    public GameObject handSingle;
    public Vector3 directionTarg;
    public Quaternion rotationTarg;
    public Renderer rend;
    public bool onLocal;
    public bool attractable = true;
    public AimShootAI aimShoot;
    public SyncMoveStateAI syncMoveState;
    public Material invisible;
    public GameObject parent; //AI that sent it
    public GameObject particle;
    public AudioSource audioSource;
    public AudioClip grappleSwoosh;
    public AudioClip grappled;
    public AudioClip grappleFail;
    public Transform local;

    IEnumerator Start()
    {
        while (!GameObject.Find("LocalPlayer") && !GameObject.Find("LoadingPlayer"))
        {
            yield return null;
        }
        if (GameObject.Find("LocalPlayer"))
        {
            local = GameObject.Find("LocalPlayer").transform;
        }
        else if (GameObject.Find("LoadingPlayer"))
        {
            local = GameObject.Find("LoadingPlayer").transform;
        }
        audioSource.PlayOneShot(grappleSwoosh, SyncData.sfx * 0.4f * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
        GameObject[] playerParts_ = parent.GetComponent<GroundForceAI>().playerParts;
        StartCoroutine("endTime");
        aimShoot = parent.transform.GetChild(0).gameObject.GetComponent<AimShootAI>();
    }
    public void Setup(Color syncedC)
    {
        rend.material.SetColor("_Color", syncedC);
    }

    void OnCollisionEnter(Collision collsionInfo)
    {
        //if (collsionInfo.collider.gameObject.layer != 15/* && backTime == false*/)
        //{
        if (collsionInfo.gameObject.tag == "NoGrapple")
        {
            backTime = true;
            audioSource.PlayOneShot(grappleFail, SyncData.sfx * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
            return;
        }
        else if (collsionInfo.gameObject.tag == "NoAttract")
        {
            attractable = false;
            audioSource.PlayOneShot(grappled, SyncData.sfx * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
        }
        else
        {
            audioSource.PlayOneShot(grappled, SyncData.sfx * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
        }
        grapple.layer = 14;
        hitTarg = collsionInfo.gameObject;
        hitCoords = transform.position - collsionInfo.gameObject.transform.position;
        rb.isKinematic = true;
        Collided = true;
        if (onLocal)
        {
            parent.GetComponent<GroundForceAI>().grappled = true;
        }
        transform.position = hitTarg.transform.position + hitCoords;
        transform.parent = hitTarg.transform;

        Instantiate(particle, transform.position, Quaternion.identity);

        rend.material = invisible;
        //}
    }

    void Update()
    {
        if (parent)
        {
            if (parent.GetComponent<PlayerControl>())
            {
                if (parent.GetComponent<PlayerControl>().e)
                {
                    if (onLocal)
                    {
                        parent.GetComponent<GroundForceAI>().grappled = false;
                    }
                    Destroy(grapple);
                }
            }
            else if (parent.GetComponent<BaseControl>())
            {
                if (parent.GetComponent<BaseControl>().e)
                {
                    if (onLocal)
                    {
                        parent.GetComponent<GroundForceAI>().grappled = false;
                    }
                    Destroy(grapple);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!backTime && !Collided)
        {
            if (rb.velocity.magnitude < 100)
            {
                rb.AddForce(rb.velocity.normalized * (100000 / 50) * Time.deltaTime);
            }
        }
        if (hand)
        {
            angle = AngleBetweenPoints(hand.GetComponent<Transform>().position, transform.position);
        }
        rotation = Quaternion.Euler(0, 0, angle);
        direction = rotation * -Vector3.right;
        if (Collided)
        {
            angleTarg = AngleBetweenPoints(hitTarg.transform.position, hand.GetComponent<Transform>().position);
            rotationTarg = Quaternion.Euler(0, 0, angleTarg);
            directionTarg = rotationTarg * -Vector3.right;
        }
        handSingle = hand;
        if (Collided)
        {
            //transform.position = hitTarg.transform.position + hitCoords;
            if (Vector2.Distance(new Vector2(handSingle.transform.position.x, handSingle.transform.position.y), new Vector2(transform.position.x, transform.position.y)) > 5)
            {
                handSingle.GetComponent<Rigidbody>().AddForce( new Vector3(direction.x * 40000 * Time.deltaTime, direction.y * 80000 * Time.deltaTime)/* * Mathf.Pow(Vector3.Distance(handSingle.transform.position, hitTarg.transform.position), 2f)*/);

                if (hitTarg.GetComponent<Rigidbody>() != null && attractable)
                {
                    hitTarg.GetComponent<Rigidbody>().AddForce(directionTarg * 30000 * Time.deltaTime/* * Mathf.Pow(Vector3.Distance(handSingle.transform.position, hitTarg.transform.position), 2f)*/);
                }
            }
        }
        if (backTime)
        {
            rb.AddForce(-direction * (90000 / 25) * Time.deltaTime);
            if (Vector3.Distance(handSingle.transform.position, transform.position) < 2)
            {
                if (onLocal)
                {
                    parent.GetComponent<GroundForceAI>().grappled = false;
                }
                Destroy(grapple);
            }
        }
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    IEnumerator endTime()
    {
        yield return new WaitForSeconds(0.47f);
        if (Collided == false)
        {
            backTime = true;
        }
    }
}

