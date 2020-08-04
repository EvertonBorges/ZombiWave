using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField]
    private int vidaInicial = 100;
    [SerializeField]
    private float velocidade = 5;

    private int _vida;

    void Awake()
    {
        _vida = vidaInicial;
    }

    public int GetVida()
    {
        return _vida;
    }

    public void TirarVida(int dano)
    {
        _vida -= dano;
    }

    public void CurarVida(int vida)
    {
        _vida += vida;
        if (_vida >= vidaInicial)
        {
            _vida = vidaInicial;
        }
    }

    public float GetVelocidade()
    {
        return velocidade;
    }

}
