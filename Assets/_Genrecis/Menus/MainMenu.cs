using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{

    public GameObject Buttons;
    public GameObject About;

    public GameObject planets;
    public GameObject intruc;

    public Animator anim;

    public Text text;

    public GameObject locker;
    private int cp = -1;
    private bool onNavigate;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (!onNavigate)
            return;


        if (Input.GetKeyDown(KeyCode.A))
        {
            Toplanet(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Toplanet(1);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            loadalevel();
        }



    }


    public void toAbout()
    {
        anim.SetBool("MainMenu", false);
        anim.SetBool("About", true);
        Buttons.SetActive(false);
        StartCoroutine(closeMain());
    }
    public void loadalevel()
    {
        if (GameManager.Instance.GetLvlLock(cp))
        {
            GameManager.Instance.loadGame(cp);
        }
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        cp = 0;
        anim.SetBool("MainMenu", true);
        anim.SetBool("About", false);
        anim.SetBool("Select", false);
        anim.SetInteger("planet", cp);
        anim.SetBool("Intruction", false);
        intruc.SetActive(false);
        planets.SetActive(false);
        About.SetActive(false);
        onNavigate = false;
        StartCoroutine(openMain());

    }
    public void instruc()
    {
        anim.SetBool("MainMenu", false);
        anim.SetBool("Intruction", true);
        Buttons.SetActive(false);
        intruc.SetActive(true);
    }
    public void Toplanet(int index)
    {

        cp = GameManager.Instance.getNextplanetIndex(index, cp);
        anim.SetBool("MainMenu", false);
        anim.SetBool("About", false);
        anim.SetBool("Select", true);
        updateTitle();
        locker.SetActive(!GameManager.Instance.GetLvlLock(cp));
        anim.SetInteger("planet", cp);
        onNavigate = true;

    }
    public void TofirtPLanet()
    {
        cp = 0;
        anim.SetBool("MainMenu", false);
        anim.SetBool("About", false);
        anim.SetBool("Select", true);
        updateTitle();
        anim.SetInteger("planet", cp);
        onNavigate = true;
        Buttons.SetActive(false);
        StartCoroutine(openPlanetDes());
    }

    private void updateTitle()
    {
        text.text = GameManager.Instance.GetLvlDescription(cp);
    }
    IEnumerator closeMain()
    {
        yield return new WaitForSeconds(0.2f);
        About.SetActive(true);
    }
    IEnumerator openMain()
    {
        yield return new WaitForSeconds(0.2f);
        Buttons.SetActive(true);
    }
    IEnumerator openPlanetDes()
    {
        yield return new WaitForSeconds(0.2f);
        planets.SetActive(true);
    }
}
