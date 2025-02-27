using UnityEngine;
using UnityEngine.Events;

public class MainCarDataEventAdapter : MonoBehaviour
{
    [SerializeField] private GameEvent carDataEvent;
    [SerializeField] private EventManager eventManager;

    private void OnEnable()
    {
        if (carDataEvent != null)
        {
            GameEventListener listener = new GameEventListener();
            listener.gameEvent = carDataEvent;
            listener.response = new UnityEvent<object>();
            listener.response.AddListener(OnCarSwapBoolReceived);
            carDataEvent.RegisterListener(listener);
        }
        else
        {
            Debug.LogError("carDataEvent не инициализирован!");
        }
    }

    private void OnDisable()
    {
        if (carDataEvent != null)
        {
            GameEventListener listener = new GameEventListener();
            listener.gameEvent = carDataEvent; 
            listener.response = new UnityEvent<object>();
            listener.response.AddListener(OnCarSwapBoolReceived);
            carDataEvent.UnregisterListener(listener);
        }
    }

    private void OnCarSwapBoolReceived(object data)
    {
        if (eventManager != null && data != null)
        {
            if (data is bool cardata)
            {
                eventManager.OnCarSelected(cardata);
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