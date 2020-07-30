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
        if (other.CompareTag(Tags.INIMIGO))
        {
            other.GetComponent<ControlaInimigo>().TomarDano(dano);
        }
        Destroy(gameObject);
    }

}