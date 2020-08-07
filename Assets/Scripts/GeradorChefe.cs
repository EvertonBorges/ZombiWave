using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{

    [SerializeField]
    private GameObject chefePrefab;

    [SerializeField]
    private float tempoEntreGeracoes = 60;

    private float _tempoParaProximaGeracao = 0f;
    private ControlaInterface _scriptControlaInterface;

    private void Start()
    {
        _tempoParaProximaGeracao = tempoEntreGeracoes;
        _scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > _tempoParaProximaGeracao)
        {
            Instantiate(chefePrefab, transform.position, Quaternion.identity);
            _scriptControlaInterface.AparecerTextoChefeCriado();
            _tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }

}
