using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SneneManager : MonoBehaviour
{
    [SerializeField] private Button _gameButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private TMP_Text _scoreText;

    private const string GAME_SCENE = "Game";
    private const string START_SCENE = "Start";
    
    private void Awake()
    {
        if (_gameButton != null)
        {
            _gameButton.onClick.AddListener(LoadGameScene);
        }

        if (_menuButton != null)
        {
            _menuButton.onClick.AddListener(LoadStartScene);
        }

        if (_scoreText != null)
        {
            ScoreManager scoreManager = GameManager.Instance.ScoreManager;
            _scoreText.text = scoreManager.TotalScore.ToString();
            
            scoreManager.Clear();
        }
    }
    
    private void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    
    private void LoadGameScene() => LoadSceneByName(GAME_SCENE); 
    private void LoadStartScene() => LoadSceneByName(START_SCENE);
}
