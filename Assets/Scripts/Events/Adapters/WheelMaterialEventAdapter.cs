using UnityEngine;
using UnityEngine.Events;

public class WheelMateriallEventAdapter : MonoBehaviour
{
    [SerializeField] private GameEvent colorChangedEvent;
    [SerializeField] private EventManager eventManager;

    private void OnEnable()
    {
        if (colorChangedEvent != null)
        {
            GameEventListener listener = new GameEventListener();
            listener.gameEvent = colorChangedEvent;
            listener.response = new UnityEvent<object>();
            listener.response.AddListener(OnMaterialReceived);
            colorChangedEvent.RegisterListener(listener);
        }
        else
        {
            Debug.LogError("colorChangedEvent не инициализирован!");
        }
    }

    private void OnDisable()
    {
        if (colorChangedEvent != null)
        {
            GameEventListener listener = new GameEventListener();
            listener.gameEvent = colorChangedEvent; 
            listener.response = new UnityEvent<object>();
            listener.response.AddListener(OnMaterialReceived);
            colorChangedEvent.UnregisterListener(listener);
        }
    }

    private void OnMaterialReceived(object data)
    {
        if (eventManager != null && data != null)
        {
            if (data is Material material)
            {
                eventManager.OnWheelColorChanged(material);
            }
            else
            {
                Debug.LogError("ѕолучен некорректный тип данных: " + data.GetType().Name);
            }
        }
        else
        {
            Debug.LogError("eventManager или data не инициализированы!");
        }
    }
}