using UnityEngine;

/// <summary>
/// کلاس پایه برای دشمنان (شیاطین)
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("آمار")]
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int attack = 8;
    [SerializeField] private int experienceReward = 50;
    [SerializeField] private int coinReward = 10;
    
    [Header("حرکت")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float chaseDistance = 15f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float attackCooldown = 2f;
    
    [Header("انیمیشن")]
    [SerializeField] private Animator animator;
    
    private int currentHealth;
    private Transform playerTransform;
    private PlayerStats playerStats;
    private float lastAttackTime;
    private bool isAlive = true;

    private void Start()
    {
        currentHealth = maxHealth;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerStats = player.GetComponent<PlayerStats>();
        }
        
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAlive) return;
        
        if (playerTransform == null) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        if (distanceToPlayer < chaseDistance)
        {
            ChasePlayer(distanceToPlayer);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    /// <summary>
    /// تعقیب بازیکن
    /// </summary>
    private void ChasePlayer(float distance)
    {
        if (distance > attackDistance)
        {
            // حرکت به سمت بازیکن
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            
            // چرخش به سمت بازیکن
            transform.LookAt(playerTransform);
            
            animator.SetBool("IsWalking", true);
        }
        else
        {
            // حمله به بازیکن
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                AttackPlayer();
            }
        }
    }

    /// <summary>
    /// حمله به بازیکن
    /// </summary>
    private void AttackPlayer()
    {
        lastAttackTime = Time.time;
        animator.SetTrigger("Attack");
        
        if (playerStats != null)
        {
            playerStats.TakeDamage(attack);
            Debug.Log($"👹 دشمن حمله کرد! آسیب: {attack}");
        }
    }

    /// <summary>
    /// دریافت آسیب
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"👹 دشمن آسیب دید: {damage} | سلامتی باقی: {currentHealth}");
        
        animator.SetTrigger("Hit");
        
        if (currentHealth <= 0)
            Die();
    }

    /// <summary>
    /// مرگ دشمن
    /// </summary>
    private void Die()
    {
        isAlive = false;
        animator.SetTrigger("Die");
        
        // پاداش بازیکن
        if (playerStats != null)
        {
            playerStats.AddExperience(experienceReward);
            playerStats.AddCoins(coinReward);
        }
        
        Debug.Log($"💀 دشمن مرد! پاداش: {experienceReward} تجربه + {coinReward} سکه");
        
        // حذف دشمن بعد از انیمیشن
        Destroy(gameObject, 2f);
    }

    /// <summary>
    /// بررسی اینکه دشمن زنده است یا نه
    /// </summary>
    public bool IsAlive => isAlive;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
}
