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

    [SerializeField]
    private Text textoQuantidadeDeZumbisMortos;

    [SerializeField]
    private Text textoChefeAparece;

    private ControlaJogador _scriptControlaJogador;
    private float _tempoPontuacaoSalvo;
    private int _quantidadeDeZumbisMortos;

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

    public void AtualizarQuantidadeDeZumbisMortos()
    {
        _quantidadeDeZumbisMortos++;
        textoQuantidadeDeZumbisMortos.text = string.Format("x {0}", _quantidadeDeZumbisMortos);
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

    public void AparecerTextoChefeCriado()
    {
        StartCoroutine(DesaparecerTexto(2, textoChefeAparece));
    }

    IEnumerator DesaparecerTexto (float tempoDeSumico, Text textoParaSumir)
    {
        textoParaSumir.gameObject.SetActive(true);
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while (textoParaSumir.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;
            if (textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

}
