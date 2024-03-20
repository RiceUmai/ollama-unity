using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[Serializable]
public class UIView
{
    [SerializeField]
    private string apiURL;
    private string ApiURL => apiURL;

    [SerializeField]
    private TMP_InputField promptInputText;
    public TMP_InputField PromptInputText => promptInputText;

    [SerializeField]
    private Button sendButton;
    public Button SendButton => sendButton;

    [SerializeField]
    private ApiManager apiManager;
    public ApiManager ApiManager => apiManager;

    [SerializeField]
    private TMP_Text responeTextUI;
    public TMP_Text ResponeTextUI => responeTextUI;

    [SerializeField]
    private GameObject loadingUI;
    public GameObject LoadingUI => loadingUI;
}
