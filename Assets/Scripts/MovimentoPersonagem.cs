using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        _rigidbody.MovePosition(_rigidbody.position + direcao.normalized * velocidade * Time.deltaTime);
    }

    public void Rotacionar(Vector3 direcao)
    {
        Quaternion rotacao = Quaternion.LookRotation(direcao);
        _rigidbody.MoveRotation(rotacao);
    }

    public void Morrer()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.velocity= Vector3.zero;
        _rigidbody.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }

}