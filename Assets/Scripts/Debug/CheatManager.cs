using UnityEngine;

/// <summary>
/// سیستم کد‌های تقلب برای تست و توسعه
/// فعال‌سازی: فشار دادن دکمه‌های ترکیبی
/// </summary>
public class CheatManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private PlayerCombat playerCombat;
    private PlayerController playerController;
    
    private bool cheatsEnabled = false;
    private string cheatInput = "";
    
    private static CheatManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerStats = PlayerStats.Instance;
        playerCombat = FindObjectOfType<PlayerCombat>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        CheckCheatCodes();
    }

    /// <summary>
    /// بررسی کد‌های تقلب
    /// </summary>
    private void CheckCheatCodes()
    {
        // فعال‌سازی منوی تقلب: Ctrl + Shift + C
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C))
        {
            cheatsEnabled = !cheatsEnabled;
            Debug.Log(cheatsEnabled ? "✓ کد‌های تقلب فعال شدند!" : "✗ کد‌های تقلب غیرفعال شدند!");
        }

        if (!cheatsEnabled) return;

        // کد‌های تقلب مختلف
        if (Input.GetKeyDown(KeyCode.H))
            FullHeal();

        if (Input.GetKeyDown(KeyCode.G))
            AddGold();

        if (Input.GetKeyDown(KeyCode.L))
            LevelUp();

        if (Input.GetKeyDown(KeyCode.E))
            AddExperience();

        if (Input.GetKeyDown(KeyCode.M))
            FullMana();

        if (Input.GetKeyDown(KeyCode.K))
            KillAllEnemies();

        if (Input.GetKeyDown(KeyCode.S))
            IncreaseSpeeed();

        if (Input.GetKeyDown(KeyCode.T))
            TestMode();

        if (Input.GetKeyDown(KeyCode.D))
            ToggleDamageMode();
    }

    /// <summary>
    /// تقلب: درمان کامل (H)
    /// </summary>
    private void FullHeal()
    {
        if (playerStats == null) return;
        
        playerStats.Heal(playerStats.MaxHealth);
        Debug.Log("💚 دان کاملاً درمان شد!");
    }

    /// <summary>
    /// تقلب: اضافه کردن سکه (G)
    /// </summary>
    private void AddGold()
    {
        if (playerStats == null) return;
        
        playerStats.AddCoins(1000);
        Debug.Log("💰 1000 سکه اضافه شد!");
    }

    /// <summary>
    /// تقلب: بالا رفتن سطح (L)
    /// </summary>
    private void LevelUp()
    {
        if (playerStats == null) return;
        
        playerStats.AddExperience(playerStats.ExperiencePerLevel);
        Debug.Log($"⬆️ دان به سطح {playerStats.CurrentLevel} رسید!");
    }

    /// <summary>
    /// تقلب: اضافه کردن تجربه (E)
    /// </summary>
    private void AddExperience()
    {
        if (playerStats == null) return;
        
        playerStats.AddExperience(500);
        Debug.Log("⭐ 500 تجربه اضافه شد!");
    }

    /// <summary>
    /// تقلب: مانا کامل (M)
    /// </summary>
    private void FullMana()
    {
        if (playerStats == null) return;
        
        playerStats.RestoreMana(playerStats.MaxMana);
        Debug.Log("💙 مانا کاملاً پر شد!");
    }

    /// <summary>
    /// تقلب: کشتن همه دشمنان (K)
    /// </summary>
    private void KillAllEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        
        foreach (Enemy enemy in enemies)
        {
            if (enemy.IsAlive)
            {
                enemy.TakeDamage(9999);
            }
        }
        
        Debug.Log($"💀 {enemies.Length} دشمن کشته شدند!");
    }

    /// <summary>
    /// تقلب: افزایش سرعت حرکت (S)
    /// </summary>
    private void IncreaseSpeeed()
    {
        if (playerController == null) return;
        
        playerController.SetMoveSpeed(15f);
        Debug.Log("⚡ سرعت کاراکتر افزایش یافت!");
    }

    /// <summary>
    /// تقلب: حالت تست (T)
    /// </summary>
    private void TestMode()
    {
        Debug.Log("🧪 حالت تست فعال شد!");
        Debug.Log("آمار کاراکتر:");
        Debug.Log($"  سطح: {playerStats.CurrentLevel}");
        Debug.Log($"  سلامتی: {playerStats.CurrentHealth}/{playerStats.MaxHealth}");
        Debug.Log($"  مانا: {playerStats.CurrentMana}/{playerStats.MaxMana}");
        Debug.Log($"  حمله: {playerStats.Attack}");
        Debug.Log($"  دفاع: {playerStats.Defense}");
        Debug.Log($"  سکه: {playerStats.TotalCoins}");
    }

    /// <summary>
    /// تقلب: حالت بی‌آسیب (D)
    /// </summary>
    private void ToggleDamageMode()
    {
        Debug.Log("🛡️ حالت بی‌آسیب تغییر یافت!");
        // بعداً می‌تونید سیستم حفاظت را پیاده‌سازی کنید
    }

    /// <summary>
    /// نمایش تمام کد‌های تقلب
    /// </summary>
    public static void ShowCheatCodes()
    {
        Debug.Log("="*50);
        Debug.Log("🎮 کد‌های تقلب DanRPG:");
        Debug.Log("="*50);
        Debug.Log("Ctrl + Shift + C : فعال‌سازی/غیرفعال‌سازی تقلب");
        Debug.Log("");
        Debug.Log("H : درمان کامل");
        Debug.Log("G : اضافه کردن 1000 سکه");
        Debug.Log("L : بالا رفتن سطح");
        Debug.Log("E : اضافه کردن 500 تجربه");
        Debug.Log("M : مانا کامل");
        Debug.Log("K : کشتن همه دشمنان");
        Debug.Log("S : افزایش سرعت");
        Debug.Log("T : نمایش آمار");
        Debug.Log("D : حالت بی‌آسیب");
        Debug.Log("="*50);
    }

    public static CheatManager Instance => instance;
    public bool CheatsEnabled => cheatsEnabled;
}
