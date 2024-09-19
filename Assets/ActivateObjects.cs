using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class ActivateObjects : MonoBehaviour
{
    //public GameObject interactables;
    //public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeAfterSceneLoad());
    }

    IEnumerator InitializeAfterSceneLoad()
    {
        yield return new WaitForSeconds(1f);
        InitializeObjects();
    }

    void InitializeObjects()
    {
        var rooms = MRUK.Instance.GetCurrentRoom().GetRoomAnchors();

        foreach (var anchor in rooms)
        {
            if (anchor.gameObject.name == "TABLE")
            {
                Vector3 anchorCenter = anchor.GetAnchorCenter();
                Quaternion anchorRotation = anchor.transform.rotation;

                this.transform.position = anchorCenter;
                this.transform.rotation = anchorRotation;
                this.transform.GetChild(0).gameObject.SetActive(true);
                
                break;
            }
        }


    }

}
