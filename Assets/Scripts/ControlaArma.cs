﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
	
	[SerializeField]
	private GameObject bala;
	
	[SerializeField]
	private GameObject canoDaArma;

    [SerializeField]
    private AudioClip somDeTiro;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
		{
			Instantiate(bala, canoDaArma.transform.position, canoDaArma.transform.rotation);
            ControlaAudio.Instancia().PlayOneShot(somDeTiro);
		}
    }
}
