using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class ButtonClickedEvent : UnityEvent<string> { }

public class DialogController : MonoBehaviour
{
    public string OperationText;
    public ButtonClickedEvent Operation;
    public Button OperationButton;
    public Text ButtonText;
    public InputField Path;

    public void Start()
    {
        ButtonText.text = OperationText;
        OperationButton.onClick.AddListener(SaveAndClose);
    }

    private void SaveAndClose()
    {
        Operation.Invoke(Path.text);
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}