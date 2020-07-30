using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{

    private const string PLAYERPREFS_PONTUACAO_MAXIMA = "PontuacaoMaxima";

    [SerializeField]
    private Slider sliderVidaJogador;

    [SerializeField]
    private GameObject painelGameOver;

    [SerializeField]
    private Text textoTempoSobrevivencia;

    [SerializeField]
    private Text textoPontuacaoMaxima;

    private ControlaJogador _scriptControlaJogador;
    private float _tempoPontuacaoSalvo;

    void Awake()
    {
        _tempoPontuacaoSalvo = PlayerPrefs.HasKey(PLAYERPREFS_PONTUACAO_MAXIMA) ? PlayerPrefs.GetFloat(PLAYERPREFS_PONTUACAO_MAXIMA) : 0f;
    }

    void Start()
    {
        painelGameOver.SetActive(false);

        _scriptControlaJogador = GameObject.FindWithTag(Tags.JOGADOR).GetComponent<ControlaJogador>();
        sliderVidaJogador.maxValue = _scriptControlaJogador.GetVida();
        AtualizarSliderVidaJogador();

        Time.timeScale = 1;
    }

    public void AtualizarSliderVidaJogador()
    {
        sliderVidaJogador.value = _scriptControlaJogador.GetVida();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        painelGameOver.SetActive(true);

        float time = Time.timeSinceLevelLoad;

        int minutos = Mathf.FloorToInt(time / 60);
        int segundos = Mathf.FloorToInt(time % 60);
        textoTempoSobrevivencia.text = "Você sobreviveu por " + minutos + "min e " + segundos + "s";

        AjustarPontuacaoMaxima();
    }

    void AjustarPontuacaoMaxima()
    {
        if(Time.timeSinceLevelLoad > _tempoPontuacaoSalvo)
        {
            _tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            PlayerPrefs.SetFloat(PLAYERPREFS_PONTUACAO_MAXIMA, _tempoPontuacaoSalvo);
        }

        int minutos = Mathf.FloorToInt(_tempoPontuacaoSalvo / 60);
        int segundos = Mathf.FloorToInt(_tempoPontuacaoSalvo % 60);

        textoPontuacaoMaxima.text = string.Format("Seu melhor tempo é {0}min e {1}s", minutos, segundos);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Game");
    }

}
