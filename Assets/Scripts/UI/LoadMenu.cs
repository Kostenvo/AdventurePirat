using UnityEngine;

namespace UI
{
    public static class LoadMenu
    {
        public static void Load(string menuPath)
        {
            var option = Resources.Load(menuPath);
            var canvas = GameObject.FindWithTag("Canvas");
            Object.Instantiate(option, canvas.transform);
        }
    }
}