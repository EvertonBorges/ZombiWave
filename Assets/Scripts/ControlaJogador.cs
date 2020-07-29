using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{

    [SerializeField]
    private LayerMask mascaraChao;

    [SerializeField]
    private float velocidade = 10;

    [SerializeField]
    private GameObject textoGameOver;

    [SerializeField]
    private int vida = 100;

    [SerializeField]
    private ControlaInterface scriptControlaInterface;

    [SerializeField]
    private AudioClip somDeDano;

    private Vector3 direcao;

    private Animator _animator;
    private Rigidbody _rigidbody;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        bool movendo = direcao != Vector3.zero;
        _animator.SetBool("Movendo", movendo);

        if (vida <= 0 && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Game");
        }
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + direcao * velocidade * Time.deltaTime);

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, mascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;

            Quaternion rotacao = Quaternion.LookRotation(posicaoMiraJogador);
            _rigidbody.MoveRotation(rotacao);
        }
    }

    public void TomarDano(int dano)
    {
        vida -= dano;
        ControlaAudio.Instancia().PlayOneShot(somDeDano);
        if (vida <= 0)
        {
            Time.timeScale = 0;
            textoGameOver.SetActive(true);
        }

        scriptControlaInterface.AtualizarSliderVidaJogador();
    }

    public int GetVida()
    {
        return vida;
    }

}
