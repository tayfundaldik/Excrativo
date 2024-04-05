using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Slider volume;
    
    public GameObject titleText;
    public bool isGameActive;
    public bool isGamePaused;
    private int score = 0;
    private int lives = 3;
    private float spawnRate = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }
    IEnumerator SpawnTarget(){
        while(isGameActive){
        yield return new WaitForSeconds(spawnRate);
        int index = Random.Range(0,targets.Count);

        Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.text ="Score: " + score;
    }
    public void Lives(){
        if (isGameActive)
     {
        lives --;
        livesText.text= "Lives: "+ lives;
        if (lives == 0){
            GameOver();
            }
        }
    }

  
    public void GameOver(){
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive=false;
    }
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty){
        spawnRate /= difficulty;
        isGameActive = true;
        lives++;
        Lives();
        StartCoroutine(SpawnTarget());   
        titleText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!isGamePaused && isGameActive == true){
                isGamePaused = true;
                pauseText.gameObject.SetActive(true); 
                Time.timeScale = 0;
            }
            else if(isGamePaused){
                isGamePaused = false; 
                pauseText.gameObject.SetActive(false);
                Time.timeScale = 1;

            }
        }
    }
}
