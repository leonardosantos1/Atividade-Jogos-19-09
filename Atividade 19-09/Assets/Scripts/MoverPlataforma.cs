using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlataforma : MonoBehaviour
{
    [SerializeField] private Transform ponto1, ponto2;
    [SerializeField] private Transform plataforma;
    [SerializeField] private float velocidade;
    [SerializeField] private float tempoPausa;

    private Vector3 pontoDestino;
    // Start is called before the first frame update
    void Start()
    {
        plataforma.position = ponto1.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoverPlataformaRotina());

    }
    IEnumerator MoverPlataformaRotina()
    {
        if (plataforma.position == ponto1.position)
        {
            yield return new WaitForSeconds(tempoPausa);
            pontoDestino = ponto2.position;
        }
        if (plataforma.position == ponto2.position)
        {
            yield return new WaitForSeconds(tempoPausa);
            pontoDestino = ponto1.position;
        }
        plataforma.position = Vector3.MoveTowards(plataforma.position, pontoDestino, (Time.deltaTime * velocidade));
    }
}
