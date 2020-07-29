using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{

    private static AudioSource instancia;
    private AudioSource meuAudioSource;

    void Awake()
    {
        meuAudioSource = GetComponent<AudioSource>();
        instancia = meuAudioSource;
    }

    public static AudioSource Instancia()
    {
        return instancia;
    }

}
