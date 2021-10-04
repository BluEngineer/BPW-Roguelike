using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
	public enum Mode { Patrol, Search, Chase , Attack};

	NavMeshAgent nav;
	public float fov = 120f;
	Vector3 lastSeen;

	public Transform target;
	public Light alarmLight;
	public Mode mode;
	public float maxSpeed;
	public float patrolSpeed;
	public float stamina;
	//public float stoppingDistance = 0.5f;
	public RangedEnemy enemy;

	private float attackTime;
	public float attackRechargeTime = 0.5f;
	public float detectRange = 3;
	public LayerMask playerLayer;
	public LayerMask shootLayer;
    private bool canSee;
    private bool canShoot;


    [SerializeField]
    Collider[] playersInRange;

	// Use this for initialization
	void Start()
	{
		shootLayer = LayerMask.GetMask("Player");
		enemy = GetComponentInChildren<RangedEnemy>();
		nav = GetComponent<NavMeshAgent>();

		patrolSpeed = nav.speed;
		maxSpeed = patrolSpeed * 2;
		stamina = 1f;

		target = GameObject.FindGameObjectWithTag("Player").transform;
		SetMode(Mode.Patrol);
		StartCoroutine(PatrolBehaviour());
	}

	// Update is called once per frame
	void Update()
	{
		canSee = canSeeTarget();
		Vector3 direction = target.position - transform.position;
		switch (mode)
		{
			case Mode.Patrol:
				if (canSee)
				{
					SetMode(Mode.Chase);
				}
				break;
			case Mode.Search:
				if (nav.remainingDistance < 0.5f)
				{
					SetMode(Mode.Patrol);
				}
				break;
			case Mode.Chase:
				nav.destination = target.position;

				if (stamina > 0)
				{
					stamina -= 0.1f * Time.deltaTime;
				}
				nav.speed = patrolSpeed + (maxSpeed - patrolSpeed) * stamina;
				if (!canSee) 
				{
					SetMode(Mode.Search);
				}
				Attack();
					break;
			case Mode.Attack:
				break;
		}

	}

	IEnumerator PatrolBehaviour()
	{
		while (true)
		{
			if (mode == Mode.Patrol)
			{
				Vector3 destination = transform.position + new Vector3(Random.Range(-13, 13), 0f, Random.Range(-13, 13));
				nav.SetDestination(destination);
			}
			yield return new WaitForSeconds(Random.Range(4, 7));
		}
	}

	void SetMode(Mode m)
	{
		mode = m;
		switch (mode)
		{
			case Mode.Patrol:
				nav.speed = patrolSpeed;
				stamina = 1f;
                alarmLight.enabled = false;
                break;

			case Mode.Search:
				nav.SetDestination(lastSeen);
                alarmLight.enabled = false;
                break;

			case Mode.Chase:
                alarmLight.enabled = true;
				nav.speed = maxSpeed;
				lastSeen = target.position;
				CentralIntellignece.instance?.Alert(gameObject);
				break;

			case Mode.Attack:
                alarmLight.enabled = true;
                break;

		}
	}

	public void Alert()
	{
		if (mode != Mode.Chase)
		{
			SetMode(Mode.Chase);
		}
	}

	bool canSeeTarget()
	{
        
		//Collider[] objects = Physics.OverlapSphere(transform.position, detectRange, playerLayer);
		playersInRange = Physics.OverlapSphere(transform.position, detectRange, playerLayer);
		foreach (var obj in playersInRange )
		{
			if (obj.gameObject == target.gameObject)
			{
				RaycastHit hit;
				Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				Vector3 targetPos = new Vector3(target.position.x,target.position.y, target.position.z);
				if (Physics.Linecast(pos, targetPos,out hit))
				{
					if (hit.collider.gameObject.CompareTag("Player"))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	void Attack()
	{
		attackRechargeTime = Random.Range(enemy.minShootTime, enemy.maxShootTime);
		attackTime += Time.deltaTime;
		if (attackTime >= attackRechargeTime)
		{
			attackTime = 0.0f;
			enemy.AttackBehaviour();
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, detectRange);
	}
}
