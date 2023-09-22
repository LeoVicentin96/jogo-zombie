using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade = 20;
    private Rigidbody rigibodyBala;
    public AudioClip SomDeMorte;

    private void Start()
    {
        rigibodyBala = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigibodyBala.MovePosition
            (rigibodyBala.position + 
            transform.forward * Velocidade * Time.deltaTime);
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        Quaternion rotacaoOpostaABala = Quaternion.LookRotation(-transform.forward);
        switch (objetoDeColisao.tag)
        {
            case "Inimigo":
                ControlaInimigo inimigo = objetoDeColisao.GetComponent<ControlaInimigo>();
                inimigo.TomarDano(1);
                inimigo.ParticulaSangue(transform.position, rotacaoOpostaABala);
            break;

            case "ChefeDeFase":
                ControlaChefe chefe = objetoDeColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(1);
                chefe.ParticulaSangue(transform.position, rotacaoOpostaABala);
                objetoDeColisao.GetComponent<ControlaChefe>().TomarDano(1);
            break;
        }

        Destroy(gameObject);
        
    }
}
