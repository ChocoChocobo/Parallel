using System.Collections;
using System.Collections.Generic;
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

    // Появление обучения по каждому шагу
    private void ShowTutorialStep(int stepIndex) 
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

    // Запуск обучения с начала
    private void RedoTutorial()
    {
        redoTutorialPanel.SetActive(true);

        currentStep = tutorialBackground.transform.childCount;
    }

    // Метод для кнопки да после прохождения обучения
    public void OnRedoYesButtonClicked()
    {
        redoTutorialPanel.SetActive(false);

        currentStep = 0;
        ShowTutorialStep(currentStep);
    }

    // Метод для кнопки нет после прохождения обучения
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
