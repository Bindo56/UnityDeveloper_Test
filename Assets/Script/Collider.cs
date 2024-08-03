using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collider : MonoBehaviour
{
   
    [SerializeField] Transform gameOverPanel;
    [SerializeField] Transform Player;
    [SerializeField] Transform playerResetPoint;

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.transform.TryGetComponent(out Player player))
        {
            Debug.Log("PlayerTouch");
            gameOverPanel.gameObject.SetActive(true);
            Player.position = playerResetPoint.position + new Vector3(0,0,0);
            Physics.gravity = Vector3.down;
        }
    }
    

public void Restart()
    {
        gameOverPanel.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
