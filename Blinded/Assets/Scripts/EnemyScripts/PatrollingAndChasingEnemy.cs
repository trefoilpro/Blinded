using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingAndChasingEnemy : MonoBehaviour
{
    [SerializeField] private EnemyAnimations _enemyAnimations;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float viewRadius = 15f;
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Transform[] wayPoints;

    private float startWaitTime = 0.5f;
    private float speedWalk = 6f;
    private float speedRun = 9f;

    private float meshResolution = 1f;
    private int edgeIterations = 4;
    private float edgeDistance = 0.5f;

    private int m_СurrentWayPointIndex;

    private Vector3 m_PlayerPosition;
    private float m_WaitTime;
    private bool m_IsPatrol;
    private bool m_CaughtPlayer;


    private void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_WaitTime = startWaitTime;

        m_СurrentWayPointIndex = 0;

        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speedWalk;
        _navMeshAgent.SetDestination(wayPoints[m_СurrentWayPointIndex].position);
    }

    private void Update()
    {
        EnviromentView();

        if (!m_IsPatrol)
        {
            Chasing();
        }
        else
        {
            Patroling();
        }
        //Debug.Log("remainingDistance = " + _navMeshAgent.remainingDistance + " stoppingDistance = " + _navMeshAgent.stoppingDistance);
    }

    private void Chasing()
    {
        if (!m_CaughtPlayer)
        {
            Run(speedRun);
            _navMeshAgent.SetDestination(m_PlayerPosition);
        }

        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            Debug.Log("_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance");
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position,
                    Player.Instance.gameObject.transform.position) >= 6f)
            {
                m_IsPatrol = true;
                Run(speedWalk);
                m_WaitTime = startWaitTime;
                _navMeshAgent.SetDestination(wayPoints[m_СurrentWayPointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position,
                        Player.Instance.gameObject.transform.position) >= 2.5f)
                    Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    private void Patroling()
    {
        _navMeshAgent.SetDestination(wayPoints[m_СurrentWayPointIndex].position);
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            if (m_WaitTime <= 0)
            {
                NextPoint();
                Move(speedWalk);
                m_WaitTime = startWaitTime;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    public void NextPoint()
    {
        m_СurrentWayPointIndex = (m_СurrentWayPointIndex + 1) % wayPoints.Length;
        _navMeshAgent.SetDestination(wayPoints[m_СurrentWayPointIndex].position);
    }

    void Stop()
    {
        _enemyAnimations.SetEnemyAnimation(EnemyAnimations.TypesOfMovement.Idle);
        _navMeshAgent.isStopped = true;
        _navMeshAgent.speed = 0;
    }

    void Move(float speed)
    {
        _enemyAnimations.SetEnemyAnimation(EnemyAnimations.TypesOfMovement.Walk);
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
    }

    private void Run(float speed)
    {
        _enemyAnimations.SetEnemyAnimation(EnemyAnimations.TypesOfMovement.Run);
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
    }

    private void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_IsPatrol = false;
                }
            }

            if (!m_IsPatrol)
            {
                m_PlayerPosition = player.position;
            }
        }
    }
}