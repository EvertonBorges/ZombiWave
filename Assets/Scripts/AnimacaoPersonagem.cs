using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private const string ANIMATOR_ATACANDO = "Atacando";
    private const string ANIMATOR_MOVENDO = "Movendo";
    private const string ANIMATOR_MORRER = "Morrer";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Atacar(bool estado)
    {
        _animator.SetBool(ANIMATOR_ATACANDO, estado);
    }

    public void Movimentar(float movimento)
    {
        _animator.SetFloat(ANIMATOR_MOVENDO, movimento);
    }

    public void Morrer()
    {
        _animator.SetTrigger(ANIMATOR_MORRER);
    }

}