using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FYP.Storyline
{
    public class Story_OutsideRoom : MonoBehaviour
    {
        [SerializeField] GameObject questTrigger;
        [SerializeField] GameObject exitTrigger;
        [SerializeField] GameObject textBox;
        [SerializeField] GameObject playerMom;
        [SerializeField] GameObject playerGirl;

        public void LoadQuest()
        {
            StartCoroutine(FirstQuestScene());
        }

        IEnumerator FirstQuestScene()
        {
            playerMom.SetActive(true);
            textBox.GetComponent<TMP_Text>().text = " Can you buy some tomatoes? ";
            yield return new WaitForSeconds(1.5f);
            playerMom.SetActive(false);
            textBox.GetComponent<TMP_Text>().text = " ";
            yield return new WaitForSeconds(1.5f);
            playerGirl.SetActive(true);
            textBox.GetComponent<TMP_Text>().text = " Sure! ";
            yield return new WaitForSeconds(1.5f);
            textBox.GetComponent<TMP_Text>().text = " ";
            yield return new WaitForSeconds(2.5f);
            exitTrigger.SetActive(true);
            questTrigger.SetActive(false);
        }

    }
}

