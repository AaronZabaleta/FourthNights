using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject panelEleccion;
    public AudioSource musicSource;


    private void Start()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

 public void EmpezarNivel(string nombreNivel)
{
    if (musicSource != null)
    {
        musicSource.Stop();
    }

    SceneManager.LoadScene(nombreNivel);
}

    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void MostrarEleccion()
    {
        panelEleccion.SetActive(true);
    }

    public void OcultarEleccion()
    {
        panelEleccion.SetActive(false);
    }
}


