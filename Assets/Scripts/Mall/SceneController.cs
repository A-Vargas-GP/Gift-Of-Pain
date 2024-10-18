using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Child Object Selection")]
    [Tooltip("Object Selector")]
    [SerializeField] private GameObject childPrefab;
    public EnemyDamageScriptableObject childRef;
    public GameObject child;
    private Child_Stats childScript;

    [Header("Child Spawn Selection")]
    [Tooltip("Enable Child Spawning")]
    public bool enableSpawn;

    [Header("Child Object Stats")]
    [Tooltip("Child UI Script")]
    public PlayerSettingsScriptableObject user_interface;

    [Header("Gift Textures")]
    [Tooltip("List of Textures")]
    public List<Texture> giftTextures = new List<Texture>();
    private Renderer giftMesh;

    [Header("Prefab Selector")]
    [Tooltip("Choose the Object")]
    [SerializeField] private GameObject giftPrefab;
    private GameObject gift;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // Starting in 2 seconds with method triggering every 10 seconds
        InvokeRepeating("ChildSpawn", 0.0f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        enableSpawn = user_interface.enemySpawning;
        DestroyChild();
    }

    //Spawns Child from Door
    void ChildSpawn()
    {
        if (enableSpawn)
        {
            child = Instantiate(childPrefab);
            child.transform.position = this.transform.position + new Vector3(2, -1, 0);
            float angle = Random.Range(0, 180);
            child.transform.Rotate(0, angle, 0);
            // GiftSpawn();
        }
    }

    public void DestroyChild()
    {
        if (child != null)
        {
            childScript = child.GetComponent<Child_Stats>();
            childScript.DestroySelf(child);
        }
    }

    void GiftSpawn()
    {
        giftMesh = giftPrefab.GetComponent<Renderer>();
        int listNum = Random.Range(0, giftTextures.Count);
        giftMesh.sharedMaterial.SetTexture("_BaseColorMap", giftTextures[listNum]);  

        gift = Instantiate(giftPrefab) as GameObject;
        gift.transform.position = child.transform.TransformPoint(Vector3.forward);
        gift.transform.rotation = child.transform.rotation;
    }
}
