using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float fuerzaLanzamiento = 25f;


    private void Start()
    {
        GazeManager.Instance.OnGazeSelection += LanzarBola;
    }

    void OnDestroy()
    {
        // Desuscribir el m�todo al destruir la bola (importante para evitar fugas de memoria)
        GazeManager.Instance.OnGazeSelection -= LanzarBola;
    }

    void LanzarBola()
    {
        // Lanzar la bola en la direcci�n en la que est� mirando la c�mara
        Camera cam = Camera.main;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(cam.transform.forward * fuerzaLanzamiento, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pino"))
        {
           

            // Si prefieres reiniciar la escena, puedes hacerlo llamando al GameManager
            GameManager.instance.FinalizarJuego();
        }
    }
}

