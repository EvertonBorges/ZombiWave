using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour, IMatavel
{

    [SerializeField]
    private LayerMask mascaraChao;

    [SerializeField]
    private GameObject textoGameOver;

    [SerializeField]
    private ControlaInterface scriptControlaInterface;

    [SerializeField]
    private AudioClip somDeDano;

    private Vector3 direcao;

    private MovimentoJogador _movimentoJogador;
    private AnimacaoPersonagem _animacaoPersonagem;
    private Status _status;

    void Awake()
    {
        _movimentoJogador = GetComponent<MovimentoJogador>();
        _animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
        _status = GetComponent<Status>();
    }

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        _animacaoPersonagem.Movimentar(direcao.magnitude);

        if (_status.GetVida() <= 0 && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Game");
        }
    }

    void FixedUpdate()
    {
        _movimentoJogador.Movimentar(direcao, _status.GetVelocidade());
        _movimentoJogador.RotacaoJogador(mascaraChao);
    }

    public void TomarDano(int dano)
    {
        _status.SetVida(dano);
        ControlaAudio.Instancia().PlayOneShot(somDeDano);
        if (_status.GetVida() <= 0)
        {
            Morrer();
        }

        scriptControlaInterface.AtualizarSliderVidaJogador();
    }

    public int GetVida()
    {
        return _status.GetVida();
    }

    public void Morrer()
    {
        Time.timeScale = 0;
        textoGameOver.SetActive(true);
    }
}
