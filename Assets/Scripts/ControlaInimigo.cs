using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{
    private const string ANIMATOR_ATACANDO = "Atacando";

    [SerializeField]
    private GameObject jogador;

    [SerializeField]
    private float velocidade = 5;

    private Rigidbody _rigidbody;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
		float distancia = Vector3.Distance(transform.position, jogador.transform.position);
		Vector3 direcao = jogador.transform.position - transform.position;
		
		Quaternion rotacao = Quaternion.LookRotation(direcao);
		_rigidbody.MoveRotation(rotacao);

		if (distancia > 2.5)
		{
			_rigidbody.MovePosition(_rigidbody.position + direcao.normalized * velocidade * Time.deltaTime);
            _animator.SetBool(ANIMATOR_ATACANDO, false);
		} else
        {
            _animator.SetBool(ANIMATOR_ATACANDO, true);
        }
    }
}
