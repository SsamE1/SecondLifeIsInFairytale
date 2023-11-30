using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionWindow;
    public void NewStartBtn()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("CurrentChapter", 1);
        PlayerPrefs.SetInt("RollPaper", 0);
        SceneManager.LoadScene("����");
    }

    public void ContinueBtn()
    {
        switch (PlayerPrefs.GetInt("CurrentChapter"))
        {
            case 1:
                SceneManager.LoadScene("1�����");
                break;
            case 2:
                SceneManager.LoadScene("2�����");
                break;
            case 3:
                SceneManager.LoadScene("3�����");
                break;
            default:
                SceneManager.LoadScene("1�����");
                break;
        }
        
    }

    public void OptionBtn()
    {
        OptionWindowOpen();
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void OptionWindowOpen()
    {
        optionWindow.SetActive(true);
    }

    public void OptionWindowClose()
    {
        optionWindow.SetActive(false);
    }
}
