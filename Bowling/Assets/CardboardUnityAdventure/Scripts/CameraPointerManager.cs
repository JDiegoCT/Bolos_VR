using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointerManager : MonoBehaviour
{
    //intancia el objeto
    public static CameraPointerManager instance;
    [SerializeField] private GameObject pointer;
    [SerializeField] private float maxDistancePointer = 4.5f;
    //esto es para que solo interactue con los tag permitidos
    private readonly string interactableTag = "Interactable";
    private float scaleSize = 0.025f;
    [Range(0f, 1f)]
    [SerializeField] private float distPointerObject = 0.95f;

    private const float _maxDistance = 20;
    private GameObject _gazedAtObject = null;

    [HideInInspector] public Vector3 hitPoint;

    

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>

    private void Start()
    {
        //referencia la seleccion de gazeManager
        GazeManager.Instance.OnGazeSelection += GazeSelection;
    }

    public void GazeSelection()
    {
        _gazedAtObject?.SendMessage("OnPointerClickXR", null, SendMessageOptions.DontRequireReceiver);

    }




    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        //evalua la posicion de la camara hasta donde choca  (raycast de la camara)
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            hitPoint = hit.point;
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.
                //caso contrario = -?.- envia como mensaje  la funcion "OnPointerExit"
                //el SendMesaageOptions... es para manajar una exepcion de errores
                //------------esto es para usar sin botones-------------
                //_gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
                //_gazedAtObject = hit.transform.gameObject;
                //_gazedAtObject.SendMessage("OnPointerEnter", null, SendMessageOptions.DontRequireReceiver);

                _gazedAtObject?.SendMessage("OnPointerExitXR", null, SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnterXR", null, SendMessageOptions.DontRequireReceiver);

                //este codigo es para la carga del gazepointer
                GazeManager.Instance.StartGazeSelection();
            }
            if(hit.transform.CompareTag(interactableTag))
            {
                //
                PointerOnGaze(hit.point);
            }
            else { PointerOutGaze(); }

        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExitXR", null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClickXR", null, SendMessageOptions.DontRequireReceiver);
        }
    }
    private void PointerOutGaze()
    {
        pointer.transform.localScale = Vector3.one * 0.1f;
        pointer.transform.parent.transform.localPosition = new Vector3(0,0,maxDistancePointer);
        pointer.transform.parent.parent.transform.rotation = transform.rotation;
        GazeManager.Instance.CancelGazeSelection();
    }
    private void PointerOnGaze(Vector3 hitPoint)//quiere decir que la variable vector3 se llamara "hitPoint"
    {
        float scaleFactor = scaleSize * Vector3.Distance(transform.position, hitPoint);
        pointer.transform.localScale = Vector3.one * scaleFactor;
        pointer.transform.parent.position = CalculatePointerPosition(transform.position, hitPoint, distPointerObject);

    }
    private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t)
    {
        float x = p0.x + t * (p1.x - p0.x);
        float y = p0.y + t * (p1.y - p0.y);
        float z = p0.z + t * (p1.z - p0.z);
        return new Vector3(x, y, z);
    }
   
}
