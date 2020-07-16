using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour
{

    public float velocidade = 10;

    private Vector3 direcao;

    private Animator _animator;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        bool movendo = direcao != Vector3.zero;
        _animator.SetBool("Movendo", movendo);
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + direcao * velocidade * Time.deltaTime);
    }

}
