using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PhysicsPlayer player;
    private bool PlayerAlive;

    public GameObject curCameraPos;
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
        PlayerAlive = player != null;
        if (!PlayerAlive)
        {
            PauseGame();
            QuitDialog.transform.position = curCameraPos.transform.position;
            QuitDialog.SetActive(true);
            ActiveDialog = true;
        }
        else if (player.transform.position.y < 0)
        {
            PauseGame();
            QuitDialog.transform.position = curCameraPos.transform.position;
            QuitDialog.SetActive(true);
            ActiveDialog = true;
        }
        if(ActiveDialog && Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Thorat_Level_1");
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
