using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private const string ANIMATOR_ATACANDO = "Atacando";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Atacar(bool estado)
    {
        _animator.SetBool(ANIMATOR_ATACANDO, estado);
    }
}
