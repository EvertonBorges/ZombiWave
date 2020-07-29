using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    private const string ANIMATOR_ATACANDO = "Atacando";

    [SerializeField]
    private float velocidade = 5;

    private Animator _animator;
    private GameObject _jogador;
    private MovimentoPersonagem _movimentoPersonagem;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _movimentoPersonagem = GetComponent<MovimentoPersonagem>();

        _jogador = GameObject.FindGameObjectWithTag("Jogador");
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
		float distancia = Vector3.Distance(transform.position, _jogador.transform.position);
		Vector3 direcao = _jogador.transform.position - transform.position;

        _movimentoPersonagem.Rotacionar(direcao);

		if (distancia > 2.5)
		{
            _movimentoPersonagem.Movimentar(direcao, velocidade);
            _animator.SetBool(ANIMATOR_ATACANDO, false);
		} else
        {
            _animator.SetBool(ANIMATOR_ATACANDO, true);
        }
    }

    void AtacaJogador ()
    {
        int dano = Random.Range(20, 30);
        _jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

}