using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//using Gestor = GestionEscenas.SceneManager;
public class GameManager : MonoBehaviour
{

    public GameObject panelInicio;
    public GameObject panelPausa;
    public GameObject panelControl;
    public GameObject mapPanel;
    AudioManager audioManager;

    public bool controlPanelMenuIsActive;


    public bool paused;
    private bool mapIsActive;


    // Start is called before the first frame update
    void Start()
    {
        //string escenaActual = Gestor.GetActiveScene().name;

        // Activar el AudioSource dependiendo de la escena activa
        switch (tag)
        {
            case "MainMenu":
                AudioManager.PlayMainMenuMusic();
                break;


            case "Game":
                AudioManager.PlayBGMMusic();
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();

        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1.0f;
        panelControl.SetActive(false);

    }

   
    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        paused = false;
        panelPausa.SetActive(false);
        Time.timeScale = 1.0f;
        panelControl.SetActive(false);

    }

    public void ReloadScene()
    {
        //Gestor.LoadScene("Game");
        Time.timeScale = 1.0f;

    }



    public void PauseGame()
    {
        

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Confined;
            paused = !paused; // Cambia el estado de pausa

            if (paused)
            {
                panelPausa.SetActive(true);
                Time.timeScale = 0f; // Pausa el juego
            }
            else
            {
                panelPausa.SetActive(false);
                Time.timeScale = 1.0f; // Reanuda el juego
                Cursor.lockState = CursorLockMode.Locked;

            }
        }

    }

    public void ControlMenu()
    {
        if (controlPanelMenuIsActive == false)
        {
            controlPanelMenuIsActive = true;
            panelControl.SetActive(true);
            panelPausa.SetActive(false);
        }
        else
        {
            controlPanelMenuIsActive = false;

        }

    }
    
   
      

    public void ControlMenuOut()
    {
        controlPanelMenuIsActive = false;
        panelControl.SetActive(false);
        panelPausa.SetActive(true);

    }
    
    public void MapOut()
    {
        mapIsActive = false;
        mapPanel.SetActive(false);

    }
}
