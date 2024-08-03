using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeCollectionManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI collectedText;
    public int totalCubes = 5;
    public float timeLimit = 120f; // 2 minutes in seconds

    private int cubesCollected = 0;
    private float timer;
    private bool gameOver = false;

    void Start()
    {
        timer = timeLimit;
        UpdateUI();
    }

    void Update()
    {
        if (gameOver) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            GameOver(false);
        }

        UpdateUI();
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.TryGetComponent(out Cube cube))
        {
            cubesCollected++;
            Destroy(other.gameObject);

            if (cubesCollected >= totalCubes)
            {
                GameOver(true);
            }
        }
    }
   

    void UpdateUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
        collectedText.text = "Cubes: " + cubesCollected.ToString() + "/" + totalCubes.ToString();
    }

    void GameOver(bool won)
    {
        gameOver = true;
        if (won)
        {
            timerText.text = "You Win!";
        }
        else
        {
            timerText.text = "Time's Up!";
        }

       
        Invoke("ReloadScene", 3f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
