using UnityEngine;

/// <summary>
/// ابزار دیباگ برای توسعه‌دهندگان
/// </summary>
public class DebugConsole : MonoBehaviour
{
    private bool showConsole = false;
    private string input = "";
    private Vector2 scrollPosition = Vector2.zero;
    private static DebugConsole instance;

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

    private void Update()
    {
        // باز کردن کنسول: Alt + D
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.D))
        {
            showConsole = !showConsole;
        }

        // خروج از کنسول: Escape
        if (Input.GetKeyDown(KeyCode.Escape) && showConsole)
        {
            showConsole = false;
        }
    }

    private void OnGUI()
    {
        if (!showConsole) return;

        DrawConsole();
    }

    /// <summary>
    /// رسم کنسول
    /// </summary>
    private void DrawConsole()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height * 0.3f), "🐛 کنسول دیباگ");

        GUILayout.BeginArea(new Rect(10, 30, Screen.width - 20, Screen.height * 0.25f));

        GUILayout.Label("دستورات:");
        GUILayout.Label("help - نمایش دستورات");
        GUILayout.Label("stats - نمایش آمار");
        GUILayout.Label("heal - درمان کامل");
        GUILayout.Label("gold [عدد] - اضافه کردن سکه");
        GUILayout.Label("level [عدد] - تنظیم سطح");

        GUILayout.Space(10);

        input = GUILayout.TextField(input, GUILayout.Height(30));

        if (GUILayout.Button("اجرا", GUILayout.Height(30)))
        {
            ExecuteCommand(input);
            input = "";
        }

        GUILayout.EndArea();
    }

    /// <summary>
    /// اجرای دستور
    /// </summary>
    private void ExecuteCommand(string command)
    {
        string[] parts = command.Split(' ');
        string cmd = parts[0].ToLower();

        switch (cmd)
        {
            case "help":
                Debug.Log("دستورات دستیار:");
                Debug.Log("stats - نمایش آمار");
                Debug.Log("heal - درمان کامل");
                Debug.Log("gold [عدد] - اضافه کردن سکه");
                Debug.Log("level [عدد] - تنظیم سطح");
                break;

            case "stats":
                ShowStats();
                break;

            case "heal":
                PlayerStats.Instance.Heal(PlayerStats.Instance.MaxHealth);
                Debug.Log("درمان کامل!");
                break;

            case "gold":
                if (parts.Length > 1 && int.TryParse(parts[1], out int gold))
                {
                    PlayerStats.Instance.AddCoins(gold);
                    Debug.Log($"{gold} سکه اضافه شد!");
                }
                break;

            case "level":
                if (parts.Length > 1 && int.TryParse(parts[1], out int level))
                {
                    Debug.Log($"سطح تنظیم به {level}");
                }
                break;

            default:
                Debug.Log($"دستور نشناخته: {cmd}");
                break;
        }
    }

    /// <summary>
    /// نمایش آمار
    /// </summary>
    private void ShowStats()
    {
        PlayerStats stats = PlayerStats.Instance;
        Debug.Log("=== آمار کاراکتر ===");
        Debug.Log($"سطح: {stats.CurrentLevel}");
        Debug.Log($"سلامتی: {stats.CurrentHealth}/{stats.MaxHealth}");
        Debug.Log($"مانا: {stats.CurrentMana}/{stats.MaxMana}");
        Debug.Log($"حمله: {stats.Attack}");
        Debug.Log($"دفاع: {stats.Defense}");
        Debug.Log($"سکه: {stats.TotalCoins}");
        Debug.Log($"تجربه: {stats.CurrentExperience}/{stats.ExperiencePerLevel}");
    }

    public static DebugConsole Instance => instance;
}
