using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager intance;
    public static GameManager Instance { get { if (intance == null) { intance = new GameManager(); } return intance; } }
    private bool pause = false;


    [SerializeField]
    private LevelList Levels;


    private int CurrentLevel = -1;
    // Start is called before the first frame update
    private void Awake()
    {
        intance = this;
        Cursor.lockState = CursorLockMode.Confined;
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(loadgame(Levels.Menu));
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


    public int getNextplanetIndex(int index, int cp)
    {
        if (cp + index < Levels.levels.Length && cp + index >= 0)
        {
            return cp + index;
        }
        else if (cp + index >= Levels.levels.Length && cp + index >= 0)
        {
            return 0;
        }
        else
        {
            return Levels.levels.Length - 1;
        }
    }
    public void loadGame(int index)
    {
        CurrentLevel = index;
        StartCoroutine(loadgame(Levels.levels[CurrentLevel]));

    }
    public void loadNextLevel()
    {
        if (CurrentLevel + 1 < Levels.levels.Length)
        {
            CurrentLevel += 1;

            StartCoroutine(loadgame(Levels.levels[CurrentLevel]));
        }
        else
        {
            CurrentLevel = 0;
            StartCoroutine(loadgame(Levels.levels[CurrentLevel]));
        }


    }

    public void UnlockNextLevel()
    {
        if (CurrentLevel + 1 <= Levels.levels.Length)
        {
            Levels.opens[CurrentLevel + 1] = true;

        }
    }

    public bool GetLvlLock(int i)
    {
        return Levels.opens[i];
    }
    public string GetLvlDescription(int i)
    {
        return Levels.Descripcion[i];
    }

    public void restarLevel()
    {
        StartCoroutine(loadgame(Levels.levels[CurrentLevel]));
    }

    public void Returntomen()
    {
        StartCoroutine(loadgame(Levels.Menu));
    }


    IEnumerator loadgame(string levelname)
    {
        bool cargo = false;
        while (!cargo)
        {
            yield return SceneManager.LoadSceneAsync(levelname);
            cargo = true;
            Time.timeScale = 1;
        }



    }
}
