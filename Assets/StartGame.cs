
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartGame : MonoBehaviour
{
    public GameObject MenuSound;

    //Text Management
    public GameObject Credits;
    public GameObject CreditsPara;

    public GameObject Mute;
    public GameObject UnMute;


    public GameObject HTP;
    public GameObject HTPPara;






    void Start()
    {
        MenuSound.SetActive(true);
    }

    public void StartG()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        MenuSound.SetActive(false);

    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("WelcomeScreen"); //Load scene called Game

    }


    //Text Management
    public void Options()
    {
        Credits.SetActive(true);
        Mute.SetActive(true);
        HTP.SetActive(true);

    }

    public void HowTP()
    {
        CreditsPara.SetActive(false);
        HTPPara.SetActive(true);
       

    }
    public void Credentials()
    {
        HTPPara.SetActive(false);
        CreditsPara.SetActive(true);
    }

    public void MuteGame()
    {
        AudioListener.pause = true;
        Mute.SetActive(false);
        UnMute.SetActive(true);
    }

    public void UnMuteGame()
    {
        AudioListener.pause = false;
        UnMute.SetActive(false);
        Mute.SetActive(true);
    }


}
