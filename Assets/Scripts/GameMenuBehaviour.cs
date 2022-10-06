using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsMenuVisible = false;
    public GameObject Menu;
    void Start()
    {
        Menu.SetActive(IsMenuVisible);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            IsMenuVisible = !IsMenuVisible;
            if (Mathf.Approximately(Time.timeScale, 0))
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
            Menu.SetActive(IsMenuVisible);
        }
    }

    public void ResumeGame()
    {
        IsMenuVisible = false;
        Menu.SetActive(IsMenuVisible);
        Time.timeScale = 1;
        Debug.Log("Resume");
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
