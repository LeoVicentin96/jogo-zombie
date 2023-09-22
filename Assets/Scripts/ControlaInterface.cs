using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoPontuacaoMaxima;
    private float tempoPontuacaoSalva;
    private int quantidadeDeZumbisMortos;
    public Text TextoDaQuantidadeDeZumbisMortos;
    public Text TextoChefeAparece;

    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>(); // pega a variavael , acha o jogador e pega o componente ControlaJogador

        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        tempoPontuacaoSalva = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    public void AtualizarSliderVidaJogador ()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void AtualizarQuantidadeDeZumbisMortos()
    {
        quantidadeDeZumbisMortos++;
        TextoDaQuantidadeDeZumbisMortos.text = $"x {quantidadeDeZumbisMortos}";

    }

    public void GameOver()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)(Time.timeSinceLevelLoad / 60); // o int elimina os quebrados depois da virgula
        int segundos = (int)(Time.timeSinceLevelLoad % 60); // modulo pega o resto da divisao
        TextoTempoDeSobrevivencia.text = $"Você sobreviveu por {minutos}min e {segundos}s";

        AjustarPontuacaoMaxima(minutos, segundos);
    }

    void AjustarPontuacaoMaxima(int minutos, int segundos)
    {
        if (Time.timeSinceLevelLoad > tempoPontuacaoSalva)
        {
            tempoPontuacaoSalva = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = $"Seu melhor tempo é {minutos} e {segundos}s";
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalva);
        }
        if (TextoPontuacaoMaxima.text == "")
        {
            minutos = (int)tempoPontuacaoSalva / 60;
            segundos = (int)tempoPontuacaoSalva % 60;
            TextoPontuacaoMaxima.text = $"Seu melhor tempo é {minutos} e {segundos}s";
        }
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("game");
    }

    public void AparecerTextoChefeCriado()
    {
        StartCoroutine(DesaparecerTexto(2, TextoChefeAparece));
    }

    IEnumerator DesaparecerTexto(float tempoDeSumico, Text textoParaSumir)
    {
        textoParaSumir.gameObject.SetActive(true);
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1; // alfa do texto totalmente visivel
        textoParaSumir.color = corTexto;
        yield return new WaitForSeconds(1); // espera 1 segundo mostrando o texto
        float contador = 0;
        while (textoParaSumir.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeSumico;
            corTexto.a = Mathf.Lerp(1, 0, contador); // troca de um numero para outro numero baseado no contador
            textoParaSumir.color = corTexto;
            if(textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
