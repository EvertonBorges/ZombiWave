using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int vidaInicial = 100;
    public float velocidade = 5;

    private int _vida;

    void Start()
    {
        _vida = vidaInicial;
    }

    public int GetVida()
    {
        return _vida;
    }

    public void SetVida(int dano)
    {
        _vida -= dano;
    }

}
