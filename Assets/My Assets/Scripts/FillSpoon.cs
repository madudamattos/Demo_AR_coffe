/* Autoria: Maria Eduarda Mattos 
 * GitHub: @madudamattos
 * Projeto VR coffe wosniak - ICA
 * 
 * Fill Spoon Script
 * 
 * Esse script enche a colher de cafe, e esvazia ela quando esta virada. 
 * Enche o filtro de café se o café for derramado em cima do filtro.
 * 
 * Isso é realizado da seguinte maneira:
 * Quando a colher entra em contado com o café do pote, o mesh renderer do objeto cafe que esta na colher é ativado. Quando a colher esta rotacionada, esse mesh renderer é desativado.
 * Se a colher estiver cheia e for rotacionada em cima do filtro de cafe, ativa uma ação de ativar o objeto cafe que esta dentro do filtro.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillSpoon : MonoBehaviour
{
    // GameObjects para interação 
    public GameObject coffe;

    // Variaveis de verificação de estado
    private bool isCoffeActive;
    private bool isRotated;
    private bool state;

    private bool canDrop = false;

    // Scrit de check de variavel
    [SerializeField] ActiveCoffeMesh coffeScript;

    // Utilizacao futura para sistema de particulas 
    //private ParticleSystem particle;
    //private Rigidbody rb;

    public ParticleSystem coffeParticle;
    public Transform coffeParticleTransform;

    void Start()
    {
        
    }

    // A funcao de ativar o cafe na colher é chamada apenas atraves da colisao pelo metodo OnTriggerEnter, portanto nao precisa ser chamada no update.
    // No Update é chamada somente a funcao DropCoffe que verifica constantemente se a colher esta derramando o cafe e chama as ações respectivas para tratar o casos
    void Update()
    {
        DropCoffe();

    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1f);
        canDrop = true;
    }

    // Ativa o cafe da colher quando a colher entra em contato com o cafe no pote
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "CoffePot")
        {
            SetCoffeOnSpoon(true);
            StartCoroutine(WaitTime());
        }
    }

    // Liga e desliga o mesh renderer do cafe na colher, atualiza o valor da variavel isCoffeActive
    void SetCoffeOnSpoon(bool state)
    {
        coffe.gameObject.GetComponent<MeshRenderer>().enabled = state;
        isCoffeActive = state;
    }

    // Verifica se o café está sendo derramado da colher, e se estiver em cima do filtro, enche o filtro de cafe
    void DropCoffe()
    {
        isRotated = isSpoonRotated();

        if (isRotated && isCoffeActive)
        {
            RaycastHit hit;

            if (Physics.Raycast(coffe.transform.position, Vector3.down, out hit, 10f))
            {
                // Verifica se o café esta sendo derrubado em cima do filtro. Se sim, chama a função para ativar visualmente o objeto do cafe no filtro
                if (hit.collider.gameObject.tag == "CoffeFilter")
                {
                    coffeScript.ActiveCoffe();
                }
            }

            // Desativa o café da colher da esponja quando ela esta rotacionada
            if (canDrop)
            {
                SetCoffeOnSpoon(false);
                ParticleSystem instance = Instantiate(coffeParticle, transform.position + new Vector3(-0.040f, 0, 0.030f), Quaternion.identity);
                instance.transform.Rotate(90, 0, 0);
                canDrop = false;
            }
        }
    }

    // Verifica se a colher esta rotacionada o suficiente para derramar o cafe
    bool isSpoonRotated()
    {

        Vector3 upVector = Vector3.up;
        Vector3 forwardVector = transform.forward * -1;

        float angle = Vector3.Angle(upVector, forwardVector);

        if (angle > 90f)
        { 
            return true;
        }

        return false;
    }
}
