using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{

    [SerializeField]
    private LayerMask mascaraChao;

    [SerializeField]
    private float velocidade = 10;

    [SerializeField]
    private GameObject textoGameOver;

    [SerializeField]
    private int vida = 100;

    [SerializeField]
    private ControlaInterface scriptControlaInterface;

    [SerializeField]
    private AudioClip somDeDano;

    private Vector3 direcao;

    private MovimentoJogador _movimentoJogador;
    private AnimacaoPersonagem _animacaoPersonagem;

    void Awake()
    {
        _movimentoJogador = GetComponent<MovimentoJogador>();
        _animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
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

        if (vida <= 0 && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Game");
        }
    }

    void FixedUpdate()
    {
        _movimentoJogador.Movimentar(direcao, velocidade);
        _movimentoJogador.RotacaoJogador(mascaraChao);
    }

    public void TomarDano(int dano)
    {
        vida -= dano;
        ControlaAudio.Instancia().PlayOneShot(somDeDano);
        if (vida <= 0)
        {
            Time.timeScale = 0;
            textoGameOver.SetActive(true);
        }

        scriptControlaInterface.AtualizarSliderVidaJogador();
    }

    public int GetVida()
    {
        return vida;
    }

}
