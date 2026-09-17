using ATTILA.Data;
using ATTILA.Save;
using UnityEngine;
using UnityEngine.UI;

namespace ATTILA.UI
{
    public sealed class EncyclopediaScreen : MonoBehaviour
    {
        public static void Show(Transform root)
        {
            var host = new GameObject("Encyclopedia Screen"); host.transform.SetParent(root, false);
            host.AddComponent<EncyclopediaScreen>().Build();
        }
        private void Build()
        {
            var backdrop = UiFactory.Panel(transform, UiFactory.Background); UiFactory.Stretch(backdrop.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            var title = UiFactory.Text(transform, "Энциклопедия", 48, TextAnchor.MiddleCenter, UiFactory.Primary); UiFactory.SetRect(title.rectTransform, new Vector2(.5f, .88f), new Vector2(520, 70), Vector2.zero);
            var back = UiFactory.Button(transform, "Артқа", () => Destroy(gameObject)); UiFactory.SetRect(back.GetComponent<RectTransform>(), new Vector2(.12f, .10f), new Vector2(150, 48), Vector2.zero);
            var categories = UiFactory.Panel(transform, new Color(.86f, .79f, .70f)); UiFactory.SetRect(categories.rectTransform, new Vector2(.23f, .49f), new Vector2(235, 350), Vector2.zero);
            var categoryText = UiFactory.Text(categories.transform, "Ойындар\n\nДәстүрлер\n\nТұлғалар\n\nЗаттар", 22, TextAnchor.UpperLeft, UiFactory.Primary); UiFactory.Stretch(categoryText.rectTransform, Vector2.zero, Vector2.one, new Vector2(20, 24), new Vector2(-20, -20));
            var content = Resources.Load<Stage2Content>("Data/Stage2Content"); var entry = content.Entry("game_sokyr_teke");
            bool unlocked = SaveService.Current.unlockedEncyclopediaEntryIds.Contains(entry.id);
            var card = UiFactory.Panel(transform, Color.white); UiFactory.SetRect(card.rectTransform, new Vector2(.62f, .49f), new Vector2(590, 350), Vector2.zero);
            var image = UiFactory.Panel(card.transform, unlocked ? UiFactory.Accent : new Color(.55f, .45f, .40f)); UiFactory.SetRect(image.rectTransform, new Vector2(.20f, .63f), new Vector2(160, 160), Vector2.zero);
            var imageLabel = UiFactory.Text(image.transform, unlocked ? "□" : "?", 64, TextAnchor.MiddleCenter, UiFactory.Background); UiFactory.Stretch(imageLabel.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            var heading = UiFactory.Text(card.transform, unlocked ? entry.title : "???", 30, TextAnchor.UpperLeft, UiFactory.Primary); UiFactory.SetRect(heading.rectTransform, new Vector2(.62f, .79f), new Vector2(320, 46), Vector2.zero);
            var category = UiFactory.Text(card.transform, entry.category, 18, TextAnchor.UpperLeft, UiFactory.Accent); UiFactory.SetRect(category.rectTransform, new Vector2(.62f, .66f), new Vector2(320, 32), Vector2.zero);
            var body = UiFactory.Text(card.transform, unlocked ? entry.unlockedDescription : entry.lockedDescription, 20, TextAnchor.UpperLeft, Color.black); UiFactory.SetRect(body.rectTransform, new Vector2(.60f, .36f), new Vector2(380, 135), Vector2.zero);
        }
    }
}
