using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Attributes
    [SerializeField] int life;
    [SerializeField] int maxLife;
    [SerializeField] int damage;

    public int movePoints;

    // Movement
    private bool isTravelling = false;
    [SerializeField] private float speed;
    [SerializeField] private float distance;

    [SerializeField] Transform player;

    [SerializeField] LayerMask floorMask;
    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask playerMask;


    // Other
    [SerializeField] Slider lifeBar;

    void Start()
    {
      
    }

    void Update()
    {
        lifeBarUpdater();
        Move();
    }

    // Enemy Movement
    void Move()
    {
        if (!isTravelling && movePoints >= 100)
        {
            Vector3 direction = Vector3.zero;

            Vector3 difference = player.position - transform.position;

            if (Mathf.Abs(difference.x) <= 1 && Mathf.Abs(difference.z) <= 1)
            {
                AttackPlayer(difference.normalized);
                return;
            }

            float dx = Mathf.Abs(difference.x);
            float dz = Mathf.Abs(difference.z);

            if (dx > dz)
            {
                // Move along x-axis
                if (difference.x > 0 && IsMoveValid(Vector3.right))
                {
                    EnemyMove(Vector3.right);
                }
                else if (difference.x < 0 && IsMoveValid(Vector3.left))
                {
                    EnemyMove(Vector3.left);
                }
            }
            else
            {
                // Move along z-axis
                if (difference.z > 0 && IsMoveValid(Vector3.forward))
                {
                    EnemyMove(Vector3.forward);
                }
                else if (difference.z < 0 && IsMoveValid(Vector3.back))
                {
                    EnemyMove(Vector3.back);
                }
            }
        }
    }

    void EnemyMove(Vector3 direction)
    {
        Vector3 destination = transform.position + direction * distance;
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
    }


    // Collisions/Movechecks
    bool IsMoveValid(Vector3 direction)
    {
        bool hasFloor = FloorCheck(direction);
        bool hasWall = WallCheck(direction);
        bool hasPlayer = PlayerCheck(direction);

        return hasFloor && hasWall && hasPlayer;
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
        movePoints = 100;
    }

    public int GetmP()
    {
        return movePoints;
    }
}
