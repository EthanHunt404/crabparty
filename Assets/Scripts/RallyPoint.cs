using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyPoint : MonoBehaviour
{
    private Rigidbody rb;

    private CrabAI crabAI;

    private GameObject player; 

    Vector3 worldPosition;

    public GameObject pole;
    public GameObject flag;

    [HideInInspector] public bool isRecalling;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        crabAI = GameObject.FindGameObjectWithTag("Crab").GetComponent<CrabAI>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ChangeRallyPoint();
        RecallCrabs();

        if(isRecalling == true)
        {
            rb.position = player.transform.position;

            flag.SetActive(false);
            pole.SetActive(false);
        }else if(isRecalling == false)
        {
            pole.SetActive(true);
            flag.SetActive(true);
        }

    }

    void ChangeRallyPoint()
    {
        if(Input.GetMouseButtonDown(1) && !crabAI.attackingEnemyFocused)
        {
            isRecalling = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitData;

            if(Physics.Raycast(ray, out hitData, 1000))
            {
                worldPosition = hitData.point;

                rb.position = worldPosition;
            }

        }
    }

    void RecallCrabs()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            isRecalling = true;
        }
    }
}
