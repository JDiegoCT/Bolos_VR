using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuntuacionesManager : MonoBehaviour
{
    public TMP_Text textoPuntuaciones;
    void Start()
    {
        MostrarPuntuaciones();
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
