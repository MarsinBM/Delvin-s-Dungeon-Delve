using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Enemy Stats
    [SerializeField] int life;
    [SerializeField] int maxLife;
    [SerializeField] int damage;

    [SerializeField] int movePoints;
    [SerializeField] int maxPoints;

    // Movement
    private bool isTravelling = false;
    private float speed = 12;
    private float distance = 1f;

    private Transform player;

    // Raycast layer masks
    [SerializeField] LayerMask floorMask;
    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask enemyMask;

    // Teleporting
    private Vector3 lastStablePos;
    public bool isTeleported;
    private Vector3 stopPos;

    // UI
    [SerializeField] Slider lifeBar;

    void Start()
    {
        lastStablePos = transform.position;

        GameObject ThePlayer = GameObject.Find("Player");
        player = ThePlayer.transform;

        EnemyScaling(LevelManager.instance.levelCounter);
    }

    // Increases the enemies stats every 5 levels
    void EnemyScaling(int gameLevel)
    {
        int HPincreaseAmount = 5;
        int DMGincreaseAmount = 1;

        if (gameLevel >= 5 && gameLevel % 5 == 0)
        {
            int Multiplier = (gameLevel / 5);
            int HPincrease = HPincreaseAmount * Multiplier;
            int DMGincrease = DMGincreaseAmount * Multiplier;

            maxLife += HPincrease;
            damage += DMGincrease;

            life = maxLife;
        }
    }

    void Update()
    {
        lifeBarUpdater();
        Move();
    }

    // Enemy Movement (Input)
    void Move()
    {
        if (!isTravelling && movePoints >= 100)
        {
            // Gets the player's position
            Vector3 difference = player.position - transform.position;
            float dx = Mathf.Abs(difference.x);
            float dz = Mathf.Abs(difference.z);

            // Attacks the player only if they are directly adjacent to them
            if ((dx == 1 && dz == 0) || (dz == 1 && dx == 0))
            {
                AttackPlayer(difference.normalized);
                return;
            }

            bool CanMove = false;

            // Move along the x axis
            if (dx > dz)
            {
                if (difference.x > 0 && IsMoveValid(Vector3.right))
                {
                    EnemyMove(Vector3.right);
                    CanMove = true;
                }
                else if (difference.x < 0 && IsMoveValid(Vector3.left))
                {
                    EnemyMove(Vector3.left);
                    CanMove = true;
                }
                else
                {
                    CanMove = false;
                }
            }
            // Move along the z axis
            else
            {
                if (difference.z > 0 && IsMoveValid(Vector3.forward))
                {
                    EnemyMove(Vector3.forward);
                    CanMove = true;
                }
                else if (difference.z < 0 && IsMoveValid(Vector3.back))
                {
                    EnemyMove(Vector3.back);
                    CanMove = true;
                }
                else
                {
                    CanMove = false;
                }
            }

            // If the enemy can't move they wait
            if (CanMove == false)
            {
                movePoints -= 100;
            }
        }
    }

    // Enemy Movement (Logic)
    void EnemyMove(Vector3 direction, Vector3? newDestination = null)
    {
        Vector3 destination = newDestination.HasValue ? newDestination.Value : transform.position + direction * distance;
        StartCoroutine(MoveToDest(destination));
    }

    IEnumerator MoveToDest(Vector3 destination)
    {
        isTravelling = true;
        while (Vector3.Distance(transform.position, destination) > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
        isTravelling = false;
        movePoints -= 100;
        UpdatePos();
    }

    // Collisions/Movechecks
    bool IsMoveValid(Vector3 direction)
    {
        bool hasFloor = FloorCheck(direction);
        bool hasWall = WallCheck(direction);
        bool hasPlayer = PlayerCheck(direction);
        bool hasOtherEnemy = OtherEnemyCheck(direction);

        return hasFloor && hasWall && hasPlayer && hasOtherEnemy;
    }

    bool FloorCheck(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + direction, Vector3.down, out hit, 1f, floorMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool WallCheck(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1f, wallMask))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool PlayerCheck(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1f, playerMask))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool OtherEnemyCheck(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1f, enemyMask))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(transform.GetSiblingIndex() == 0 || transform.GetSiblingIndex() % 2 == 0)
            {
                TeleportToPos();
                StopAllCoroutines();
            }
        }
    }

    // Teleporting
    private void UpdatePos()
    {
        lastStablePos = transform.position;
    }

    private void TeleportToPos()
    {
        transform.position = lastStablePos;
        EnemyMove(Vector3.zero, lastStablePos);
    }

    // Attacking
    void AttackPlayer(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1f, playerMask))
        {
            Player player = hit.collider.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                movePoints -= 100;
            }

        }
    }

    // Taking Damage
    public void enemyTakeLife(int playerAttack)
    {
        life -= playerAttack;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    // UI
    void lifeBarUpdater()
    {
        lifeBar.value = life;
        lifeBar.maxValue = maxLife;
    }

    // Turn Logic
    public void mPreset()
    {
        int maximumpoints;
        maximumpoints = maxPoints;
        movePoints = maximumpoints;
    }

    public int GetmP()
    {
        return movePoints;
    }
}
