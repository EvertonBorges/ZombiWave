using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{

    [SerializeField]
    private Slider sliderVidaJogador;

    [SerializeField]
    private GameObject painelGameOver;

    private ControlaJogador scriptControlaJogador;

    // Start is called before the first frame update
    void Start()
    {
        painelGameOver.SetActive(false);

        scriptControlaJogador = GameObject.FindWithTag(Tags.JOGADOR).GetComponent<ControlaJogador>();
        sliderVidaJogador.maxValue = scriptControlaJogador.GetVida();
        AtualizarSliderVidaJogador();

        Time.timeScale = 1;
    }

    public void AtualizarSliderVidaJogador()
    {
        sliderVidaJogador.value = scriptControlaJogador.GetVida();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        painelGameOver.SetActive(true);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Game");
    }

}
