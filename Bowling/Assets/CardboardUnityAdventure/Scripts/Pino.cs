using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pino : MonoBehaviour
{
    public int puntosPorBolo = 10;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.instance.AgregarPuntos(puntosPorBolo);
            GameManager.instance.FinalizarJuego();
        }
    }

   /** void ReiniciarEscenaConDelay()
    {
        GameManager.instance.ReiniciarEscenaDespuesDeDelay(0f);
    }**/
}
