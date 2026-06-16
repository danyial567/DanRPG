using UnityEngine;

/// <summary>
/// آمار و اطلاعات شخصیت - دان
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [Header("آمار اولیه")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxMana = 50;
    [SerializeField] private int attack = 15;
    [SerializeField] private int defense = 10;
    
    [Header("تجربه و سطح")]
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int currentExperience = 0;
    [SerializeField] private int experiencePerLevel = 100;
    
    private int currentHealth;
    private int currentMana;
    private int totalCoins = 0;
    
    private static PlayerStats instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    /// <summary>
    /// دریافت آسیب
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        
        Debug.Log($"دان آسیب دید: {damage} | سلامتی باقی: {currentHealth}");
        
        if (currentHealth <= 0)
            Die();
    }

    /// <summary>
    /// بهبود سلامتی
    /// </summary>
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        Debug.Log($"دان درمان شد: {amount} | سلامتی: {currentHealth}");
    }

    /// <summary>
    /// استفاده از مانا
    /// </summary>
    public bool UseMana(int amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            return true;
        }
        return false;
    }

    /// <summary>
    /// بهبود مانا
    /// </summary>
    public void RestoreMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana) currentMana = maxMana;
    }

    /// <summary>
    /// اضافه کردن تجربه
    /// </summary>
    public void AddExperience(int amount)
    {
        currentExperience += amount;
        Debug.Log($"تجربه دریافت شد: {amount}");
        
        while (currentExperience >= experiencePerLevel)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// بالا رفتن سطح
    /// </summary>
    private void LevelUp()
    {
        currentExperience -= experiencePerLevel;
        currentLevel++;
        
        // افزایش آمار
        maxHealth += 10;
        maxMana += 5;
        attack += 2;
        defense += 1;
        
        currentHealth = maxHealth;
        currentMana = maxMana;
        
        Debug.Log($"🎉 دان به سطح {currentLevel} رسید!");
    }

    /// <summary>
    /// مرگ کاراکتر
    /// </summary>
    public void Die()
    {
        Debug.Log("💀 دان مرد!");
        // بعداً منطق مرگ را اضافه کنید
    }

    /// <summary>
    /// اضافه کردن سکه
    /// </summary>
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log($"سکه دریافت شد: {amount} | کل: {totalCoins}");
    }

    // Getters
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public int CurrentMana => currentMana;
    public int MaxMana => maxMana;
    public int Attack => attack;
    public int Defense => defense;
    public int CurrentLevel => currentLevel;
    public int CurrentExperience => currentExperience;
    public int ExperiencePerLevel => experiencePerLevel;
    public int TotalCoins => totalCoins;
    
    public static PlayerStats Instance => instance;
}
