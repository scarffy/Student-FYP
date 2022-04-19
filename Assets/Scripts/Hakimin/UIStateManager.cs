using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            single,
            multiplayer,
            signup,
            signin,
            status,
            inventory,
            sell,
            buy,
            quit,
        }
        #endregion

        #region private variable
        [Header("Buttons")]
        [SerializeField] Button signupButton;
        [SerializeField] Button signinButton;
        [SerializeField] Button statusButton;
        [SerializeField] Button quitButton;
        [SerializeField] Button settingsButton;
        [SerializeField] List<Button> closeButton = new List<Button>();

        [Header("Panels")]
        [SerializeField] GameObject mainMultiplayer;
        [SerializeField] GameObject signupPanel;
        [SerializeField] GameObject signinPanel;
        [SerializeField] GameObject statusPanel;
        [SerializeField] GameObject inventoryPanel;
        [SerializeField] GameObject sellPanel;
        [SerializeField] GameObject buyPanel;
        [SerializeField] GameObject quitPanel;
        [SerializeField] GameObject settingsPanel;

        #endregion

        private void Awake()
        {
            if (Instance != this  && Instance != null) Destroy(this);
            else Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            signupButton.onClick.AddListener(() => { SetStates(1); });
            signinButton.onClick.AddListener(() => { SetStates(2); });
            for (int i = 0; i < closeButton.Count; i++)
            {
                closeButton[i].onClick.AddListener(() => { SetStates(0); });
            }
        }

        public void SetStates(int value)
        {
            state = (State)value;
            SetState(state);
        }

        private void SetState(State curState)
        {
            state = curState;

            mainMultiplayer.SetActive(state == State.multiplayer);
            signupPanel.SetActive(state == State.signup);
            signinPanel.SetActive(state == State.signin);
            statusPanel.SetActive(state == State.status);
            inventoryPanel.SetActive(state == State.inventory);
            sellPanel.SetActive(state == State.sell);
            buyPanel.SetActive(state == State.buy);
            quitPanel.SetActive(state == State.quit);
        }
    }
}