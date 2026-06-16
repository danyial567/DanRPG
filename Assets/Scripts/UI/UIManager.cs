using UnityEngine;
using TMPro;

/// <summary>
/// مدیریت رابط کاربری
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("سلامتی و مانا")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;
    
    [Header("سطح و تجربه")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image experienceBar;
    [SerializeField] private TextMeshProUGUI experienceText;
    
    [Header("سکه")]
    [SerializeField] private TextMeshProUGUI coinText;
    
    [Header("منو")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject statsPanel;
    
    private PlayerStats playerStats;
    private bool isPaused = false;
    
    private static UIManager instance;

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
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats نیافت!");
        }
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateManaBar();
        UpdateExperienceBar();
        UpdateCoinDisplay();
        UpdateLevelDisplay();
    }

    /// <summary>
    /// به‌روزرسانی نوار سلامتی
    /// </summary>
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float healthPercent = (float)playerStats.CurrentHealth / playerStats.MaxHealth;
            healthBar.fillAmount = healthPercent;
        }
        
        if (healthText != null)
        {
            healthText.text = $"{playerStats.CurrentHealth}/{playerStats.MaxHealth}";
        }
    }

    /// <summary>
    /// به‌روزرسانی نوار مانا
    /// </summary>
    private void UpdateManaBar()
    {
        if (manaBar != null)
        {
            float manaPercent = (float)playerStats.CurrentMana / playerStats.MaxMana;
            manaBar.fillAmount = manaPercent;
        }
        
        if (manaText != null)
        {
            manaText.text = $"{playerStats.CurrentMana}/{playerStats.MaxMana}";
        }
    }

    /// <summary>
    /// به‌روزرسانی نوار تجربه
    /// </summary>
    private void UpdateExperienceBar()
    {
        if (experienceBar != null)
        {
            float expPercent = (float)playerStats.CurrentExperience / playerStats.ExperiencePerLevel;
            experienceBar.fillAmount = expPercent;
        }
        
        if (experienceText != null)
        {
            experienceText.text = $"{playerStats.CurrentExperience}/{playerStats.ExperiencePerLevel}";
        }
    }

    /// <summary>
    /// به‌روزرسانی نمایش سکه
    /// </summary>
    private void UpdateCoinDisplay()
    {
        if (coinText != null)
        {
            coinText.text = $"💰 {playerStats.TotalCoins}";
        }
    }

    /// <summary>
    /// به‌روزرسانی نمایش سطح
    /// </summary>
    private void UpdateLevelDisplay()
    {
        if (levelText != null)
        {
            levelText.text = $"LVL {playerStats.CurrentLevel}";
        }
    }

    /// <summary>
    /// باز/بستن منوی توقف
    /// </summary>
    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(isPaused);
        }
        
        Time.timeScale = isPaused ? 0f : 1f;
    }

    /// <summary>
    /// نمایش پنل آمار
    /// </summary>
    public void ShowStatsPanel()
    {
        if (statsPanel != null)
        {
            statsPanel.SetActive(true);
        }
    }

    /// <summary>
    /// پنهان کردن پنل آمار
    /// </summary>
    public void HideStatsPanel()
    {
        if (statsPanel != null)
        {
            statsPanel.SetActive(false);
        }
    }

    public static UIManager Instance => instance;
}
