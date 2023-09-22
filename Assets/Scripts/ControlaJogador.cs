using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlaJogador : MonoBehaviour,IMatavel, ICuravel
{
    private Vector3 direcao;
    public LayerMask MascaraDoChao;
    public GameObject TextoGameOver;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;


    private void Start()
    {
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }

    void Update()
    {
        //Inputs do Jogador - Guardando teclas apertadas
        float eixoX =  Input.GetAxis("Horizontal");
       float eixoZ =  Input.GetAxis("Vertical");

       direcao = new Vector3(eixoX, 0 , eixoZ);

       animacaoJogador.Movimentar(direcao.magnitude); // magnitude pega o valor inteiro do Vector3
    }

    private void FixedUpdate()
    {
        meuMovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);

        meuMovimentoJogador.RotacaoJogador(MascaraDoChao);
    }

    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano; // subtrai da vida e volta o resultado pra variavel vida

        scriptControlaInterface.AtualizarSliderVidaJogador();

        ControlaAudio.instancia.PlayOneShot(SomDeDano);

        if (statusJogador.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        scriptControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        statusJogador.Vida += quantidadeDeCura;
        if (statusJogador.Vida > statusJogador.VidaInicial) // mantendo a vida até 100 que é a quantidade de vida incial
        {
            statusJogador.Vida = statusJogador.VidaInicial;
        }
        scriptControlaInterface.AtualizarSliderVidaJogador(); // atualiza slider da vida
    }
}
