using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PhysicsPlayer player;
    private bool PlayerAlive;

    public GameObject QuitDialog;
    private bool ActiveDialog = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PhysicsPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameStatus();
    }

    private void CheckGameStatus()
    {
        PlayerAlive = player.IsAlive();
        if (player.transform.position.y < 0 || !PlayerAlive)
        {
            PauseGame();
            QuitDialog.SetActive(true);
            ActiveDialog = true;
        }
        if(ActiveDialog && Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("biraj_newscene");
            ContinueGame();
            QuitDialog.SetActive(false);
            ActiveDialog = false;

        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
    }
}
