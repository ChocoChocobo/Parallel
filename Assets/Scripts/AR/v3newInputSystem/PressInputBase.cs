using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PressInputBase : MonoBehaviour
{
    protected InputAction m_PressAction;

    protected virtual void Awake()
    {
        // Создание нового инпута
        m_PressAction = new InputAction("touch", binding: "<Pointer>/press");

        // Если начинается touch, вызываем функцию OnPressBegan
        m_PressAction.started += ctx =>
        {
            if (ctx.control.device is Pointer device)
            {
                OnPressBegan(device.position.ReadValue());
            }
        };

        // Если touch происходит, вызываем функцию OnPress
        m_PressAction.performed += ctx =>
        {
            if (ctx.control.device is Pointer device)
            {
                OnPress(device.position.ReadValue());
            }
        };

        // Если уже существующее нажатие остановлено или отменено, вызываем функцию OnPressCancel
        m_PressAction.canceled += _ => OnPressCancel();
    }

    protected virtual void OnEnable()
    {
        m_PressAction.Enable(); // Включение инпута
    }

    protected virtual void OnDisable()
    {
        m_PressAction.Disable(); // Выключение инпута
    }

    protected virtual void OnDestroy()
    {
        m_PressAction.Dispose(); // Уничтожение инпута
    }
    
    protected virtual void OnPress(Vector3 position) { }

    protected virtual void OnPressBegan(Vector3 position) { }

    protected virtual void OnPressCancel() { }
}
