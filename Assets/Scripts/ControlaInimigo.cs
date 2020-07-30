using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{

    [SerializeField]
    private AudioClip somDeMorte;

    private GameObject _jogador;
    private MovimentoPersonagem _movimentoPersonagem;
    private AnimacaoPersonagem _animacaoPersonagem;
    private Status _status;

    void Start()
    {
        _jogador = GameObject.FindGameObjectWithTag(Tags.JOGADOR);

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
            _movimentoPersonagem.Movimentar(direcao, _status.GetVelocidade());
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

    public void TomarDano(int dano)
    {
        _status.SetVida(dano);
        if (_status.GetVida() <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject);
        ControlaAudio.Instancia().PlayOneShot(somDeMorte);
    }

}