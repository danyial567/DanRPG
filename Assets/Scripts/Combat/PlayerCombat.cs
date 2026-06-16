using UnityEngine;

/// <summary>
/// سیستم مبارزه کاراکتر
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    [Header("مبارزه")]
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private int attackDamage = 20;
    
    [Header("مهارت")]
    [SerializeField] private float skillCooldown = 5f;
    [SerializeField] private float skillRange = 5f;
    [SerializeField] private int skillDamage = 40;
    [SerializeField] private int skillManaCost = 20;
    
    [Header("انیمیشن")]
    [SerializeField] private Animator animator;
    
    private float lastAttackTime;
    private float lastSkillTime;
    private PlayerStats playerStats;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (playerStats == null)
            playerStats = GetComponent<PlayerStats>();
    }

    /// <summary>
    /// حمله معمولی
    /// </summary>
    public void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return;
        
        lastAttackTime = Time.time;
        
        // پخش انیمیشن حمله
        animator.SetTrigger("Attack");
        
        // بررسی دشمنان در محدوده
        DamageEnemiesInRange(attackRange, attackDamage);
        
        Debug.Log($"⚔️ دان حمله کرد! آسیب: {attackDamage}");
    }

    /// <summary>
    /// استفاده از مهارت
    /// </summary>
    public void UseSkill()
    {
        if (Time.time - lastSkillTime < skillCooldown)
            return;
        
        if (!playerStats.UseMana(skillManaCost))
        {
            Debug.Log("❌ مانا کافی نیست!");
            return;
        }
        
        lastSkillTime = Time.time;
        
        // پخش انیمیشن مهارت
        animator.SetTrigger("Skill");
        
        // بررسی دشمنان در محدوده
        DamageEnemiesInRange(skillRange, skillDamage);
        
        Debug.Log($"✨ دان از مهارت استفاده کرد! آسیب: {skillDamage}");
    }

    /// <summary>
    /// آسیب دادن به دشمنان در محدوده
    /// </summary>
    private void DamageEnemiesInRange(float range, int damage)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        
        foreach (Collider hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    /// <summary>
    /// دریافت آسیب از دشمن
    /// </summary>
    public void TakeDamage(int damage)
    {
        playerStats.TakeDamage(damage);
    }

    // Getters
    public float AttackCooldown => attackCooldown;
    public float SkillCooldown => skillCooldown;
    public float LastAttackTime => lastAttackTime;
    public float LastSkillTime => lastSkillTime;
}
