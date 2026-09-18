using UnityEngine;

namespace ATTILA.UI
{
    /// <summary>Presentation-only references for the Main Menu. Replacing sprites does not alter navigation.</summary>
    [CreateAssetMenu(fileName = "MainMenuVisuals", menuName = "ATTILA/UI/Main Menu Visuals")]
    public sealed class MenuVisualDefinition : ScriptableObject
    {
        public Sprite background;
        public Sprite logo;
        public Sprite character;
        public Sprite khan;
        public Material characterMaterial;
    }
}
