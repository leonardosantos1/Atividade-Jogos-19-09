using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlataformaInimigo : MonoBehaviour
{
    [SerializeField] private Transform pontoA, pontoB;
    [SerializeField] private Transform monstro;
    [SerializeField] private float velocidade;
    [SerializeField] private float tempoPausa;


    private Vector3 pontoDestino;
    // Start is called before the first frame update
    void Start()
    {
        monstro.position = pontoA.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoverPlataformaRotina());

    }
    IEnumerator MoverPlataformaRotina()
    {
        if (monstro.position == pontoA.position)
        {
            monstro.eulerAngles = new Vector3(0, 0,0);
            yield return new WaitForSeconds(tempoPausa);
            pontoDestino = pontoB.position;
        }
        if(monstro.position == pontoB.position)
        {
            monstro.eulerAngles = new Vector3(0, 180,0);
            yield return new WaitForSeconds(tempoPausa);
            pontoDestino = pontoA.position;
        }
        monstro.position = Vector3.MoveTowards(monstro.position, pontoDestino,(Time.deltaTime * velocidade));
    }
}
