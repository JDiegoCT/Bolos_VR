using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NombreJugador : MonoBehaviour
{
    public static string nombreActual = "Jugador"; // Valor predeterminado si no se proporciona ningún nombre
    public TMP_InputField inputField;
   
    void Start()
    {
        // Asegúrate de que el nombre se inicialice correctamente
        nombreActual = PlayerPrefs.GetString("NombreJugador", "Jugador");
        inputField.text = nombreActual;
    }

    public void GuardarNombre()
    {
        nombreActual = inputField.text;
        PlayerPrefs.SetString("NombreJugador", nombreActual);
        Debug.Log("Nombre guardado: " + nombreActual);
    }
}
