using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.LevelEditor
{
    [Serializable]
    public class ButtonClickedEvent : UnityEvent<string> { }

    public class DialogController : MonoBehaviour
    {
        public string OperationText;
        public ButtonClickedEvent Operation;
        public Button OperationButton;
        public Text ButtonText;
        public InputField Path;

        public DialogController()
        {
            Events.instance.AddListener<DefaultPathChanged>(SetDefaultPath);
        }

        public void Start()
        {
            ButtonText.text = OperationText;
            OperationButton.onClick.AddListener(SaveAndClose);
        }

        public void OnDestroy()
        {
            OperationButton.onClick.RemoveListener(SaveAndClose);
            Events.instance.RemoveListener<DefaultPathChanged>(SetDefaultPath);
        }

        private void SetDefaultPath(DefaultPathChanged x)
        {
            Path.text = x.Path;
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
}