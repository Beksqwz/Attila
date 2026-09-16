using ATTILA.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ATTILA.UI
{
    public sealed class MainMenuScreen : MonoBehaviour
    {
        private void Start()
        {
            var canvas = UiFactory.CreateCanvas();
            var background = UiFactory.Panel(canvas.transform, UiFactory.Background); UiFactory.Stretch(background.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            var title = UiFactory.Text(canvas.transform, "ATTILA", 76, TextAnchor.MiddleCenter, UiFactory.Primary); UiFactory.SetRect(title.rectTransform, new Vector2(.5f, .74f), new Vector2(600, 110), Vector2.zero);
            var sub = UiFactory.Text(canvas.transform, "", 20, TextAnchor.MiddleCenter, UiFactory.Accent); UiFactory.SetRect(sub.rectTransform, new Vector2(.5f, .64f), new Vector2(550, 40), Vector2.zero);
            CreateButton(canvas.transform, "Ойнау", 0, () => SceneLoader.Load(SceneId.CharacterSelect), true);
            CreateButton(canvas.transform, "Энциклопедия", 1, null, false);
            CreateButton(canvas.transform, "Киім", 2, null, false);
            CreateButton(canvas.transform, "Баптаулар", 3, null, false);
        }
        private static void CreateButton(Transform root, string label, int index, System.Action action, bool enabled)
        { var button = UiFactory.Button(root, label, action, enabled); UiFactory.SetRect(button.GetComponent<RectTransform>(), new Vector2(.5f, .45f), new Vector2(330, 58), new Vector2(0, -index * 72)); }
    }
}
