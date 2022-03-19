using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using FYP.Backend;

public class PhotonPlayerController : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject cameraObject;

    public PhotonView pView;
    public PhotonTransformView pTransformView;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        //InMultiplayer();
        PhotonManager.Instance.OnJoin += InMultiplayer;
    }

    public void InMultiplayer()
    {
        playerObject.AddComponent<PhotonTransformView>();
        pTransformView = playerObject.GetComponent<PhotonTransformView>();
        pTransformView.m_SynchronizePosition = true;
        pTransformView.m_SynchronizeRotation = true;

        gameObject.AddComponent<PhotonView>();
        pView = GetComponent<PhotonView>();

        pView.ObservedComponents = new List<Component>();
        pView.ObservedComponents.Add(pTransformView);

        PhotonManager.Instance.SendViewId(pView);
        
        if (pView.IsMine)
        {
            //cameraObject.SetActive(false);
            
        }
    }
}
