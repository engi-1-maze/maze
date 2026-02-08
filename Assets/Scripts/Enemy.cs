using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Transform patrolPoints;
    public Transform enemies;

    public float reachDistance = 0.7f;
    public float viewDistance = 10f;

    [Range(0, 180)] public float viewAngle = 70f;
    public LayerMask obstacleMask;

    public Detection detection;
    public bool showmsg = true;

    private NavMeshAgent agent;
    private Transform pA, pB;
    private int currentTarget;
    private bool warned;

    private void Awake()
    {
        agent=GetComponent<NavMeshAgent>(); 
    }

    private void Start()
    {
        int enemyIndex = transform.GetSiblingIndex();
        AssignPatrolPair(enemyIndex);

        if (pA != null)
            agent.SetDestination(pA.position);
    }

    void Update()
    {
        if (SeePlayer())
        {
            if(showmsg&&!warned)
            {
                detection?.Show("Detectado");
                warned = true;
            }
            agent.SetDestination(player.position);
        }
        else
        {
            warned = false;
            Patrol();
        }
    }
    void AssignPatrolPair(int enemyIndex)
    {
        int n=patrolPoints.childCount;
        int a=enemyIndex;
        int b = enemyIndex + 1;

        if(b<=n)
        {
            a = (enemyIndex) % (n - 1);
            b = a + 1;
        }
        pA=patrolPoints.GetChild(a);
        pB=patrolPoints.GetChild(b);
        currentTarget = 0;
    }

    void Patrol()
    {
        if(pA==null||pB==null) return;
        if(agent.pathPending) return;

        if(agent.remainingDistance<=reachDistance)
        {
            currentTarget=1-currentTarget;
            agent.SetDestination((currentTarget == 0 ? pA : pB).position);
        }

    }

    bool SeePlayer()
    {
        if (player == null) return false;

        Vector3 origin = transform.position + Vector3.up * 0.8f;
        Vector3 toPlayer = (player.position + Vector3.up * 0.8f) - origin;

        float dist = toPlayer.magnitude;
        if (dist > viewDistance) return false;

        Vector3 dir = toPlayer / dist;

        if (Vector3.Angle(transform.forward, dir) > viewAngle * 0.5f) return false;

        if(Physics.Raycast(origin,dir,dist,obstacleMask))
            return false;

        return true;
    }

}
    

