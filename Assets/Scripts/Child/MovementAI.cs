using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementAI : MonoBehaviour
{
    [Header("Child Movement Values")]
    [Tooltip("Generic Movement and Turning from Wall Values")]
    public float speed = 1.5f;
    public float obstacleRange = 5.0f;

    [Header("Santa Object Selector")]
    [Tooltip("Choose the Object")]
    [SerializeField] private GameObject player;

    [Header("Prefab Selector")]
    [Tooltip("Choose the Object")]
    [SerializeField] private GameObject giftPrefab;
    private GameObject gift;
    public bool beginGift;

    private Rigidbody rb;
    public NavMeshAgent agent;

    [SerializeField] private Vector3 point;

    public GameObject floor;
    [SerializeField] private float rangeX; //13
    [SerializeField] private float rangeZ; //13

    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Santa");
        floor = GameObject.FindWithTag("Floor");
    }

    // Start is called before the first frame update
    void Start()
    {
        rangeX = floor.transform.localScale.x * 4.0f;
        rangeZ = floor.transform.localScale.z * 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        gift = GameObject.FindWithTag("Gift");
        rb.constraints = RigidbodyConstraints.FreezePositionY;

        // FollowSanta();
        Movement();
        DetectWalls();
    }

    void Movement()
    {
        Vector3 currPos = this.transform.position;

        Debug.Log(agent.pathStatus + ": Path Status");

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            point = new Vector3((Random.Range(-rangeX,rangeX) + currPos.x), transform.position.y, (Random.Range(-rangeZ,rangeZ) + currPos.z));
            agent.SetDestination(point);
        }
        // Debug.Log("Point Value: " + point);
    }

    //Determine current distance and angle between player and child
    void FollowSanta()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        //TODO: Initiate the gift throwing aspect
        //If within 5 units of distance of player, send child towards player
        
        //if (distance <= obstacleRange && distance >= 1.5f)
        if (distance <= obstacleRange)
        {
            // Debug.Log("FOLLOWING PLAYER");
            Vector3 santaPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
            transform.LookAt(santaPosition);
            transform.position = Vector3.Slerp(this.transform.position, santaPosition, Time.deltaTime/4.0f);
            // StartCoroutine(ThrowGift());
        }
    }

    //Detect Walls and Rotate
    void DetectWalls()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.75f, out hit))
        {   
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    //Child begins to throw gift forward
    private IEnumerator ThrowGift()
    {
        if (gift == null)
        {
            gift = Instantiate(giftPrefab) as GameObject;
            gift.transform.position = transform.TransformPoint(Vector3.forward * 1.15f);
            gift.transform.rotation = transform.rotation;
        }

        yield return new WaitForSeconds(0.7f);
        // StartCoroutine(ThrowGift());
    }
}
