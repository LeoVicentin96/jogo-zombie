using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    public GameObject posicaoMira;

    public void RotacaoJogador(LayerMask MascaraChao)
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plano = new Plane(Vector3.up, posicaoMira.transform.position);

        //RaycastHit impacto;

        float distanciaColisao;

        if (plano.Raycast(raio, out distanciaColisao))
        {
            Vector3 localColisao = raio.GetPoint(distanciaColisao);
            localColisao.y = 0; //ou transform.position.y

            //direcao para onde vamos olhar baseado onde estamos
            Vector3 posicaoParaOlhar = localColisao - transform.position;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoParaOlhar);
            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }
    }
}