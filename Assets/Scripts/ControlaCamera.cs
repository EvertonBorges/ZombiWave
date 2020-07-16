using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour
{

    public GameObject jogador;

    Vector3 distCompensar;
    
    void Start()
    {
        distCompensar = transform.position - jogador.transform.position;
    }

    void Update()
    {
        transform.position = jogador.transform.position + distCompensar;
    }

}