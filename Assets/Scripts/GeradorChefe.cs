using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{

    [SerializeField]
    private GameObject chefePrefab;

    [SerializeField]
    private float tempoEntreGeracoes = 60;

    [SerializeField]
    private Transform[] posicoesPossiveisDeGeracao;

    private float _tempoParaProximaGeracao = 0f;
    private ControlaInterface _scriptControlaInterface;
    private Transform _jogador;

    private void Start()
    {
        _tempoParaProximaGeracao = tempoEntreGeracoes;
        _scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        _jogador = GameObject.FindGameObjectWithTag(Tags.JOGADOR).transform;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > _tempoParaProximaGeracao)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistanteDoJogador();
            Instantiate(chefePrefab, posicaoDeCriacao, Quaternion.identity);
            _scriptControlaInterface.AparecerTextoChefeCriado();
            _tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }

    private Vector3 CalcularPosicaoMaisDistanteDoJogador()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;

        foreach(Transform posicao in posicoesPossiveisDeGeracao)
        {
            float distanciaEntreOJogador = Vector3.Distance(posicao.position, _jogador.position);
            if (distanciaEntreOJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreOJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }

        return posicaoDeMaiorDistancia;
    }

}
