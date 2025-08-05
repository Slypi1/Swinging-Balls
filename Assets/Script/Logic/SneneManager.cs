using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SneneManager : MonoBehaviour
{
    [SerializeField] private Button _gameButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private TMP_Text _scoreText;

    private void Awake()
    {
        if (_gameButton != null)
        {
            _gameButton.onClick.AddListener(() => LoadSceneByName("Game"));
        }

        if (_menuButton != null)
        {
            _menuButton.onClick.AddListener(()=> LoadSceneByName("Start"));
        }

        if (_scoreText != null)
        {
            ScoreManager scoreManager = GameManager.Instance.ScoreManager;
            _scoreText.text = scoreManager.TotalScore.ToString();
            
            scoreManager.Clear();
        }
    }
    
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
}
