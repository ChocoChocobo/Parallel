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
    /*private void Update()
    {
        //Инпут нажатия мышкой для теста
        if (Input.GetMouseButtonDown(0)) // ПОМЕНЯТЬ НА ИНПУТ НАЖАТИЯ НА ТЕЛЕФОНЕ!!!
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
    }*/

    private void Update()
    {
        // Инпут нажатия на экран телефона
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Одно нажатие на экран
            if (touch.phase == TouchPhase.Began)
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
    }

    private void ShowTutorialStep(int stepIndex) // Сделать появление туториала по каждому шагу как в фигме
    {
        int childCount = tutorialBackground.transform.childCount;

        if (stepIndex >= 0 && stepIndex < childCount)
        {
            // Скрываем все картинки с обучением
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

        // Останавливаем счетчик шагов туториала НЕ РАБОТАЕТ, НАДО СКРЫВАТЬ вСЕ ШАГИ ТУТОРИАЛА!!!
        currentStep = tutorialBackground.transform.childCount;
    }

    public void OnRedoYesButtonClicked()
    {
        redoTutorialPanel.SetActive(false);

        currentStep = 0;
        ShowTutorialStep(currentStep);
    }

    public void OnRedoNoButtonClicked()
    {
        StartARScene();
    }

    private void StartARScene()
    {
        // Загружаем сцену с АР
        SceneManager.LoadScene(1);
    }
}
