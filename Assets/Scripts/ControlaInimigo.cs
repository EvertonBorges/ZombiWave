using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{

    private GameObject _jogador;
    private MovimentoPersonagem _movimentoPersonagem;
    private AnimacaoPersonagem _animacaoPersonagem;
    private Status _status;

    void Start()
    {
        _jogador = GameObject.FindGameObjectWithTag("Jogador");

        _movimentoPersonagem = GetComponent<MovimentoPersonagem>();
        _animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        _status = GetComponent<Status>();

        AleatorizarZumbis();
    }

    void FixedUpdate()
    {
		float distancia = Vector3.Distance(transform.position, _jogador.transform.position);
		Vector3 direcao = _jogador.transform.position - transform.position;

        _movimentoPersonagem.Rotacionar(direcao);

		if (distancia > 2.5)
		{
            _movimentoPersonagem.Movimentar(direcao, _status.velocidade);
            _animacaoPersonagem.Atacar(false);
		} 
        else
        {
            _animacaoPersonagem.Atacar(true);
        }
    }

    void AtacaJogador ()
    {
        int dano = Random.Range(20, 30);
        _jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbis()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

}