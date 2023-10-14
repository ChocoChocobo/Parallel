using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialBackground;
    [SerializeField] private GameObject redoTutorialPanel;
    private int currentStep = 0;

    // Start is called before the first frame update
    void Start()
    {
        ShowTutorialStep(0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // œŒÃ≈Õﬂ“‹ Õ¿ »Õœ”“ Õ¿∆¿“»ﬂ Õ¿ “≈À≈‘ŒÕ≈!!!
        {
            if (currentStep < tutorialBackground.transform.childCount)
            {
                currentStep++;
                ShowTutorialStep(currentStep);
            }
            else
            {
                RedoTutorial();
            }
        }
    }

    private void ShowTutorialStep(int stepIndex)
    {
        int childCount = tutorialBackground.transform.childCount;

        if (stepIndex >= 0 && stepIndex < childCount)
        {
            // —Í˚‚‡ÂÏ ‚ÒÂ Í‡ÚËÌÍË Ò Ó·Û˜ÂÌËÂÏ
            for (int i = 0; i < childCount; i++)
            {
                tutorialBackground.transform.GetChild(i).gameObject.SetActive(false);
            }

            tutorialBackground.transform.GetChild(stepIndex).gameObject.SetActive(true);
        }        
    }

    private void RedoTutorial()
    {
        redoTutorialPanel.SetActive(true);

        // ŒÒÚ‡Ì‡‚ÎË‚‡ÂÏ Ò˜ÂÚ˜ËÍ ¯‡„Ó‚ ÚÛÚÓË‡Î‡ Õ≈ –¿¡Œ“¿≈“, Õ¿ƒŒ — –€¬¿“‹ ‚—≈ ÿ¿√» “”“Œ–»¿À¿!!!
        currentStep = tutorialBackground.transform.childCount;
    }



    private void StartARScene()
    {
        // «‡„ÛÊ‡ÂÏ ÒˆÂÌÛ Ò ¿–
        SceneManager.LoadScene(1);
    }
}
