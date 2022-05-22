using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FYP.Storyline
{
    public class Story_OutsideRoom : Singleton<Story_OutsideRoom>
    {
        [SerializeField] GameObject questTrigger;
        [SerializeField] GameObject exitTrigger;
        [SerializeField] GameObject textPanel;
        [SerializeField] GameObject textBox;
        [SerializeField] GameObject momDialogue;
        [SerializeField] GameObject playerDialogue;

        public void LoadQuest_1()
        {
            StartCoroutine(FirstQuestScene());
        }

        IEnumerator FirstQuestScene()
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
        }

    }
}

