using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ApiManager : MonoBehaviour
{
    [SerializeField]
    string apiURL;

    public async UniTask<string> SendPrompt(string massage)
    {
        PacketModelData packet = new PacketModelData()
        {
            model = "gemma:7b",
            messages = new List<PacketMassage>()
            {
                new PacketMassage()
                {
                    role = "user",
                    content = massage,
                }
            },

            stream = false,
        };

        var jsonBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(packet));

        using (var www = new UnityWebRequest(apiURL, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(jsonBytes);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Accept", " text/plain");

            await www.SendWebRequest().ToUniTask(Progress.Create<float>(n =>
            {
                Debug.Log($"is progressing : {n * 100}%");
            }));

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                return string.Empty;
            }

            ResponesModelData responesModelData = JsonConvert.DeserializeObject<ResponesModelData>(www.downloadHandler.text);
            return responesModelData.message.content;
        }
    }
}

[Serializable]
public class PacketMassage
{
    public string role;
    public string content;
}

[Serializable]
public class PacketModelData
{
    public string model;
    public List<PacketMassage> messages;
    public bool stream;
}

[Serializable]
public class ResponesMessage
{
    public string role;
    public string content;
}

[Serializable]
public class ResponesModelData
{
    public string model;
    public DateTime created_at;
    public ResponesMessage message;
    public bool done;
    public long total_duration;
    public long load_duration;
    public int prompt_eval_count;
    public long prompt_eval_duration;
    public int eval_count;
    public long eval_duration;
}