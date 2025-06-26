using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;

    public float PaddleSpeed = 5.0f;
    public float InitBallForce = 5.0f;
    public float PaddleInfluence = 0.4f;
    public float BallSpeedIncrement = 1.1f;
    public float _minYVelocity = 2.1f;

    [SerializeField] private int _pointsToVictory;
    [SerializeField] private TMP_Text _scoreGUI;
    [SerializeField] private GameObject _paddle;
    
    private int _score;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            UpdateScoreUI();
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        ResetGame();
    }

    public void ScorePoint()
    {
        Score++;
        CheckWinner();
    }

    void CheckWinner()
    {
        if (_score >= _pointsToVictory)
        {
            ResetGame();
        }
    }

    public void UpdateScoreUI()
    {
        if (_scoreGUI != null)
            _scoreGUI.text = _score.ToString();
    }
    public void ResetGame()
    {
        Score = 0;
        ResetPositions();
    }
    
    public void ResetPositions()
    {
        if (_paddle != null)
            _paddle.transform.position = new Vector3(0.0f, -4.3f, 0.0f);
    }
} 
