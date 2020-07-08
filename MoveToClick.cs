using UnityEngine;
using UnityEngine.AI;

//Moves object to clicked position (diablo like movement for player). Needs a baked navmesh !

[RequireComponent(typeof(NavMeshAgent))]
public class MoveToClick : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }
}
