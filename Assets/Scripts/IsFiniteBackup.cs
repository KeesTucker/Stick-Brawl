using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFiniteBackup : MonoBehaviour {

    // Update is called once per frame
    public float x = 99999999f;
    public float y = 99999999f;
    public Vector3 pos;
    public int count;
    public bool isPlatform;

    IEnumerator Start()
    {
        Destroy(this);
        yield return new WaitForSeconds(0.5f);
        x = SyncData.worldSize * 1000;
        y = 2000;
    }

	void LateUpdate () {
        if (isPlatform)
        {
            if (transform.position.x == float.NaN || transform.position.y == float.NaN || transform.position.z == float.NaN)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.position.x > x || transform.position.x < -x || transform.position.y > y || transform.position.y < -y || transform.position.z > 200 || transform.position.z < -200 || transform.position.x == float.NaN || transform.position.y == float.NaN || transform.position.z == float.NaN)
            {
                if (count > 10)
                {
                    if (gameObject.tag == "PosRelay")
                    {
                        GetComponent<HealthAI>().health = -100;
                        StartCoroutine("Destroy");
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                    // FindParent(transform);
                }
                transform.position = pos;
                count++;
            }
            else
            {
                count = 0;
                pos = transform.position;
            }
        }
	}

    void FindParent(Transform t)
    {
        if (t.gameObject.layer == 24)
        {
            if (t.parent)
            {
                FindParent(t.parent);
            }
            else
            {
                Destroy(t.gameObject);
            }
        }
        else
        {
            Destroy(t.gameObject);
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
