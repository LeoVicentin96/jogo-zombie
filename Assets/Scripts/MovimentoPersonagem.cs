using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody meuRigidbody;

    void Awake ()
    {
        meuRigidbody = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        meuRigidbody.MovePosition
                (meuRigidbody.position +
                direcao.normalized * velocidade * Time.deltaTime); // move zumbi
    }

    public void Rotacionar(Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        meuRigidbody.MoveRotation(novaRotacao);
    }

    public void Morrer()
    {
        meuRigidbody.constraints = RigidbodyConstraints.None; // desabilita as constraints do rigibody
        meuRigidbody.velocity = Vector3.zero; // zera a velocidade do rigibody
        meuRigidbody.isKinematic = false;
        GetComponent<Collider>().enabled = false; // desabilida todos collider do objeto
    }
}
