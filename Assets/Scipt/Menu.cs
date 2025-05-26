using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject panelEleccion;  

    public void EmpezarNivel(string nombreNivel)
    {
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

