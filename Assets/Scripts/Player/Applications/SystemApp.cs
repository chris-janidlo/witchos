using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WitchOS
{
    public class SystemApp : MonoBehaviour
    {
        [TextArea(5, 100)]
        public string NormalSaveInformation, DemoSaveInformation;
        public string NormalSavePrompt, SaveInProgressMessage, SaveCompletedMessage;
        public string NormalDeletePrompt, ClickAgainDeletePrompt, DeleteCompletedMessage;
        public float ArtificialSaveDelay, ReturnToPreviousMessageDelay;

        public Button ManualSaveButton, DeleteSaveButton;
        public TextMeshProUGUI SaveInformationText, SaveButtonText, DeleteButtonText;
        public Image DemoSeal;

        public SaveManager SaveManager;

#if !UNITY_WEBGL
        bool deletePromptInConfirmation;
#endif // !UNITY_WEBGL

        void Start ()
        {
            SaveButtonText.text = NormalSavePrompt;
            DeleteButtonText.text = NormalDeletePrompt;

#if UNITY_WEBGL
            SaveInformationText.text = DemoSaveInformation;
#else
            SaveInformationText.text = NormalSaveInformation;

            ManualSaveButton.onClick.AddListener(() => StartCoroutine(saveAnimation()));
            DeleteSaveButton.onClick.AddListener(clickDelete);

            Destroy(DemoSeal.gameObject);
#endif // UNITY_WEBGL
        }

#if !UNITY_WEBGL
        IEnumerator saveAnimation ()
        {
            resetDeleteButtonState();

            SaveButtonText.text = SaveInProgressMessage;

            ManualSaveButton.interactable = false;
            DeleteSaveButton.interactable = false;

            SaveManager.SaveAllData();
            yield return new WaitForSeconds(ArtificialSaveDelay);

            SaveButtonText.text = SaveCompletedMessage;
            yield return new WaitForSeconds(ReturnToPreviousMessageDelay);

            SaveButtonText.text = NormalSavePrompt;

            ManualSaveButton.interactable = true;
            DeleteSaveButton.interactable = true;
        }

        void clickDelete ()
        {
            if (deletePromptInConfirmation)
            {
                SaveManager.DeleteAllSaveData();
                DeleteButtonText.text = DeleteCompletedMessage;
                DeleteSaveButton.interactable = false;
            }
            else
            {
                deletePromptInConfirmation = true;
                DeleteButtonText.text = ClickAgainDeletePrompt;
            }
        }

        void resetDeleteButtonState ()
        {
            DeleteSaveButton.interactable = true;
            deletePromptInConfirmation = false;
            DeleteButtonText.text = NormalDeletePrompt;
        }
#endif // !UNITY_WEBGL
    }
}
