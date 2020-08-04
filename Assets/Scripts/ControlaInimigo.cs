using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{

    [SerializeField]
    private AudioClip somDeMorte;

    [SerializeField]
    private GameObject kitMedicoPrefab;

    private GameObject _jogador;
    private MovimentoPersonagem _movimentoPersonagem;
    private AnimacaoPersonagem _animacaoPersonagem;
    private Status _status;
    private Vector3 _posicaoAleatoria;
    private Vector3 _direcao;
    private float _contadorVagar;
    private float _tempoEntrePosicoesAleatorias = 4f;
    private float _porcentagemGerarKitMedico = 0.1f;
    private ControlaInterface _scriptControlainterface;
    private GeradorZumbis geradorZumbis;

    void Start()
    {
        _jogador = GameObject.FindGameObjectWithTag(Tags.JOGADOR);

        _movimentoPersonagem = GetComponent<MovimentoPersonagem>();
        _animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        _status = GetComponent<Status>();

        _scriptControlainterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;

        AleatorizarZumbis();
    }

    void FixedUpdate()
    {
		float distancia = Vector3.Distance(transform.position, _jogador.transform.position);

        _movimentoPersonagem.Rotacionar(_direcao);
        _animacaoPersonagem.Movimentar(_direcao.magnitude);

        if (distancia > 15)
        {
            Vagar();
        }
		else if (distancia > 2.5)
		{
            MovimentarEmDirecaoAoJogador();
		} 
        else
        {
            _direcao = _jogador.transform.position - transform.position;
            _animacaoPersonagem.Atacar(true);
        }
    }

    void Vagar()
    {
        _contadorVagar -= Time.deltaTime;
        if (_contadorVagar <= 0)
        {
            _posicaoAleatoria = AleatorizarPosicao();
            _contadorVagar = _tempoEntrePosicoesAleatorias;
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, _posicaoAleatoria) <= 0.05f;
        if (!ficouPertoOSuficiente)
        {
            _direcao = _posicaoAleatoria - transform.position;
            _movimentoPersonagem.Movimentar(_direcao, _status.GetVelocidade());
        }
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    void MovimentarEmDirecaoAoJogador()
    {
        _direcao = _jogador.transform.position - transform.position;
        _movimentoPersonagem.Movimentar(_direcao, _status.GetVelocidade());
        _animacaoPersonagem.Atacar(false);
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
        _status.TirarVida(dano);
        if (_status.GetVida() <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        Destroy(gameObject);
        ControlaAudio.Instancia().PlayOneShot(somDeMorte);
        VerificarGeracaoKitMedico(_porcentagemGerarKitMedico);
        _scriptControlainterface.AtualizarQuantidadeDeZumbisMortos();
        geradorZumbis.DiminuirQuantidadeDeZumbisVivos();
    }

    void VerificarGeracaoKitMedico(float porcentagemGeracao)
    {
        if (Random.value <= porcentagemGeracao)
        {
            Instantiate(kitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }

    public void SetGeradorZumbis(GeradorZumbis geradorZumbis)
    {
        this.geradorZumbis = geradorZumbis;
    }

}