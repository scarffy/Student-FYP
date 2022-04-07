using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace FYP.Backend
{
    public class PhotonMobsManager : MonoBehaviourPunCallbacks
    {
        public bool isMasterClient;

        bool _isMasterClient 
        {
            get {
                return PhotonNetwork.IsMasterClient;
            }
            set
            {
                isMasterClient = value;
            } 
        }

        // Start is called before the first frame update
        void Start()
        {
            PhotonManager.Instance.OnJoin += UpdateGetter;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateGetter()
        {
            isMasterClient = _isMasterClient;
        }
    }
}