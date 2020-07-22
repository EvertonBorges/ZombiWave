using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
	
	[SerializeField]
	private float velocidade = 20;
	
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
        if (other.CompareTag("Inimigo"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

}