using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceMov : MonoBehaviour
{
    Rigidbody rb;               // Var para armazenar o componente RigidBody
    public float velocidade;    // Var para controlar a velocidade pela Unity
    float moverX, moverZ;       // Vars para adicionar movimentação

    void Start() {
        rb = GetComponent<Rigidbody>(); // Adicionar o Rigidbody
    }

    void FixedUpdate() {
        moverX = Input.GetAxis("Horizontal");   // Coletamos os valores dos eixos pré-estabelecidos
        moverZ = Input.GetAxis("Vertical");     // e adicionamos nas variáveis de movimento

        rb.AddForce(new Vector3(moverX, 0.0f, moverZ) * velocidade * Time.deltaTime); // Passamos os valores para uma força no RB
    }
}
