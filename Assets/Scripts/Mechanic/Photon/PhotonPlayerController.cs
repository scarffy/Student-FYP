using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using FYP.Backend;
using StarterAssets;
using UnityEngine.InputSystem;

public class PhotonPlayerController : MonoBehaviour
{
    [Space(20)]
    public GameObject playerObject;
    public GameObject cameraObject;
    public GameObject playerFollowCamera;
    public ThirdPersonController tpController;
    public BasicRigidBodyPush brbPush;
    public StarterAssetsInputs saInputs;
    public PlayerInput plInput;
    public Rigidbody rb;

    [Space(20)]
    public PhotonView pView;
    public PhotonTransformView pTransformView;

    public bool isOtherPlayer = true;

    void Start()
    {
        PhotonManager.Instance.OnJoin += InMultiplayer;
    }

    public void InMultiplayer()
    {
        isOtherPlayer = false;

        playerObject.AddComponent<PhotonTransformView>();
        pTransformView = playerObject.GetComponent<PhotonTransformView>();
        pTransformView.m_SynchronizePosition = true;
        pTransformView.m_SynchronizeRotation = true;

        gameObject.AddComponent<PhotonView>();
        pView = GetComponent<PhotonView>();

        pView.ObservedComponents = new List<Component>();
        pView.ObservedComponents.Add(pTransformView);

        PhotonManager.Instance.SendViewId(pView);
        
        if (!pView.IsMine)
        {
            //cameraObject.SetActive(false);
            
        }
    }

    public void InMultiplayerOther()
    {
        isOtherPlayer = true;

        playerObject.AddComponent<PhotonTransformView>();
        pTransformView = playerObject.GetComponent<PhotonTransformView>();
        pTransformView.m_SynchronizePosition = true;
        pTransformView.m_SynchronizeRotation = true;

        gameObject.AddComponent<PhotonView>();
        pView = GetComponent<PhotonView>();

        pView.ObservedComponents = new List<Component>();
        pView.ObservedComponents.Add(pTransformView);

        Destroy(cameraObject);
        cameraObject = null;

        Destroy(playerFollowCamera);
        playerFollowCamera = null;

        Destroy(tpController);
        tpController = null;

        Destroy(brbPush);
        brbPush = null;

        Destroy(saInputs);
        saInputs = null;

        Destroy(plInput);
        plInput = null;

        Destroy(rb);
        rb = null;
    }
}
