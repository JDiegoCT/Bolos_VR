using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject bolo;

    public GameObject[] pinos;
    public Vector3 posicionInicialBolo; // Guarda las posiciones iniciales de los bolos
    public Vector3[] posicionesInicialesPinos; // Guarda las posiciones iniciales de los pinos
    

    
    public TextMeshProUGUI textoPuntos; // Asigna un objeto Text en el Inspector para mostrar los puntos
    private int puntuacionActual = 0;
    private string nombreJugadorActual = "Jugador";


    private int intentosRealizados = 0;
    public int IntentosRealizados => intentosRealizados;

    private  int maxIntentos = 2;
    public int MaxIntentos => maxIntentos;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
           // Destroy(gameObject);
        }
    }

    void Start()
    {
        nombreJugadorActual = "Jugador";

        puntuacionActual = PlayerPrefs.GetInt("PuntuacionActual", 0);
        nombreJugadorActual = PlayerPrefs.GetString("NombreJugadorActual", "Jugador");
    }
    void GuardarPosicionesInicialesPinos()
    {
        posicionesInicialesPinos = new Vector3[pinos.Length];
        for (int i = 0; i < pinos.Length; i++)
        {
            posicionesInicialesPinos[i] = pinos[i].transform.position;
        }
    }

    


    public void AgregarPuntos(int cantidad)
    {
        puntuacionActual += cantidad;
        textoPuntos.text = "Puntos: " + puntuacionActual.ToString();
    }

    public void FinalizarJuego()
    {
        intentosRealizados++;

        if (intentosRealizados >= maxIntentos)
        {
            // Guarda la puntuación actual y el nombre del jugador actual en PlayerPrefs
            PlayerPrefs.SetInt("PuntuacionActual", puntuacionActual);
            PlayerPrefs.SetString("NombreJugadorActual", nombreJugadorActual);

            // Guarda la puntuación actual y el nombre del jugador actual en las listas
            AlmacenarPuntuacion(puntuacionActual, nombreJugadorActual);

            // Reinicia la escena después de 3 segundos
            ReiniciarEscenaDespuesDeDelay(3f);

            // También reinicia el contador de intentos
            intentosRealizados = 0;

            // Restablece el nombre del jugador al finalizar el juego
            nombreJugadorActual = "Jugador";
        }

    }

    void AlmacenarPuntuacion(int nuevaPuntuacion, string nombreJugador)
    {
        // Itera sobre las puntuaciones almacenadas y las compara con la nueva puntuación
        // para insertar la nueva puntuación en la posición correcta
        for (int i = 0; i < 5; i++)
        {
            int puntuacionAlmacenada = PlayerPrefs.GetInt("Puntuacion_" + i, 0);

            if (nuevaPuntuacion > puntuacionAlmacenada)
            {
                int puntuacionAnterior = puntuacionAlmacenada;
                string nombreAnterior = PlayerPrefs.GetString("NombreJugador_" + i, "Jugador");

                PlayerPrefs.SetInt("Puntuacion_" + i, nuevaPuntuacion);
                PlayerPrefs.SetString("NombreJugador_" + i, nombreJugador);

                nuevaPuntuacion = puntuacionAnterior;
                nombreJugador = nombreAnterior;
            }
        }
    }

    public void ReiniciarEscenaDespuesDeDelay(float delay)
    {
        Invoke("ReiniciarEscena", delay);
    }

    void ReiniciarEscena()
    {
        Debug.Log($"Puntuación antes de reiniciar: {puntuacionActual}");

        // Reposiciona los bolos y los pinos en sus posiciones iniciales
        ReposicionarBolo();
        ReposicionarPinos();

        // Guarda la puntuación actual antes de reiniciar
        PlayerPrefs.SetInt("PuntuacionActual", puntuacionActual);
        PlayerPrefs.SetString("NombreJugadorActual", nombreJugadorActual);

        // Reinicia la escena después de 3 segundos
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ReposicionarBolo()
    {
        // Verifica si el objeto bolo no ha sido destruido antes de intentar acceder a él
        if (bolo != null)
        {
            bolo.transform.position = posicionInicialBolo;
        }
        else
        {
            Debug.LogWarning("Intentando acceder a un bolo que ha sido destruido.");
            bolo=Instantiate(bolo, posicionInicialBolo, Quaternion.identity);
            // Si el bolo ha sido destruido, puedes manejar esto según tus necesidades.
        }
    }

    void ReposicionarPinos()
    {
        for (int i = 0; i < pinos.Length; i++)
        {
            if (pinos[i] != null)
            {
                pinos[i].transform.position = posicionesInicialesPinos[i];
            }
            else
            {
                Debug.LogWarning("Intentando acceder a un pino que ha sido destruido.");
            }
        }
    }

    
}

