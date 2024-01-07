using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void CambiarEscenario(string escenario) 
    {
        //funcion que permite cambiar de escenario
        //"escenario" es el nombre que recibe como parametro del escenario
        //debe ser igual el texto
        SceneManager.LoadScene(escenario);
    }

    public void Salir()
    {
        Application.Quit();//este comando te permite salir del juego
    }
}
