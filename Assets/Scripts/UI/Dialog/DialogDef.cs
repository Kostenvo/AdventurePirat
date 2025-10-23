using UI.Dialoge;
using UnityEngine;

namespace UI.Dialog
{
    [CreateAssetMenu(menuName = "Dialog/DialogDef", fileName = "CurrentDialog")]
    public class DialogDef : ScriptableObject
    {
        [SerializeField] public DialogData _dialogData;
    }
}