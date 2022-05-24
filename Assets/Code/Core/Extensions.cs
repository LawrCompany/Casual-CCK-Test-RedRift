using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;


namespace Code.Core{
    public static class Extensions{
        public static async Task<Texture2D> GetRemoteTexture(string url){
            using var www = UnityWebRequestTexture.GetTexture(url);
            var asyncOp = www.SendWebRequest();
            while (asyncOp.isDone == false)
                await Task.Delay(1000 / 30);
            if (www.result != UnityWebRequest.Result.Success){
#if DEBUG
                Debug.Log($"{www.error}, URL:{www.url}");
#endif
                return null;
            } else{
                return DownloadHandlerTexture.GetContent(www);
            }
        }
    }
}