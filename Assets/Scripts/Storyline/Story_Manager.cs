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

                switch(currentState)
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

        [Header("OutsideRoom Stuff")]
        [SerializeField] GameObject questTrigger;
        [SerializeField] GameObject exitTrigger;
        [SerializeField] GameObject textPanel;
        [SerializeField] GameObject textBox;
        [SerializeField] GameObject momDialogue;
        [SerializeField] GameObject playerDialogue;

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
                if(isEntering)
                {
                    yield return new WaitForSeconds(0.5f);
                    ChangeStoryState(STORY_STATE.OUTSIDEROOM);
                }

                yield return null;
            }
            yield return null;
        }
        public IEnumerator OutsideRoom()
        {
            while(currentState  == STORY_STATE.OUTSIDEROOM)
            {
                if(isEntering)
                {
                    textPanel.SetActive(true);
                    momDialogue.GetComponent<TMP_Text>().text = " Mom : ";
                    textBox.GetComponent<TMP_Text>().text = " Can you buy some tomatoes? ";
                    yield return new WaitForSeconds(1.5f);
                    momDialogue.GetComponent<TMP_Text>().text = " ";
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
                    yield return new WaitForSeconds(0.5f);
                    ChangeStoryState(STORY_STATE.TOWN);
                }
                yield return null;
            }

            yield return null;
        }

        public IEnumerator StoryTown()
        {
            yield return new WaitForSeconds(5);
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
        }
        #endregion
    }
}

