using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class CrabAI : MonoBehaviour
{
    private Rigidbody rb;
    private RallyPoint rallyPointCS;

    public bool inRallyRadius;

    [Header("Setup Fields")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask everythingElseLayer;

    private GameObject rallyPoint;
    private NavMeshAgent agent;

    [HideInInspector] public bool attackingEnemyFocused;
    private GameObject focusedEnemy;
    

    // Start is called before the first frame update
    void Start()
    {
        attackingEnemyFocused = false;

        inRallyRadius = false;
        agent = GetComponent<NavMeshAgent>();

        rallyPointCS = GameObject.Find("RallyPoint").GetComponent<RallyPoint>();

        rallyPoint = GameObject.Find("RallyPoint");
    }

    // Update is called once per frame
    void Update()
    {
        CrabMovement();
        ClickEnemy();
        AttackEnemyFocused();
    }

    void CrabMovement()
    {
        if(inRallyRadius == false)
        {
            rallyPoint = GameObject.Find("RallyPoint");

            agent.SetDestination(rallyPoint.transform.position);

        }else if(inRallyRadius == true)
        {
            agent.SetDestination(this.gameObject.transform.position);
        }    
    }

    void ClickEnemy()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 1000, enemyLayer))
            {
                focusedEnemy = hit.transform.gameObject;

                attackingEnemyFocused = true;
            }else if(Physics.Raycast(ray, out hit, 1000, everythingElseLayer))
            {
                attackingEnemyFocused = false;
            }
        }
    }

    void AttackEnemyFocused()
    {
        if(attackingEnemyFocused == true)
        {
            rallyPointCS.isRecalling = false;
            rallyPointCS.pole.SetActive(false);
            rallyPointCS.flag.SetActive(false);
            agent.SetDestination(focusedEnemy.transform.position);
        }else
        {

            if(rallyPointCS.isRecalling)
            {  
                rallyPointCS.isRecalling = true;      
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("RallyPoint"))
        {
            inRallyRadius = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("RallyPoint"))
        {
            inRallyRadius = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("RallyPoint"))
        {
            inRallyRadius = false;
        }
    }
}
