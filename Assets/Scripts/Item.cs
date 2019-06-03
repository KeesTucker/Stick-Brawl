using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public int id = 0;

    new public string name = "New Item";
    public Sprite icon = null;
    public Vector3 muzzlePosition = new Vector3(0, 0, 0);

    public Vector3 position = new Vector3(0, 0, 0);

    public Vector3 positionFlipped = new Vector3(0, 0, 0);

    public Vector3 scale = new Vector3(0, 0, 0);

    public Vector3 rotation = new Vector3(0, 0, 0);

    public Vector3 rotationFlipped = new Vector3(0, 0, 0);

    public Vector3 spawnPosition = new Vector3(0, 0, 0);

    public Vector3 spawnScale = new Vector3(0, 0, 0);

    public Vector3 spawnRotation = new Vector3(0, 0, 0);

    public Vector3 switchOffset = new Vector3(0, 0, 0);

    public GameObject bullet;

    public GameObject shell;

    public int magazineSize = 10;

    public float reloadTime = 3f;

    public float mass;

    public int axisToFlip = 3;

    public float bloom = 2;

    public int recoil = 0;
    public int impact = 0;
    public float fireRate = 0;

    public Vector3 muzzleFlashScale = new Vector3(3.77f, 10f, 0);

    public GameObject itemModel = null;

    public int bulletSplit = 1;

    public int burstSize = 0;

    public float burstTime = 0;

    public bool mag = true;

    public int currentBullets = 10;

    public Vector3 ItemColliderPos = new Vector3(0, 0, 0);

    public float damage;

    public bool special = false;

    public AudioClip shot;
    public AudioClip reload;

    public bool heal;
}
