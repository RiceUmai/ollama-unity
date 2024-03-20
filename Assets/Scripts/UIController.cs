using UnityEngine;
using Cysharp.Threading.Tasks;

public class UIController : MonoBehaviour
{
    [SerializeField]
    UIView view;

    private void Awake()
    {
        BindView();
    }

    private void BindView()
    {
        view.PromptInputText.onValueChanged.AddListener(OnInputFieldValueChanged);
        view.SendButton.onClick.AddListener(() => SendButtonEvent().Forget());
    }

    private async UniTask SendButtonEvent()
    {
        try
        {
            view.LoadingUI.SetActive(true);
            view.SendButton.interactable = false;
            view.ResponeTextUI.text = await view.ApiManager.SendPrompt(view.PromptInputText.text);
        }
        finally
        {
            view.LoadingUI.SetActive(false);
            view.SendButton.interactable = true;
        }
    }

    private void OnInputFieldValueChanged(string newValue)
    {
        Debug.Log("New value: " + newValue);
    }
}
