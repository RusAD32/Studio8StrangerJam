using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject thislol;
    public GameObject settings;

    // Start is called before the first frame update
    void Start()
    {
        thislol.SetActive(true);
    }

    // Update is called once per frame
    public void Play()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Settigns()
    {
        thislol.SetActive(false);
        settings.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
