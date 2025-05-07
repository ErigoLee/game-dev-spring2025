using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack, Dead }

    public State currentState = State.Patrol;

    public float detectRange = 15f;
    public float attackRange = 7f;
    public float moveSpeed = 3f;
    public float rotateSpeed = 5f;
    //public int health = 100;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootCooldown = 2f;
    private float shootTimer = 0f;

    public float xMin = -20f;
    public float xMax = 20f;
    public float zMin = -20f;
    public float zMax = 20f;

    private Animator animator;
    private Transform player;
    private GameManager gameManager;
    private Vector3 patrolTarget;

    [SerializeField] private GameObject coinPrefab;

    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        gameManager = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        SetRandomPatrolTarget();
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                if (distanceToPlayer <= detectRange)
                {
                    currentState = State.Chase;
                }
                break;

            case State.Chase:
                Chase();
                if (distanceToPlayer <= attackRange)
                {
                    currentState = State.Attack;
                }
                else if (distanceToPlayer > detectRange)
                {
                    currentState = State.Patrol;
                    SetRandomPatrolTarget();
                }
                break;

            case State.Attack:
                Attack();
                if (distanceToPlayer > attackRange)
                {
                    currentState = State.Chase;
                }
                break;

            case State.Dead:
                break;
        }
    }

    void Patrol()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", true);
        MoveTo(patrolTarget);

        if (Vector3.Distance(transform.position, patrolTarget) < 1.5f)
        {
            SetRandomPatrolTarget();
        }
    }

    void Chase()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", true);
        MoveTo(player.position);
    }

    void Attack()
    {
        animator.SetBool("Attack", true);
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown)
        {
            //animator.SetTrigger("Shoot");
            Vector3 shootDir = bulletSpawnPoint.forward;
            Quaternion shootRot = Quaternion.LookRotation(shootDir) * Quaternion.Euler(90f, 0f, 0f);
            Instantiate(bulletPrefab, bulletSpawnPoint.position, shootRot);
            shootTimer = 0f;
        }
    }

    void MoveTo(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        Vector3 nextPosition = transform.position + direction * moveSpeed * Time.deltaTime;
        nextPosition.x = Mathf.Clamp(nextPosition.x, xMin, xMax);
        nextPosition.z = Mathf.Clamp(nextPosition.z, zMin, zMax);

        transform.position = nextPosition;
    }

    void SetRandomPatrolTarget()
    {
        float randX = Random.Range(xMin, xMax);
        float randZ = Random.Range(zMin, zMax);
        patrolTarget = new Vector3(randX, transform.position.y, randZ);
    }

/*
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
*/
    public void MakingCoin()
    {
        isDead = true;
        currentState = State.Dead;
        animator.SetBool("Die",true);
        float x = transform.position.x + 2.7f;
        float z = transform.position.z;
        float y = transform.position.y + 0.7f;
        Vector3 pos = new Vector3(x, y, z);
        GameObject _coin=Instantiate(coinPrefab, pos, Quaternion.Euler(0f, 90f, 90f));
        gameManager.settingCoinObj(_coin);
        gameManager.GettingEnemies();
        Destroy(gameObject);
    }

}
