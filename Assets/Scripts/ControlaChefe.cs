using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel
{

    [SerializeField]
    private GameObject kitMedico;

    [SerializeField]
    private Slider sliderVida;

    private Transform _jogador;
    private NavMeshAgent _navMeshAgent;

    private Status _status;
    private AnimacaoPersonagem _animacaoPersonagem;
    private MovimentoPersonagem _movimentoPersonagem;

    private void Start()
    {
        _jogador = GameObject.FindWithTag(Tags.JOGADOR).transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        _status = GetComponent<Status>();
        _animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        _movimentoPersonagem = GetComponent<MovimentoPersonagem>();

        _navMeshAgent.speed = _status.GetVelocidade();

        sliderVida.maxValue = _status.GetVida();
        AtualizarInterface();
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_jogador.position);
        _animacaoPersonagem.Movimentar(_navMeshAgent.velocity.magnitude);

        if (_navMeshAgent.hasPath)
        {
            bool estouPertoDoJogador = _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;
            _animacaoPersonagem.Atacar(estouPertoDoJogador);
            if (estouPertoDoJogador)
            {
                Vector3 direcao = _jogador.position - transform.position;
                _movimentoPersonagem.Rotacionar(direcao);
            }
        }
    }

    void AtacaJogador()
    {
        int dano = Random.Range(30, 40);
        _jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    public void TomarDano(int dano)
    {
        _status.TirarVida(dano);
        AtualizarInterface();
        if (_status.GetVida() <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        _animacaoPersonagem.Morrer();
        _movimentoPersonagem.Morrer();
        this.enabled = false;
        _navMeshAgent.enabled = false;

        Instantiate(kitMedico, transform.position, Quaternion.identity);
        Destroy(gameObject, 2f);
    }

    void AtualizarInterface ()
    {
        sliderVida.value = _status.GetVida();
    }

}
