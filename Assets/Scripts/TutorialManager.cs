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
        //����� ������� ������ ��� �����
        if (Input.GetMouseButtonDown(0)) // �������� �� ����� ������� �� ��������!!!
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
        // ����� ������� �� ����� ��������
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // ���� ������� �� �����
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

    private void ShowTutorialStep(int stepIndex) // ������� ��������� ��������� �� ������� ���� ��� � �����
    {
        int childCount = tutorialBackground.transform.childCount;

        if (stepIndex >= 0 && stepIndex < childCount)
        {
            // �������� ��� �������� � ���������
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

        // ������������� ������� ����� ��������� �� ��������, ���� �������� ��� ���� ���������!!!
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
        // ��������� ����� � ��
        SceneManager.LoadScene(1);
    }
}
