using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{


    public GameObject ObjetoMenuPausa;
    public bool pausa = false;
    public bool won = false;
    public bool dead = false;
    public GameObject menuSalir;
    public GameObject win;
    public GameObject death;
    private Player player;
    void Start()
    {

        player = GameObject.Find("Player").GetComponent<Player>();

    }

    
    void Update()
    {
        
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (pausa == false)
            {
                ObjetoMenuPausa.SetActive(true);
                pausa = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
                
                for(int i = 0; i < sonidos.Length; i++)
                {

                    sonidos[i].Pause();

                }
                
            }
            else if (pausa == true)
            {

                Resumir();

            }
                

        }
        if (player.lifeState <= 0)
        {


            if (dead == false)
            {
                death.SetActive(true);
                dead = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                AudioSource[] sonidos = FindObjectsOfType<AudioSource>();

                for (int i = 0; i < sonidos.Length; i++)
                {

                    sonidos[i].Pause();

                }

            }

        }

    }

   public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        menuSalir.SetActive(false);
        pausa = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        AudioSource[] sonidos = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < sonidos.Length; i++)
        {

            sonidos[i].Play();

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            
                if (won == false)
                {
                    win.SetActive(true);
                    won = true;

                    Time.timeScale = 0;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    AudioSource[] sonidos = FindObjectsOfType<AudioSource>();

                    for (int i = 0; i < sonidos.Length; i++)
                    {

                        sonidos[i].Pause();

                    }

                }



            
        }


    }
    public void IrAlMenu(string nombreMenu)
    {

        SceneManager.LoadScene(nombreMenu);

    }

    public void RestartLevel(string nombreLevel)
    {

        SceneManager.LoadScene(nombreLevel);

    }

    public void SalirDelJuego()
    {

        Application.Quit();

    }



}
