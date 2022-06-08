using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FYP.Storyline
{
    public class Story_Manager : MonoBehaviour
    {
        public enum STORY_STATE
        {
            HOME,
            OUTSIDEROOM,
            TOWN,
            MARIYA,
            LIBRARY,
            BOOK,
            LASTTOWN
        }

        public STORY_STATE CurrentState
        {
            get { return currentState; }

            set
            {
                currentState = value;

                StopAllCoroutines();

                switch (currentState)
                {
                    case STORY_STATE.HOME:
                        StartCoroutine(StoryHome());
                        break;

                    case STORY_STATE.OUTSIDEROOM:
                        StartCoroutine(OutsideRoom());
                        break;

                    case STORY_STATE.TOWN:
                        StartCoroutine(StoryTown());
                        break;

                    case STORY_STATE.MARIYA:
                        StartCoroutine(StoryMariya());
                        break;

                    case STORY_STATE.LIBRARY:
                        StartCoroutine(StoryLibrary());
                        break;

                    case STORY_STATE.BOOK:
                        StartCoroutine(StoryBook());
                        break;


                    case STORY_STATE.LASTTOWN:
                        StartCoroutine(LastTown());
                        break;
                }
            }
        }

        public void ChangeStoryState(STORY_STATE state)
        {
            CurrentState = state;
        }

        #region Variable

        public STORY_STATE currentState = STORY_STATE.HOME;

        Collider playerCollider = null;

        [SerializeField] bool isEntering;


        [Header("Universal Use")]
        [SerializeField] GameObject textPanel;
        [SerializeField] GameObject textBox;
        [SerializeField] GameObject playerDialogue;
        [SerializeField] GameObject npcDialogue;

        [Header("OutsideRoom Stuff")]
        [SerializeField] GameObject questTrigger;
        [SerializeField] GameObject exitTrigger;
        [SerializeField] GameObject momCharacter;


        [Header("StoryTown Stuff")]
        [SerializeField] GameObject townTrigger;
        [SerializeField] GameObject backtoroomTrigger;
        [SerializeField] GameObject findmariyaTrigger;

        void Start()
        {
            ChangeStoryState(STORY_STATE.HOME);
        }

        #endregion

        #region IEnumerator(s)

        public IEnumerator StoryHome()
        {
            while (currentState == STORY_STATE.HOME)
            {
                if (isEntering)
                {
                    ChangeStoryState(STORY_STATE.OUTSIDEROOM);

                    yield return null;
                }
                yield return null;
            }

        }
        public IEnumerator OutsideRoom()
        {
            while (currentState == STORY_STATE.OUTSIDEROOM)
            {
                textPanel.SetActive(true);
                npcDialogue.GetComponent<TMP_Text>().text = " Mom : ";
                textBox.GetComponent<TMP_Text>().text = " Can you buy some tomatoes? ";
                yield return new WaitForSeconds(1.5f);
                npcDialogue.GetComponent<TMP_Text>().text = " ";
                textBox.GetComponent<TMP_Text>().text = " ";
                yield return new WaitForSeconds(1.5f);
                playerDialogue.GetComponent<TMP_Text>().text = " You : ";
                textBox.GetComponent<TMP_Text>().text = " Sure! ";
                yield return new WaitForSeconds(1.5f);
                textPanel.SetActive(false);
                playerDialogue.GetComponent<TMP_Text>().text = " ";
                textBox.GetComponent<TMP_Text>().text = " ";
                yield return new WaitForSeconds(2.5f);
                exitTrigger.SetActive(true);
                questTrigger.SetActive(false);
                momCharacter.SetActive(false);

                break;
            }

            PlayerExit(playerCollider);

            while (currentState == STORY_STATE.OUTSIDEROOM)
            {
                if (isEntering)
                {
                    ChangeStoryState(STORY_STATE.TOWN);

                    yield return null;
                }

                yield return null;
            }
        }

        public IEnumerator StoryTown()
        {
            while (currentState == STORY_STATE.TOWN)
            {
                textPanel.SetActive(true);
                npcDialogue.GetComponent<TMP_Text>().text = " Uncle Lim : ";
                textBox.GetComponent<TMP_Text>().text = " Hi! How can I help you? ";
                yield return new WaitForSeconds(1.5f);
                npcDialogue.GetComponent<TMP_Text>().text = " ";
                textBox.GetComponent<TMP_Text>().text = " ";
                yield return new WaitForSeconds(1.5f);
                playerDialogue.GetComponent<TMP_Text>().text = " You : ";
                textBox.GetComponent<TMP_Text>().text = " Hi Uncle Lim! Can I get some tomatoes?";
                yield return new WaitForSeconds(1.5f);
                playerDialogue.GetComponent<TMP_Text>().text = " ";
                textBox.GetComponent<TMP_Text>().text = " ";
                yield return new WaitForSeconds(1.5f);
                npcDialogue.GetComponent<TMP_Text>().text = " Uncle Lim: ";
                textBox.GetComponent<TMP_Text>().text = " Sure! Later I will deliver to your house ";
                yield return new WaitForSeconds(1.5f);
                textPanel.SetActive(false);
                npcDialogue.GetComponent<TMP_Text>().text = " ";
                textBox.GetComponent<TMP_Text>().text = " ";
                yield return new WaitForSeconds(2.5f);
                townTrigger.SetActive(false);
                backtoroomTrigger.SetActive(true);
                findmariyaTrigger.SetActive(true);

                if (findmariyaTrigger == true)
                {
                    textPanel.SetActive(true);
                    playerDialogue.GetComponent<TMP_Text>().text = " You : ";
                    textBox.GetComponent<TMP_Text>().text = " Mom, already bought your tomatoes. Later Uncle Lim will deliver to you ";
                    yield return new WaitForSeconds(1.5f);
                    playerDialogue.GetComponent<TMP_Text>().text = " ";
                    textBox.GetComponent<TMP_Text>().text = " ";
                    yield return new WaitForSeconds(1.5f);
                    npcDialogue.GetComponent<TMP_Text>().text = " Mom : ";
                    textBox.GetComponent<TMP_Text>().text = " Thanks dear! Just now Mariya was looking for you. She said to find her at usual place ";
                    yield return new WaitForSeconds(1.5f);
                    npcDialogue.GetComponent<TMP_Text>().text = " ";
                    textBox.GetComponent<TMP_Text>().text = " ";
                    yield return new WaitForSeconds(1.5f);
                    backtoroomTrigger.SetActive(false);
                    textPanel.SetActive(false);
                    findmariyaTrigger.SetActive(false);
                }

                break;
            }


            PlayerExit(playerCollider);

            while (currentState == STORY_STATE.TOWN)
            {
                if (isEntering)
                {
                    ChangeStoryState(STORY_STATE.MARIYA);

                    yield return null;
                }
                yield return null;
            }
        }

        public IEnumerator StoryMariya()
        {
            yield return new WaitForSeconds(5);
        }

        public IEnumerator StoryLibrary()
        {
            yield return new WaitForSeconds(5);
        }

        public IEnumerator StoryBook()
        {
            yield return new WaitForSeconds(5);
        }

        public IEnumerator LastTown()
        {
            yield return new WaitForSeconds(5);
        }

        #endregion

        #region Function

        public void PlayerEnter(Collider value)
        {
            if (value.CompareTag("Player"))
            {
                playerCollider = value;
                isEntering = true;
            }

        }

        public void PlayerExit(Collider value)
        {
            if (value.CompareTag("Player"))
            {
                isEntering = false;
            }
        }
        #endregion
    }
}

