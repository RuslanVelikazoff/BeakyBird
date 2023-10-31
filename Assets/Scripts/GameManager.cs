using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField]
    TMP_Text _scoreText, _endScoreText, _highScoreText;

    private int score,highScore;
    private bool hasGameFinished;
    private const string HIGHSCORE = "HIGHSCORE";


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        startPanel.SetActive(true);
        helpPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        player.SetActive(false);

        score = 0;
        hasGameFinished = false;
    }

    public void StartHelp()
    {
        startPanel.SetActive(false);
        helpPanel.SetActive(true);
    }

    public void UpdateScore(int v)
    {
        score += v;
        _scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        hasGameFinished = true;

        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var item in obstacles)
        {
            item.gameObject.SetActive(false);
        }

        highScore = PlayerPrefs.HasKey(HIGHSCORE) ? PlayerPrefs.GetInt(HIGHSCORE) : 0;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HIGHSCORE, highScore);
        }
        _highScoreText.text = "HIGHSCORE " + highScore.ToString();
        _endScoreText.text = "SCORE " + score.ToString();

        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void StartGame()
    {
        helpPanel.SetActive(false);
        gamePanel.SetActive(true);
        player.SetActive(true);

        StartCoroutine(Spawner());

    }

    public void GameRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    IEnumerator Spawner()
    {
        while(!hasGameFinished)
        {
            Instantiate(obstaclePrefab, Vector3.zero, obstaclePrefab.transform.rotation);
            yield return new WaitForSeconds(2f);
        }
    }
}
