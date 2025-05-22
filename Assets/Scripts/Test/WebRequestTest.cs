using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebRequestTest : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private Button testButton;
    [SerializeField] private string testUrl = "https://jsonplaceholder.typicode.com/todos/1";
    [SerializeField] private Text resultText;

    void Start()
    {
        if (testButton != null)
            testButton.onClick.AddListener(StartDownload);
    }

    
    public void StartDownload()
    {
        StartCoroutine(DownloadTest());
    }

    
    private IEnumerator DownloadTest()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(testUrl))
        {
            yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
        bool isError = request.result == UnityWebRequest.Result.ConnectionError || 
                      request.result == UnityWebRequest.Result.ProtocolError;
#else
            bool isError = request.isNetworkError || request.isHttpError;
#endif

            if (resultText != null)
            {
                if (isError)
                {
                    resultText.text = $"<color=red>⚠️ Error\n{request.error}</color>";
                }
                else
                {
                    string prettyJson = JsonUtility.ToJson(
                        JsonUtility.FromJson<JsonObject>(request.downloadHandler.text),
                        true
                    );
                    resultText.text = $"<color=green>✅ API Response:</color>\n{prettyJson}";
                }
            }
        }
    }

    // Helper method (can remain private)
    private string FormatJson(string json)
    {
        return json
            .Replace(",", ",\n")
            .Replace("{", "{\n")
            .Replace("}", "\n}");
    }

    [System.Serializable]
public class JsonObject
    {
        public int userId;
        public int id;
        public string title;
        public bool completed;
    }

    void OnDestroy()
    {
        if (testButton != null)
            testButton.onClick.RemoveListener(StartDownload);
    }
}