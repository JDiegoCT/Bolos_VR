using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoControl : MonoBehaviour
{
    public GameObject sonidoSelec;
    public GameObject sonidoClick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BotonSonSelec()
    {
        Instantiate(sonidoSelec);
    }

    public void BotonSonClick()
    {
        Instantiate(sonidoClick);
    }
}
