using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlataformaInimigo : MonoBehaviour
{
    [SerializeField] private Transform pontoA, pontoB;//pega o transform dos pontos que a plataforma irá de mover
    [SerializeField] private Transform monstro;//pega o transform do inimigo
    [SerializeField] private float velocidade;//velocidade que o inimigo irá de mover
    [SerializeField] private float tempoPausa;//tempo de causa do inimigo

    private Vector3 pontoDestino;//ponto de destino da plataforma
    // Start is called before the first frame update
    void Start()
    {
        monstro.position = pontoA.position;// seta a position do inimigo com a position do ponto1 ao começar a cena 
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoverPlataformaRotina());// chama o metodo de rotina

    }

    //realiza a movimentação do inimigo para dois pontos predeterminados
    IEnumerator MoverPlataformaRotina()
    {   //verificação para ver se a position do inimigo se encontra igual a do ponto1
        if (monstro.position == pontoA.position)
        {
            //faz a flipagem do inimigo
            monstro.eulerAngles = new Vector3(0, 0,0);
            //faz o inimigo esperar por um tempo determinado para continuar seu ciclo
            yield return new WaitForSeconds(tempoPausa);
            //seta o ponto de destino do inimigo igual ao proximo ponto
            pontoDestino = pontoB.position;
        }
         //verificação para ver se a position do inimigo se encontra igual a do ponto1
            if (monstro.position == pontoB.position)
        {
            //faz a flipagem do inimigo
            monstro.eulerAngles = new Vector3(0, 180,0);
            //faz o inimigo esperar por um tempo determinado para continuar seu ciclo
            yield return new WaitForSeconds(tempoPausa);
            //seta o ponto de destino do inimigoigual ao proximo ponto
            pontoDestino = pontoA.position;
        }
        //realiza a movimentação do inimigo para o ponto de destino setado
        monstro.position = Vector3.MoveTowards(monstro.position, pontoDestino,(Time.deltaTime * velocidade));
    }
}
