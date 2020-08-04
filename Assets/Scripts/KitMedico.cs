using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{

    [SerializeField]
    private int quantidadeCura = 15;

    private int _tempoDeDestruicao = 5;

    void Start()
    {
        Destroy(gameObject, _tempoDeDestruicao);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.JOGADOR))
        {
            ControlaJogador scriptJogador = other.GetComponent<ControlaJogador>();
            scriptJogador.CurarVida(quantidadeCura);
            Destroy(gameObject);
        }
    }

}
