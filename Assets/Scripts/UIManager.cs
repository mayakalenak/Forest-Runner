using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image score;
    // [SerializeField] private Image live;
    [SerializeField] private Button muteButton;
    [SerializeField] private Button restartButton;


    public bool muted = false;
    public bool isAlive = true;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    private void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateScore;

        UpdateScore(GameManager.Instance.score);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScore;
        }
    }

    private void UpdateScore(int score)
    {
        scoreText.text = " " + score;
    }


    public void onMute()
    {
        SoundManager.Instance?.ToggleMusic();
    }

    // public void UpdateHeartIcon(bool isAlive)
    // {
    //     // heartIcon.enabled = isAlive;
    //     // brokenHeartIcon.enabled = !isAlive;

    //     if (!isAlive)
    //     {
    //         GameManager.Instance.EndGame();
    //     }
    // }

    public void onButtonPress()
    {
        GameManager.Instance.RestartGame();
    }
}