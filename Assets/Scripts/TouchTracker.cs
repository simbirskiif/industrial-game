using Unity.VisualScripting;
using UnityEngine;

public class TouchTracker : MonoBehaviour
{
    [Header("Sensitivity settings - Настр. чувствительности")]
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float scaleSensitivity = 1f;
    [SerializeField] private float rotateSensitivity = 1f;
    [Header("Scale settings - Настр. масштабирования\nNOT NEGATIVE - НЕ ОТРИЦАТЕЛЬНЫЕ")]
    [SerializeField] private float minScale = 10f;
    [SerializeField] private float maxScale = 75f;
    [Header("Target Objects - Целевые объекты,\nнеобходимые для перемещения камеры")]
    public GameObject cameraTargetPosition;
    public GameObject cameraTargetRotation;
    public GameObject cameraTargetScale;
    private float targetPosition;
    private float targetRotation;
    private float targetScale;
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 60, 100, 20), $"targetRotation: {targetRotation}", style);
        GUI.Label(new Rect(10, 85, 100, 20), $"targetScale: {targetScale}", style);
        GUI.Label(new Rect(10, 110, 100, 20), $"targetPosition: {targetPosition}", style);
    }

    void Update()
    {
        // Пример использования функций
        Vector2 singleFingerDelta = GetSingleFingerDelta();
        if (singleFingerDelta != Vector2.zero)
        {
            targetPosition = singleFingerDelta.magnitude;
            float rotationY = cameraTargetRotation.transform.eulerAngles.y;

            // Преобразуем вектор перемещения с учетом угла поворота
            Vector3 deltaMovement = new Vector3(singleFingerDelta.x, 0, singleFingerDelta.y);
            Quaternion rotation = Quaternion.Euler(0, rotationY, 0); // Создаем поворот вокруг оси Y
            deltaMovement = rotation * deltaMovement; // Поворачиваем вектор

            // Применяем перемещение
            cameraTargetPosition.transform.position += deltaMovement * sensitivity * Time.deltaTime;
            //Debug.Log($"Перемещение одного пальца: {singleFingerDelta}");
        }


        float twoFingerDistanceDelta = GetTwoFingerDistanceDelta();
        if (Mathf.Abs(twoFingerDistanceDelta) > 0.01f) // Игнорируем слишком маленькие изменения
        {
            cameraTargetScale.transform.localPosition += new Vector3(0, 0, twoFingerDistanceDelta) * scaleSensitivity * Time.deltaTime;
            targetScale = twoFingerDistanceDelta;
        }
        float thisScale = cameraTargetScale.transform.localPosition.z;
        if (thisScale > -minScale)
        {
            cameraTargetScale.transform.localPosition = new Vector3(0, 0, Mathf.Lerp(thisScale, -minScale, 0.1f));
        }
        else if (thisScale > -(minScale - 2))
        {
            cameraTargetScale.transform.localPosition = new Vector3(0, 0, Mathf.Lerp(thisScale, -10, 10f));
        }
        else if (thisScale > -1)
        {
            cameraTargetScale.transform.localPosition = new Vector3(0, 0, -1);
        }
        if (thisScale < -maxScale)
        {
            cameraTargetScale.transform.localPosition = new Vector3(0, 0, Mathf.Lerp(thisScale, -maxScale, 0.1f));
        }



        float rotationDelta = GetTwoFingerRotationDelta();

        if (Mathf.Abs(rotationDelta) > 0.01f) // Игнорируем слишком маленькие изменения
        {
            targetRotation = rotationDelta;
            cameraTargetRotation.transform.Rotate(Vector3.up, rotationDelta * rotateSensitivity, 0);
            // Debug.Log($"Изменение угла между двумя пальцами: {rotationDelta} градусов");
        }
    }
    public Vector2 GetSingleFingerDelta()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // Убедимся, что палец перемещается
            if (touch.phase == TouchPhase.Moved)
            {
                return touch.deltaPosition; // Разница в позициях между кадрами
            }
        }

        return Vector2.zero;
    }


    // Разница расстояний между двумя пальцами
    public float GetTwoFingerDistanceDelta()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Позиции пальцев в предыдущем кадре
            Vector2 prevPos0 = touch0.position - touch0.deltaPosition;
            Vector2 prevPos1 = touch1.position - touch1.deltaPosition;

            // Расстояние между пальцами в текущем и предыдущем кадрах
            float currentDistance = Vector2.Distance(touch0.position, touch1.position);
            float previousDistance = Vector2.Distance(prevPos0, prevPos1);

            return currentDistance - previousDistance;
        }

        return 0f;
    }
    public float GetTwoFingerRotationDelta()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Позиции пальцев в текущем кадре
            Vector2 currentVector = touch1.position - touch0.position;

            // Позиции пальцев в предыдущем кадре
            Vector2 prevPos0 = touch0.position - touch0.deltaPosition;
            Vector2 prevPos1 = touch1.position - touch1.deltaPosition;
            Vector2 previousVector = prevPos1 - prevPos0;

            // Угол между текущим и предыдущим векторами
            float angle = Vector2.SignedAngle(previousVector, currentVector);

            return angle; // Возвращаем разницу углов
        }

        return 0f;
    }

}
