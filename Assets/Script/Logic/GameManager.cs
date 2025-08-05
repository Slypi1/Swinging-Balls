using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ScoreManager ScoreManager => _scoreManager;
    public static GameManager Instance;
    
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private ZoneManager _zoneManager;
   
    private ScoreManager _scoreManager;
    private InputSystem _inputSystem;
    

    public void LoadScene()
    {
        SceneManager.LoadScene("GameOver");
    }
    
    private void Start()
    {
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
        
        _inputSystem = new InputSystem();
        _scoreManager = new ScoreManager();
        
        _ballSpawner.Initialize();
        _zoneManager.Initialize(this);
    }

    private void Update()
    {
        if (_inputSystem.MouseButtonDown && _ballSpawner != null)
        {
            _ballSpawner.CurrentBall.Drop();
        }
    }
}

