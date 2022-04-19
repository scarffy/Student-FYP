using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FYP.UI
{
    public class GenderSelection : MonoBehaviour
    {
        public static GenderSelection Instance { get; private set; }

        [SerializeField] GameObject canvasParent;
        [SerializeField] CanvasGroup cg;
        [SerializeField] TextMeshProUGUI questionTexts;

        [Header("Gender selection panel")]
        [SerializeField] GameObject genderPanel;
        [SerializeField] Button boyButton;
        [SerializeField] Button girlButton;
        [SerializeField] bool isBoy;

        [Header("Conversation")]
        [SerializeField] string[] conversations;
        [SerializeField] Button nextButton;
        [SerializeField] bool isAnswered = false;
        [SerializeField] int questionIndex = 0;

        public AnimationCurve animationCurve;
        public float fadingSpeed = 2.5f;
        public enum Direction { FadeIn, FadeOut };

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            boyButton.onClick.AddListener(() => SelectGender(true));
            girlButton.onClick.AddListener(() => SelectGender(false));
            nextButton.onClick.AddListener(() => NextQuestion());
            questionTexts.text = conversations[questionIndex];
            genderPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SelectGender(bool isBoy)
        {
            this.isBoy = isBoy;
            genderPanel.SetActive(false);
            isAnswered = true;
        }

        void NextQuestion()
        {
            if (questionIndex < conversations.Length - 1)
            {
                if (questionIndex < 3)
                {
                    questionIndex++;
                    questionTexts.text = conversations[questionIndex];
                }
                else if (isAnswered)
                {
                    questionIndex++;
                    questionTexts.text = conversations[questionIndex];
                }
                    if(questionIndex == 3)
                    {
                        genderPanel.SetActive(true);
                    }
            }
            else
            {
                StartCoroutine(FadeCanvas(cg, Direction.FadeOut, fadingSpeed));
                UIStateManager.Instance.SetState(UIStateManager.State.single);
            }
        }

        public IEnumerator FadeCanvas(CanvasGroup canvasGroup, Direction direction, float duration)
        {
            canvasGroup.interactable = false;
            // keep track of when the fading started, when it should finish, and how long it has been running
            var startTime = Time.time;
            var endTime = Time.time + duration;
            var elapsedTime = 0f;

            // set the canvas to the start alpha – this ensures that the canvas is ‘reset’ if you fade it multiple times
            if (direction == Direction.FadeIn) canvasGroup.alpha = animationCurve.Evaluate(0f);
            else canvasGroup.alpha = animationCurve.Evaluate(1f);

            // loop repeatedly until the previously calculated end time
            while (Time.time <= endTime)
            {
                elapsedTime = Time.time - startTime; // update the elapsed time
                var percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are
                if ((direction == Direction.FadeOut)) // if we are fading out
                {
                    canvasGroup.alpha = animationCurve.Evaluate(1f - percentage);
                }
                else // if we are fading in/up
                {
                    canvasGroup.alpha = animationCurve.Evaluate(percentage);
                }

                yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
            }

            // force the alpha to the end alpha before finishing – this is here to mitigate any rounding errors, e.g. leaving the alpha at 0.01 instead of 0
            if (direction == Direction.FadeIn) canvasGroup.alpha = animationCurve.Evaluate(1f);
            else canvasGroup.alpha = animationCurve.Evaluate(0f);
        }
    }
}