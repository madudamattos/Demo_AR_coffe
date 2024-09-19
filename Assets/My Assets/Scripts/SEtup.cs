using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class SEtup : MonoBehaviour
{
    public GameObject obj;
    private Vector3 anchorCenter;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeAfterSceneLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InitializeAfterSceneLoad()
    {
        // Aguarde um curto período para garantir que todos os objetos da cena estejam carregados
        yield return new WaitForSeconds(1f);

        // Agora execute o código para instanciar os prefabs
        MRUKAnchor[] anchors = FindObjectsOfType<MRUKAnchor>();

        foreach (MRUKAnchor anchor in anchors)
        {
            if (anchor.gameObject.name == "TABLE")
            {
                Vector3 anchorCenter = anchor.GetAnchorCenter();
                Vector3 anchorSize = anchor.GetAnchorSize();

                obj.SetActive(true);
                obj.transform.position = anchorCenter;

                //GameObject instance = Instantiate(prefab, instancePosition, anchor.transform.rotation);
                //obj.transform.SetParent(anchor.transform, true);
                //obj.transform.position = Vector3.zero;

                //anchor.transform.transform.SetParent(obj.transform, true);
            }
        }

    }
}
