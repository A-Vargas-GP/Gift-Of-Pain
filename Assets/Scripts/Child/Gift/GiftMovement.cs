using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftMovement : MonoBehaviour
{
    [Header("Gift Values")]
    [Tooltip("Values")]
    public float speed = 4.0f;
    public int damage = 1;
    private Vector3 originalPos;

    [Header("Gift Textures")]
    [Tooltip("List of Textures")]
    public List<Texture> giftTextures = new List<Texture>();
    private Renderer giftMesh;

    // Start is called before the first frame update
    void Start()
    {
        //Components to change the gift color
        giftMesh = this.GetComponent<Renderer>();
        int listNum = Random.Range(0, giftTextures.Count);
        giftMesh.material.SetTexture("_BaseColorMap", giftTextures[listNum]);

        //Component for destroying gift based on distance
        originalPos = transform.position;
    }

    void Update() 
    {   
        //Movement
        transform.Translate(0, 0, speed * Time.deltaTime);
        DestroyFromDistance();
    }

    //Collision to Santa Player; dissolve
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Santa") 
        {
            // Debug.Log("Player hit");
        }

        Destroy(this.gameObject);
    }

    void DestroyFromDistance()
    {
        float distance = Vector3.Distance(originalPos, this.transform.position);

        if (distance >= 10.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
