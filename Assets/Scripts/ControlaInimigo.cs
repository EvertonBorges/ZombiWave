using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{

    [SerializeField]
    private GameObject jogador;

    [SerializeField]
    private float velocidade = 5;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 direcao = jogador.transform.position - transform.position;
        _rigidbody.MovePosition(_rigidbody.position + direcao.normalized * velocidade * Time.deltaTime);
    }
}
