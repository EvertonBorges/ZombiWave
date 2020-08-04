using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{

    [SerializeField]
    private int quantidadeCura = 15;

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
