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

    [Header("Stuffs to destroy for other player")]
    public GameObject cameraObject;
    public Animator animator;
    public GameObject playerFollowCamera;
    public ThirdPersonController tpController;
    public BasicRigidBodyPush brbPush;
    public StarterAssetsInputs saInputs;
    public PlayerInput plInput;

    [Space(20)]
    public PhotonView pView;
    public PhotonTransformView pTransformView;
    public PhotonAnimatorView photonAnimatorView;

    public bool isOtherPlayer = true;

    public Photon.Realtime.Player phPlayer;

    [Space(20)]
    public FYP.Data.LocalSaveFile SaveFile;

    void Start()
    {
        PhotonManager.Instance.OnJoin += InMultiplayer;
    }

    private void OnDestroy()
    {
        PhotonManager.Instance.OnJoin -= InMultiplayer;
    }

    public void InMultiplayer()
    {
        isOtherPlayer = false;

        playerObject.AddComponent<PhotonTransformView>();
        pTransformView = playerObject.GetComponent<PhotonTransformView>();
        pTransformView.m_SynchronizePosition = true;
        pTransformView.m_SynchronizeRotation = true;

        photonAnimatorView = playerObject.AddComponent<PhotonAnimatorView>();
        
        for (int i = 0; i < animator.layerCount; i++)
        {
            photonAnimatorView.SetLayerSynchronized(i, PhotonAnimatorView.SynchronizeType.Discrete);
        }

        foreach (var animatorControllerParameter in animator.parameters)
        {
            photonAnimatorView.SetParameterSynchronized(animatorControllerParameter.name, (PhotonAnimatorView.ParameterType)animatorControllerParameter.type,
                PhotonAnimatorView.SynchronizeType.Discrete);
        }

        gameObject.AddComponent<PhotonView>();
        pView = GetComponent<PhotonView>();

        pView.ObservedComponents = new List<Component>();
        pView.ObservedComponents.Add(pTransformView);
        pView.ObservedComponents.Add(photonAnimatorView);

        PhotonManager.Instance.SendViewId(pView);

    }

    public void InMultiplayerOther()
    {
        isOtherPlayer = true;

        playerObject.AddComponent<PhotonTransformView>();
        pTransformView = playerObject.GetComponent<PhotonTransformView>();
        pTransformView.m_SynchronizePosition = true;
        pTransformView.m_SynchronizeRotation = true;

        photonAnimatorView = playerObject.AddComponent<PhotonAnimatorView>();
        
        for (int i = 0; i < animator.layerCount; i++)
        {
            photonAnimatorView.SetLayerSynchronized(i, PhotonAnimatorView.SynchronizeType.Discrete);
        }

        foreach (var animatorControllerParameter in animator.parameters)
        {
            photonAnimatorView.SetParameterSynchronized(animatorControllerParameter.name, (PhotonAnimatorView.ParameterType)animatorControllerParameter.type,
                PhotonAnimatorView.SynchronizeType.Discrete);
        }

        gameObject.AddComponent<PhotonView>();
        pView = GetComponent<PhotonView>();

        pView.ObservedComponents = new List<Component>();
        pView.ObservedComponents.Add(pTransformView);
        pView.ObservedComponents.Add(photonAnimatorView);

        //! Destroying stuffs we don't need.
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
    }

    public void InMultiplayerOther(Photon.Realtime.Player player)
    {
        isOtherPlayer = true;

        playerObject.AddComponent<PhotonTransformView>();
        pTransformView = playerObject.GetComponent<PhotonTransformView>();
        pTransformView.m_SynchronizePosition = true;
        pTransformView.m_SynchronizeRotation = true;

        photonAnimatorView = playerObject.AddComponent<PhotonAnimatorView>();

        for (int i = 0; i < animator.layerCount; i++)
        {
            photonAnimatorView.SetLayerSynchronized(i, PhotonAnimatorView.SynchronizeType.Discrete);
        }

        foreach (var animatorControllerParameter in animator.parameters)
        {
            photonAnimatorView.SetParameterSynchronized(animatorControllerParameter.name, (PhotonAnimatorView.ParameterType)animatorControllerParameter.type,
                PhotonAnimatorView.SynchronizeType.Discrete);
        }

        gameObject.AddComponent<PhotonView>();
        pView = GetComponent<PhotonView>();

        pView.ObservedComponents = new List<Component>();
        pView.ObservedComponents.Add(pTransformView);
        pView.ObservedComponents.Add(photonAnimatorView);

        //! Destroying stuffs we don't need.
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

        if (player.CustomProperties.ContainsKey("PlayFabID"))
        {
            //Debug.Log("Contain PlayFabID");
            gameObject.name = (string)player.CustomProperties["PlayFabID"];
        }
        else
        {
            //Debug.Log("Contain No Key");
        }
    }


    /// <summary>
    /// Remove photon component and destroy all other players
    /// </summary>
    public void OnLeftRoom()
    {
        Destroy(pTransformView);
        Destroy(photonAnimatorView);
        Destroy(pView);
        
        pTransformView = null;
        photonAnimatorView = null;
        pView = null;
    }
}
