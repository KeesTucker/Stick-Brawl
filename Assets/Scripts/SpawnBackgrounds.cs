using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBackgrounds : MonoBehaviour {

    public GameObject backGround;

    public int frames;
    public GameObject back;

    public bool inLoadScene = false;
    private int layerMaskGround = 1 << 12;
    private RaycastHit hit;

    private Sprite last;

    private ColorBack cam;
    private Color lastColor;

    // Use this for initialization
    void Start () {
        frames = 60;
        cam = Camera.main.transform.GetChild(0).GetComponent<ColorBack>();
        if (!inLoadScene)
        {
            back = Instantiate(backGround, new Vector3(0, 0, 100), Quaternion.identity);
            back.GetComponent<MoveBackground>().cam = transform;
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y, 0), -Vector3.up, out hit, Mathf.Infinity, layerMaskGround);
            if (hit.collider.gameObject.name == "TerrainLoader(Clone)")
            {
                if (last != hit.collider.transform.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome.background)
                {
                    back.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = hit.collider.transform.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome.background;
                    back.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = hit.collider.transform.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome.background;
                    last = back.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
                    cam.biomeCol = hit.collider.transform.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome.backColor;
                    lastColor = cam.biomeCol;
                    cam.Color();
                }
            }
            else
            {
                StartCoroutine(FindTerrain(hit.collider.transform));
            }
        }
	}

    void Update()
    {
        frames++;
        if (frames > 60 && !inLoadScene)
        {
            frames = 0;
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y, 0), -Vector3.up, out hit, Mathf.Infinity, layerMaskGround);
            if (hit.collider.transform)
            {
                StartCoroutine(FindTerrain(hit.collider.transform));
            }
        }
    }

    IEnumerator FindTerrain(Transform parent)
    {
        if (parent)
        {
            if (parent.name == "TerrainLoader(Clone)")
            {
                if (last)
                {
                    if (last != parent.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome.background)
                    {
                        back.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                        back.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = parent.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome.background;
                        last = back.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
                        for (int i = 0; i < 100; i++)
                        {
                            back.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (float)i / 99f);
                            back.GetComponent<MoveBackground>().offset = Mathf.Lerp(back.GetComponent<MoveBackground>().offset, transform.position.x, (float)i / 99f);
                            Color c = parent.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome.backColor;
                            cam.biomeCol = Color.Lerp(lastColor, c, i / 99f);
                            cam.Color();
                            yield return new WaitForEndOfFrame();
                        }
                        back.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = parent.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome.background;
                        lastColor = cam.biomeCol;
                    }
                }
                else if (parent.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome)
                {
                    back.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    back.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = parent.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome.background;
                    last = back.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
                    back.GetComponent<MoveBackground>().offset = transform.position.x;
                    for (int i = 0; i < 100; i++)
                    {
                        back.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (float)i / 99f);
                        
                        Color c = parent.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome.backColor;
                        cam.biomeCol = Color.Lerp(lastColor, c, i / 99f);
                        cam.Color();
                        yield return new WaitForEndOfFrame();
                    }
                    back.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = parent.GetChild(0).gameObject.GetComponent<BiomeHolder>().biome.background;
                    lastColor = cam.biomeCol;
                }
            }
            else
            {
                StartCoroutine(FindTerrain(parent.parent));
            }
        }
        else
        {
            if (parent)
            {
                StartCoroutine(FindTerrain(parent.parent));
            }
        }
    }
}
