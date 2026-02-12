using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Configuración de Ataque y Salud")]
    public Transform player;
    public float distanciaAtaque = 1.5f;
    public float danioNormal = 25f;
    public float intervaloAtaque = 1.5f;
    private float tiempoSiguienteAtaque = 0f;
    [Range(0, 100)] public float probabilidadMuerteInstantanea = 15f;

    [Header("Configuración de Patrullaje y Visión")]
    public Transform patrolPoints;
    public float reachDistance = 0.7f;
    public float viewDistance = 10f;
    [Range(0, 180)] public float viewAngle = 70f;
    public LayerMask obstacleMask;

    [Header("Salud del Enemigo")]
    public int vidaMaxima = 3;          // 🔴 VIDA AÑADIDA
    private int vidaActual;
    private bool estaMuerto = false;    // 🔴 CONTROL DE MUERTE

    private NavMeshAgent agent;
    private Transform pA, pB;
    private int currentTarget;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        vidaActual = vidaMaxima; // 🔴 Inicializamos vida
    }

    private void Start()
    {
        int enemyIndex = transform.GetSiblingIndex();
        AssignPatrolPair(enemyIndex);

        if (pA != null) agent.SetDestination(pA.position);
    }

    void Update()
    {
        if (estaMuerto) return; // 🔴 Si está muerto no hace nada

        if (SeePlayer())
        {
            agent.SetDestination(player.position);

            float dist = Vector3.Distance(transform.position, player.position);
            if (dist < distanciaAtaque && Time.time >= tiempoSiguienteAtaque)
            {
                LogicaAtaque();
                tiempoSiguienteAtaque = Time.time + intervaloAtaque;
            }
        }
        else
        {
            Patrol();
        }
    }

    void LogicaAtaque()
    {
        float dado = Random.Range(0f, 100f);
        SaludJugador scriptSalud = player.GetComponent<SaludJugador>();

        if (scriptSalud == null) return;

        if (dado <= probabilidadMuerteInstantanea)
        {
            Debug.Log("¡ATAQUE DEFINITIVO!");
            IrAGameOver();
        }
        else
        {
            scriptSalud.RecibirDanio(danioNormal);
            if (scriptSalud.saludActual <= 0) IrAGameOver();
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
        if (Physics.Raycast(origin, dir, dist, obstacleMask)) return false;

        return true;
    }

    void Patrol()
    {
        if (pA == null || pB == null) return;
        if (agent.pathPending) return;

        if (agent.remainingDistance <= reachDistance)
        {
            currentTarget = 1 - currentTarget;
            agent.SetDestination((currentTarget == 0 ? pA : pB).position);
        }
    }

    void AssignPatrolPair(int enemyIndex)
    {
        if (patrolPoints == null) return;
        int n = patrolPoints.childCount;
        if (n < 2) return;

        int a = enemyIndex % n;
        int b = (enemyIndex + 1) % n;

        pA = patrolPoints.GetChild(a);
        pB = patrolPoints.GetChild(b);
    }

    void IrAGameOver()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("GameOver");
        Invoke("CargarEscena2", 3f);
    }

    void CargarEscena2()
    {
        SceneManager.LoadScene("Scene2");
        Destroy(this.gameObject);
    }

    // 🔴 ==============================
    // 🔴 SISTEMA DE DISPARO AÑADIDO
    // 🔴 ==============================

    public void TakeDamage(int damage)
    {
        if (estaMuerto) return;

        vidaActual -= damage;
        Debug.Log("Enemigo recibe daño. Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        estaMuerto = true;

        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        Destroy(gameObject);
    }
}
