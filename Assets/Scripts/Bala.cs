using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
	
	[SerializeField]
	private float velocidade = 20;

    [SerializeField]
    private int dano = 1;
	
	private Rigidbody _rigidbody;
	
	void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + transform.forward * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Quaternion rotacaoOpostaABala = Quaternion.LookRotation(-transform.forward);
        switch(other.tag)
        {
            case Tags.INIMIGO:
                ControlaInimigo scriptInimigo = other.GetComponent<ControlaInimigo>();
                scriptInimigo.TomarDano(dano);
                scriptInimigo.ParticulaSangue(transform.position, rotacaoOpostaABala);
                break;
            case Tags.CHEFE_DE_FASE:
                ControlaChefe scriptChefe = other.GetComponent<ControlaChefe>();
                scriptChefe.TomarDano(dano);
                scriptChefe.ParticulaSangue(transform.position, rotacaoOpostaABala);
                break;
        }
        Destroy(gameObject);
    }

}