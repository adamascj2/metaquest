using UnityEngine;
using System.Collections;
 
public class Mover : MonoBehaviour
{    public float velocidadeTranslacao = 3f;
    public float velocidadeRotacao = 0.07f;
    public OVRInput.RawButton botaoMovimento;
    public OVRInput.RawButton botaoRotacao;
    public float forçaSalto = 2.0f;
    public OVRInput.RawButton botaoSalto;
    private bool estaNoChao = true;

    private Rigidbody rb;
    private bool botaoMovimentoPressionado = false;
    private bool botaoRotacaoPressionado = false;
    private bool rotacaoAtiva = false;
    public float gravity = -30f;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
            rb.AddForce(Vector3.up * gravity * Time.fixedDeltaTime);
        // Verifica os botões
        botaoMovimentoPressionado = OVRInput.Get(botaoMovimento);
        botaoRotacaoPressionado = OVRInput.Get(botaoRotacao);

        // Movimento
        if (botaoMovimentoPressionado)
        {
            Vector3 direcao = transform.forward;
            rb.AddForce(direcao * velocidadeTranslacao);
        }
        else
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.deltaTime * 5);
        }

        // Rotação
        rotacaoAtiva = botaoRotacaoPressionado;

        if (rotacaoAtiva)
        {
            // Aplica um torque constante para rotação suave
            rb.AddTorque(Vector3.up * velocidadeRotacao, ForceMode.Force);
        }
        else
        {
            // Desacelera a rotação suavemente
            rb.angularVelocity *= 0.9f; // Ajuste este valor para controlar a velocidade de desaceleração
        }
 // Salto
        if (OVRInput.GetDown(botaoSalto) && estaNoChao)
        {
            rb.AddForce(Vector3.up * forçaSalto, ForceMode.Impulse);
            estaNoChao = false;
        }

        // Verificar se o jogador está no chão (simplificado)
        if (rb.linearVelocity.y < 0)
        {
            estaNoChao = Physics.Raycast(transform.position, -Vector3.up, 0.1f);
        }
    }
}