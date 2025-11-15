using UnityEngine;

namespace UI
{
    public class ShowWindowComponent :MonoBehaviour
    {
        public void ShowWindow(string menuPath)
        {
            var option = Resources.Load(menuPath);
            var canvas = GameObject.FindWithTag("Canvas");
            Object.Instantiate(option, canvas.transform);
        }
    }
}