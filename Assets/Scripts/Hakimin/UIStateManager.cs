using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FYP.UI
{
    public class UIStateManager : MonoBehaviour
    {
        #region public variable
        public static UIStateManager Instance { get; private set; }
        public State state;
        public enum State
        {
            none,
            signup,
            signin,
            status,
            inventory,
            sell,
            buy,
        }
        #endregion

        #region private variable
        [Header("Panels")]
        [SerializeField] GameObject signupPanel;
        [SerializeField] GameObject signinPanel;
        [SerializeField] GameObject statusPanel;
        [SerializeField] GameObject inventoryPanel;
        [SerializeField] GameObject sellPanel;
        [SerializeField] GameObject buyPanel;

        #endregion

        private void Awake()
        {
            if (Instance != this  && Instance != null) Destroy(this);
            else Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetStates(int value)
        {
            state = (State)value;
            SetState(state);
        }

        private void SetState(State curState)
        {
            state = curState;

            signupPanel.SetActive(state == State.signup);
            signinPanel.SetActive(state == State.signin);
            statusPanel.SetActive(state == State.status);
            inventoryPanel.SetActive(state == State.inventory);
            sellPanel.SetActive(state == State.sell);
            buyPanel.SetActive(state == State.buy);
        }
    }
}