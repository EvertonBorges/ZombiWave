using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{

    [SerializeField]
    private Slider sliderVidaJogador;

    private ControlaJogador scriptControlaJogador;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag(Tags.JOGADOR).GetComponent<ControlaJogador>();
        sliderVidaJogador.maxValue = scriptControlaJogador.GetVida();
        AtualizarSliderVidaJogador();
    }

    public void AtualizarSliderVidaJogador()
    {
        sliderVidaJogador.value = scriptControlaJogador.GetVida();
    }

}
