using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlataforma : MonoBehaviour
{
    [SerializeField] private Transform ponto1, ponto2;//pega o transform dos pontos que a plataforma irá de mover
    [SerializeField] private Transform plataforma;//pega o transform da plataforma
    [SerializeField] private float velocidade;//velocidade que a plataforma irá de mover
    [SerializeField] private float tempoPausa;//tempo de causa da plataforma

    private Vector3 pontoDestino;//ponto de destino da plataforma
    // Start is called before the first frame update
    void Start()
    {
        plataforma.position = ponto1.position;// seta a position da plataforma com a position do ponto1 ao começar a cena 
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoverPlataformaRotina());// chama o metodo de rotina

    }

    //realiza a movimentação da plataforma para dois pontos predeterminados
    IEnumerator MoverPlataformaRotina()
    {
        //verificação para ver se a position da plataforma se encontra igual a do ponto1
        if (plataforma.position == ponto1.position)
        {
            //faz a plataforma esperar por um tempo determinado para continuar seu ciclo
            yield return new WaitForSeconds(tempoPausa);
            //seta o ponto de destino da plataforma igual ao proximo ponto
            pontoDestino = ponto2.position;
        }
        //verificação para ver se a position da plataforma se encontra igual a do ponto2
        if (plataforma.position == ponto2.position)
        {
            //faz a plataforma esperar por um tempo determinado para continuar seu ciclo
            yield return new WaitForSeconds(tempoPausa);
            //seta o ponto de destino da plataforma igual ao proximo ponto
            pontoDestino = ponto1.position;
        }
        //realiza a movimentação da plataforma para o ponto de destino setado
        plataforma.position = Vector3.MoveTowards(plataforma.position, pontoDestino, (Time.deltaTime * velocidade));
    }
}
