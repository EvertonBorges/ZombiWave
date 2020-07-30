using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{

    [SerializeField]
    private GameObject zumbi;

    [SerializeField]
    private float tempoGerarZumbi = 1;

    [SerializeField]
    public LayerMask layerZumbi;

    private float _contadorTempo = 0;
    private float _distanciaDeGeracao = 3f;
    private float _distanciaDoJogadorParaGeracao = 20f;

    private GameObject _jogador;
    
    void Awake()
    {
        _jogador = GameObject.FindWithTag(Tags.JOGADOR);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _jogador.transform.position) > _distanciaDoJogadorParaGeracao)
        {
            _contadorTempo += Time.deltaTime;
            if (_contadorTempo >= tempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                _contadorTempo = 0;
            }
        }
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, layerZumbi);
        while (colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, layerZumbi);
            yield return null;
        }

        Instantiate(zumbi, posicaoDeCriacao, transform.rotation);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _distanciaDeGeracao);
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * _distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }

}
