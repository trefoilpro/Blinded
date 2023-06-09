using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingAndChasingEnemy : MonoBehaviour
{
    [SerializeField] private RoomConductor _roomConductor;
    [SerializeField] private EnemyAnimations _enemyAnimations;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Collider _enemyCollider;
    [SerializeField] private float _speedWalk;
    [SerializeField] private float _speedRun;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _viewAngle;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private Transform[] _wayPoints;


    private float _distanceToFeelPlayer = 10f;
    private float _timeToFeelPlayer = 3f;

    private float _waitTimeOnPatrolPoint = 2f;
    private float _waitTimeAfterLostPlayer = 5f;
    private float _meshResolution = 1f;
    private int _edgeIterations = 4;
    private float _edgeDistance = 0.5f;

    private int m_СurrentWayPointIndex;

    private Vector3 m_PlayerPosition;
    private float m_WaitTimeOnPatrolPoint;
    private float m_WaitTimeAfterLostPlayer;
    private bool m_IsPatrol;
    private bool m_CaughtPlayer;


    private void Awake()
    {
        _roomConductor.RemakeMonstersEnviromentView += ChangeLayoutsVisibility;
    }

    private void OnDisable()
    {
        _roomConductor.RemakeMonstersEnviromentView -= ChangeLayoutsVisibility;
    }

    private void ChangeLayoutsVisibility(bool canSeeThroughWalls)
    {
        if (canSeeThroughWalls && (_obstacleMask & (1 << LayerMask.NameToLayer("ClosedRoomWalls"))) != 0)
        {
            Debug.Log("ChangeLayoutsVisibility to " + "Add ClosedRoomWalls");
            _obstacleMask &= ~(1 << LayerMask.NameToLayer("ClosedRoomWalls"));
        }
        else if (!canSeeThroughWalls && (_obstacleMask & (1 << LayerMask.NameToLayer("ClosedRoomWalls"))) == 0)
        {
            Debug.Log("ChangeLayoutsVisibility to " + "Remove ClosedRoomWalls");
            _obstacleMask |= 1 << LayerMask.NameToLayer("ClosedRoomWalls");
        }
    }

    private void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_WaitTimeOnPatrolPoint = _waitTimeOnPatrolPoint;
        m_WaitTimeAfterLostPlayer = _waitTimeAfterLostPlayer;

        m_СurrentWayPointIndex = 0;

        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = _speedWalk;
        Move(_speedWalk);
        _navMeshAgent.SetDestination(_wayPoints[m_СurrentWayPointIndex].position);
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
    }

    private void Chasing()
    {
        
        
        Vector3 dirToPlayer = (Player.Instance.transform.position - transform.position).normalized;
        float dstToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);

        if (!m_CaughtPlayer && !Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, _obstacleMask) && !Player.Instance.IsHidden)
        {
            Run(_speedRun);
            _navMeshAgent.SetDestination(m_PlayerPosition);
            m_WaitTimeAfterLostPlayer = _waitTimeAfterLostPlayer;
            Debug.Log("Physics.Raycast");
        }
        
        Debug.Log("_navMeshAgent.destination = " + _navMeshAgent.destination + " Enemy position = " + transform.position + " _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance = " + (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance) + " _navMeshAgent.remainingDistance = " + _navMeshAgent.remainingDistance + " _navMeshAgent.stoppingDistance = " + _navMeshAgent.stoppingDistance);


        //Debug.Log("_navMeshAgent.remainingDistance = " + _navMeshAgent.remainingDistance + " _navMeshAgent.stoppingDistance = " + _navMeshAgent.stoppingDistance);
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && Vector3.Distance(transform.position, _navMeshAgent.destination) <= _navMeshAgent.stoppingDistance)
        {
            
            
            Stop();
            //Debug.Log("_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance");
            if (m_WaitTimeAfterLostPlayer <= 0 && !m_CaughtPlayer)
            {
                m_IsPatrol = true;
                Move(_speedWalk);
                m_WaitTimeAfterLostPlayer = _waitTimeAfterLostPlayer;
                _navMeshAgent.SetDestination(_wayPoints[m_СurrentWayPointIndex].position);
            }
            else
            {
                /*if (Vector3.Distance(transform.position,
                        Player.Instance.gameObject.transform.position) >= 2.5f)
                    Stop();*/
                m_WaitTimeAfterLostPlayer -= Time.deltaTime;
            }

            Debug.Log("m_WaitTime = " + m_WaitTimeAfterLostPlayer);
        }
    }

    private void Patroling()
    {
        _navMeshAgent.SetDestination(_wayPoints[m_СurrentWayPointIndex].position);
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            if (m_WaitTimeOnPatrolPoint <= 0)
            {
                NextPoint();
                Move(_speedWalk);
                m_WaitTimeOnPatrolPoint = _waitTimeOnPatrolPoint;
            }
            else
            {
                Stop();
                m_WaitTimeOnPatrolPoint -= Time.deltaTime;
            }
        }
    }

    public void NextPoint()
    {
        m_СurrentWayPointIndex = (m_СurrentWayPointIndex + 1) % _wayPoints.Length;
        _navMeshAgent.SetDestination(_wayPoints[m_СurrentWayPointIndex].position);
    }

    void Stop()
    {
        _enemyAnimations.SetEnemyAnimation(EnemyAnimations.TypesOfAnimations.Idle);
        _navMeshAgent.isStopped = true;
        _navMeshAgent.speed = 0;
    }

    void Move(float speed)
    {
        _enemyAnimations.SetEnemyAnimation(EnemyAnimations.TypesOfAnimations.Walk);
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
    }

    private void Run(float speed)
    {
        _enemyAnimations.SetEnemyAnimation(EnemyAnimations.TypesOfAnimations.Run);
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
    }

    private void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, _viewRadius, _playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float dstToPlayer = Vector3.Distance(transform.position, player.position);

            if (Vector3.Angle(transform.forward, dirToPlayer) < _viewAngle / 2)
            {
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, _obstacleMask))
                {
                    m_IsPatrol = false;
                }
            }
            else if (dstToPlayer <= _distanceToFeelPlayer)
            {
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, _obstacleMask))
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _enemyCollider.enabled = false;
            KillPlayer();
        }
    }

   

    

    private void KillPlayer()
    {
        StartCoroutine(KillPlayerAnimationCoroutine());
    }

    private IEnumerator KillPlayerAnimationCoroutine()
    {
        Player player = Player.Instance;
        
        
        enabled = false;
        _navMeshAgent.enabled = false;
        _enemyAnimations.SetEnemyAnimation(EnemyAnimations.TypesOfAnimations.Idle);
            
        Quaternion newQuaternion = Quaternion.LookRotation(player.transform.position - transform.position);
            
        Quaternion newEnemyQuaternion = new Quaternion(0, newQuaternion.eulerAngles.y + 55, 0, newQuaternion.w);
            
        gameObject.transform.DORotate(newEnemyQuaternion.eulerAngles, 0.5f);
        //_enemyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            

        player.SetFirstPersonController(false);
        player.GetPlayerAnimation().SetPlayerAnimation(PlayerMovementAnimation.TypesOfMovement.Idle);
        player.GetPlayerRigidbody().constraints = RigidbodyConstraints.FreezeAll;
        
        RaycastHit hit;
        
        
        if (Physics.Raycast(player.transform.position, Vector3.down, out hit))
        {
            hit.point = new Vector3(hit.point.x, hit.point.y + 1f, hit.point.z);
            Debug.Log("hit.point = " + hit.point);
            player.transform.DOMove(hit.point, 0.2f);
        }

        yield return new WaitForSeconds(0.21f);
        
        Vector3 newEnemyPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        
        newQuaternion = Quaternion.LookRotation(newEnemyPosition - player.transform.position);

        Quaternion newQuaternionBody = new Quaternion(0, newQuaternion.y, 0, newQuaternion.w);
        Quaternion newQuaternionCamera = new Quaternion(newQuaternion.x, 0, 0, newQuaternion.w);


        player.transform.DOLocalRotate(newQuaternionBody.eulerAngles, 0.5f);
        player.GetPlayerCamera().transform.DOLocalRotate(newQuaternionCamera.eulerAngles, 0.5f);
            
        yield return new WaitForSeconds(0.5f);
        
        _enemyAnimations.SetEnemyAnimation(EnemyAnimations.TypesOfAnimations.KillPlayer);
    }
}