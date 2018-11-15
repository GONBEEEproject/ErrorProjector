using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleErrorHandler : MonoBehaviour
{
    [SerializeField, Tooltip("ログ表示したいText")]
    private Text ErrorMessageText;

    private void Awake()
    {
        if (ErrorMessageText == null)
        {
            ErrorMessageText = GetComponentInChildren<Text>();
            ErrorMessageText.text = "";
        }

        Application.logMessageReceived += OnMessageRecieve;

        //StartCoroutine(Test());
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= OnMessageRecieve;
    }

    private void OnMessageRecieve(string log, string stackTrace, LogType type)
    {
        if (string.IsNullOrEmpty(log)) return;

        string outPutText = "";

        switch (type)
        {
            case LogType.Error:
                log = string.Format("<color=red>{0}</color>", log);
                stackTrace = string.Format("<color=red>{0}</color>", stackTrace);
                outPutText = log + "\n" + stackTrace + "\n";
                break;
            case LogType.Assert:
                log = string.Format("<color=white>{0}</color>", log);
                outPutText = log + "\n";
                break;
            case LogType.Exception:
                log = string.Format("<color=red>{0}</color>", log);
                stackTrace = string.Format("<color=red>{0}</color>", stackTrace);
                outPutText = log + "\n" + stackTrace + "\n";
                break;
            case LogType.Warning:
                log = string.Format("<color=yellow>{0}</color>", log);
                stackTrace = string.Format("<color=yellow>{0}</color>", stackTrace);
                outPutText = log + "\n";
                break;
            default:
                break;
        }
        ErrorMessageText.text += outPutText;
    }

    //テスト用に組んだログ吐く奴
    private IEnumerator Test()
    {
        while (true)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    Debug.LogFormat("Time:{0}", System.DateTime.Now);
                    break;
                case 1:
                    Debug.LogWarningFormat("Time:{0}", System.DateTime.Now);
                    break;
                case 2:
                    Debug.LogErrorFormat("Time:{0}", System.DateTime.Now);
                    break;

                default:
                    break;

            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
