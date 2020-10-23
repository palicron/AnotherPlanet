using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCAnvas : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject NextLevel;

    public GameObject Bosslife;
    public GameObject endgame;
    public GameObject restar;

    public GameObject Playerlife;
    public bool pause = false;

    private Scrollbar bScb;
    private Scrollbar pScb;

    // Start is called before the first frame update
    void Start()
    {
        bScb = Bosslife.GetComponent<Scrollbar>();
        pScb = Playerlife.GetComponent<Scrollbar>();
        bScb.size = 1;
        pScb.size = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        pause = !pause;

        if (pause)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);

        }
    }

    public void NextlvlMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        NextLevel.SetActive(true);
        Time.timeScale = 0;

    }
    public void endGamemenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        endgame.SetActive(true);
        Time.timeScale = 0;

    }
    public void RestarMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        restar.SetActive(true);
        Time.timeScale = 0;

    }
    public void retunrTomen()
    {
        GameManager.Instance.Returntomen();
    }
    public void restarlevle()
    {
        GameManager.Instance.restarLevel();
    }

    public void nextlvl()
    {
        GameManager.Instance.loadNextLevel();
    }

    public void ActiveBossLife()
    {
        Bosslife.SetActive(true);
    }
    public void DesactiveBossLife()
    {
        Bosslife.SetActive(false);
    }
    public void setBosslife(float por)
    {
        bScb.size = por;
    }
    public void setPlayerlife(float por)
    {
        pScb.size = por;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
