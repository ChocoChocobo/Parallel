using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARRaycastManager))]
public class PressOnPlane : PressInputBase
{
    [SerializeField] GameObject placedPrefab; // Префаб объекта, который поставить

    GameObject spawnedObject; // Установленный объект

    bool isPressed; // Регистрация инпута нажатия

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    protected override void Awake()
    {
        base.Awake(); // Вызываем Awake от наследованного класса
        aRRaycastManager = GetComponent<ARRaycastManager>(); // Инициализируем ARRaycastManager
    }

    private void Update()
    {
        // Проверяем наличие Pointer девайса (стилус, мышь, нажатие пальчиком)
        // Или есть ли уже инпут нажатия
        if (Pointer.current == null || isPressed == false) return;

        // Сохраняем положение нажатия
        var touchPosition = Pointer.current.position.ReadValue();

        // Проверяем попадают ли лучи по плэйнам
        if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // Поскольку попадания луча сортируются по расстоянию, первое попадание-ближайшее
            var hitPose = hits[0].pose;

            // Проверяем есть ли уже установленный объект. Если объекта нет, ставим префаб
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            }
            else // ЗДЕСЬ ПРОПИСАТЬ ЛОГИКУ УБИРАНИЯ ОБЪЕКТА ИЗ СЦЕНЫ!!!
            {
                // Изменяем установленный объект, его позицию и вращение относительно нажатия
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }

            /*// Чтобы объект смотрел в камеру
            Vector3 lookPosition = Camera.main.transform.position - spawnedObject.transform.position;
            lookPosition.y = 0;
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPosition);*/
        }
    }

    protected override void OnPress(Vector3 position) => isPressed = true;

    protected override void OnPressCancel() => isPressed = false;
}
