using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    // Variables

    // Player Stats - gameplay invisible
    public int Health = 10;
    public int MaxHealth = 10;
    public int Attack = 5;
    public float Evasion = 0f;
    public int HealthPotions = 0;
    public int SpeedPotions = 0;
    public int HolyShields = 0;

    // Player Stats - gameplay invisible
    public bool SpeedActive = false;
    public int speedDuration = 3;

    public bool ShieldActive = false;
    public int shieldDuration = 3;

    public int MaxAP = 100;
    public int ActionPoints = 100;

    // Movement
    private bool isTravelling = false;
    [SerializeField] private float speed;
    private float distance = 1f;

    // Raycast layer masks
    [SerializeField] LayerMask floorMask;
    [SerializeField] LayerMask wallMask;
    [SerializeField] LayerMask enemyMask;

    // UI
    [SerializeField] Slider HealthBar;
    [SerializeField] TMP_Text healthtxt;
    [SerializeField] TMP_Text attacktxt;
    [SerializeField] TMP_Text evadetxt;

    void Update()
    {
        // Ensures health doesn't go above the intended amount
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }

        Move();
        ConsumableUpgrades();
        SpeedPotionEffect();
        HolyShieldEffect();
        AttackAction();
        PlayerUIUpdater();
    }

    // Player Movement (Input)
    void Move()
    {
        if (!isTravelling && ActionPoints >= 100)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && FloorCheck(Vector3.forward) && WallCheck(Vector3.forward) && EnemyCheck(Vector3.forward))
            {
                PlayerMove(Vector3.forward);
            }

            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && FloorCheck(Vector3.back) && WallCheck(Vector3.back) && EnemyCheck(Vector3.back))
            {
                PlayerMove(Vector3.back);
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && FloorCheck(Vector3.left) && WallCheck(Vector3.left) && EnemyCheck(Vector3.left))
            {
                PlayerMove(Vector3.left);
            }

            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && FloorCheck(Vector3.right) && WallCheck(Vector3.right) && EnemyCheck(Vector3.right))
            {
                PlayerMove(Vector3.right);
            }
        }
    }

    // Player Movement (Logic)
    void PlayerMove(Vector3 direction)
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
        ActionPoints -= 100;
    }

    // Collision/Movechecks
    //Debug.DrawRay(transform.position + direction, Vector3.down * hit.distance, Color.green);
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

    bool EnemyCheck(Vector3 direction)
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

    // For game start
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AxeBlock")
        {
            attacktxt.gameObject.SetActive(true);
            Destroy(other.gameObject);
        }
    }

    // Attacking
    void AttackEnemy(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1f, enemyMask))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.enemyTakeLife(Attack);
                ActionPoints -= 100;  
            }
            
        }
    }

    void AttackAction()
    {
        if (!isTravelling && ActionPoints >= 100)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
            {
                AttackEnemy(Vector3.forward);
            }

            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
            {
                AttackEnemy(Vector3.back);
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
            {
                AttackEnemy(Vector3.left);
            }

            if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
            {
                AttackEnemy(Vector3.right);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ActionPoints -= 100;
            }
        }
    }

    // Taking Damage
    public void TakeDamage(int damage)
    {
        if(Random.value <= Evasion)
        {
            // Player Dodges the attack
            return;
        }

        if (ShieldActive)
        {
            Health -= 0;
        }
        else
        {
            Health -= damage;
        }
        
        if (Health <= 0)
        {
            // Game Over
        }
    }

    // UI
    void PlayerUIUpdater()
    {
        HealthBar.value = Health;
        HealthBar.maxValue = MaxHealth;

        healthtxt.text = "Health: " + Health.ToString();
        attacktxt.text = "Attack: " + Attack.ToString();
        evadetxt.text = "Evasion: " + Evasion.ToString();
    }

    // Turn logic
    public void APreset()
    {
        ActionPoints = MaxAP;
    }

    public int GetAP()
    {
        return ActionPoints;
    }

    // Upgrades
    public void Heal(int healAmount)
    {
        Health += healAmount;
    }

    void ConsumableUpgrades()
    {
        // Use 1 health potion
        if (HealthPotions >= 1 && Input.GetKeyDown(KeyCode.Z))
        {
            if (Health != MaxHealth)
            {
                // Heals the players
                Heal(MaxHealth);
                HealthPotions -= 1;
            }
        }

        // Use 1 speed potion
        if (SpeedPotions >= 1 && Input.GetKeyDown(KeyCode.X) && SpeedActive != true)
        {
            // Increase player speed for 3 turns 
            SpeedPotions -= 1;
            ActionPoints = ActionPoints * 2;
            SpeedActive = true;
        }

        // Use 1 Holy Shield
        if (HolyShields >= 1 && Input.GetKeyDown(KeyCode.C) && ShieldActive != true)
        {
            // Shield player from damage for a 3 turns
            ShieldActive = true;
            HolyShields -= 1;
        }
    }

    // Gives the player more action points allowing them to act more per turn
    void SpeedPotionEffect()
    {
        if (SpeedActive == true)
        {
            MaxAP = 200;
        }
        else
        {
            MaxAP = 100;
            speedDuration = 3;
        }
    }

    void HolyShieldEffect()
    {
        if (ShieldActive != true)
        {
            shieldDuration = 3;
        }
    }

}
