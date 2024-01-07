using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PantallaPuntuaciones : MonoBehaviour
{
    public TMP_Text textoPuntuaciones;

    void Start()
    {
        // Solo muestra las puntuaciones si el jugador ha realizado los 5 intentos
        if (GameManager.instance != null && GameManager.instance.IntentosRealizados >= GameManager.instance.MaxIntentos)
        {
            MostrarPuntuaciones();
        }
    }

    void MostrarPuntuaciones()
    {
        // Recupera las puntuaciones almacenadas y los nombres de los jugadores desde PlayerPrefs
        // y los muestra en orden de mayor a menor
        for (int i = 0; i < 5; i++)
        {
            string nombreJugador = PlayerPrefs.GetString("NombreJugador_" + i, "Jugador");
            int puntuacion = PlayerPrefs.GetInt("Puntuacion_" + i, 0);

            textoPuntuaciones.text += $"{nombreJugador}: {puntuacion}\n";
        }
    }
}
