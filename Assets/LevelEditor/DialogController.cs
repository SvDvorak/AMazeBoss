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
            OperationButton.onClick.AddListener(DoOperationAndClose);
        }

        public void OnDestroy()
        {
            OperationButton.onClick.RemoveListener(DoOperationAndClose);
            Events.instance.RemoveListener<DefaultPathChanged>(SetDefaultPath);
        }

        private void SetDefaultPath(DefaultPathChanged x)
        {
            Path.text = x.Path;
        }

        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
            {
                DoOperationAndClose();
            }
        }

        private void DoOperationAndClose()
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