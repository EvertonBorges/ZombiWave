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
    private int _quantidadeMaximaDeZumbisVivos = 2;
    private int _quantidadeDeZumbisVivos;
    private float _tempoProximoAumentoDeDificuldade = 30;
    private float _contadorDeAumentarDificuldade;

    private GameObject _jogador;
    
    void Awake()
    {
        _jogador = GameObject.FindWithTag(Tags.JOGADOR);
        for(int i = 0; i < _quantidadeMaximaDeZumbisVivos; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position, _jogador.transform.position) > _distanciaDoJogadorParaGeracao;
        bool possoGerarZumbisPelaQuantidade = _quantidadeDeZumbisVivos < _quantidadeMaximaDeZumbisVivos;

        if (possoGerarZumbisPelaDistancia && possoGerarZumbisPelaQuantidade)
        {
            _contadorTempo += Time.deltaTime;
            if (_contadorTempo >= tempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                _contadorTempo = 0;
            }
        }

        if (Time.timeSinceLevelLoad > _contadorDeAumentarDificuldade)
        {
            _quantidadeMaximaDeZumbisVivos++;
            _contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + _tempoProximoAumentoDeDificuldade;
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

        ControlaInimigo scriptZumbi = Instantiate(zumbi, posicaoDeCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        scriptZumbi.SetGeradorZumbis(this);
        _quantidadeDeZumbisVivos++;
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

    public void DiminuirQuantidadeDeZumbisVivos()
    {
        _quantidadeDeZumbisVivos--;
    }

}
