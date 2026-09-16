using ATTILA.Characters;
using ATTILA.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ATTILA.UI
{
    public sealed class CharacterSelectScreen : MonoBehaviour
    {
        private void Start()
        {
            var canvas = UiFactory.CreateCanvas();
            var background = UiFactory.Panel(canvas.transform, UiFactory.Background); UiFactory.Stretch(background.rectTransform, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero);
            var title = UiFactory.Text(canvas.transform, "ATTILA", 48, TextAnchor.MiddleCenter, UiFactory.Primary); UiFactory.SetRect(title.rectTransform, new Vector2(.5f, .88f), new Vector2(500, 70), Vector2.zero);
            var hero = Resources.Load<HeroDefinition>("Data/TEMP_HERO");
            CreateHeroCard(canvas.transform, hero, new Vector2(.5f, .48f));
            CreateLockedCard(canvas.transform, new Vector2(.20f, .48f));
            CreateLockedCard(canvas.transform, new Vector2(.80f, .48f));
            var back = UiFactory.Button(canvas.transform, "Артқа", () => SceneLoader.Load(SceneId.MainMenu)); UiFactory.SetRect(back.GetComponent<RectTransform>(), new Vector2(.12f, .1f), new Vector2(150, 48), Vector2.zero);
        }
        private static void CreateHeroCard(Transform root, HeroDefinition hero, Vector2 anchor)
        {
            var card = UiFactory.Panel(root, Color.white); card.name = "TEMP_HERO Card"; UiFactory.SetRect(card.rectTransform, anchor, new Vector2(300, 420), Vector2.zero);
            var portrait = UiFactory.Panel(card.transform, new Color(.76f, .54f, .40f)); UiFactory.SetRect(portrait.rectTransform, new Vector2(.5f, .70f), new Vector2(238, 240), Vector2.zero);
            var bust = GameObject.CreatePrimitive(PrimitiveType.Capsule); bust.name = "Neutral Placeholder Portrait"; bust.transform.SetParent(portrait.transform, false); bust.transform.localScale = new Vector3(.48f, .62f, .48f); bust.transform.localPosition = new Vector3(0, -.12f, 0); Object.Destroy(bust.GetComponent<Collider>());
            var name = UiFactory.Text(card.transform, hero != null ? hero.DisplayName : "Белгісіз кейіпкер", 25, TextAnchor.MiddleCenter, UiFactory.Primary); UiFactory.SetRect(name.rectTransform, new Vector2(.5f, .34f), new Vector2(260, 36), Vector2.zero);
            var desc = UiFactory.Text(card.transform, hero != null ? hero.Description : "Уақытша кейіпкер орны", 16, TextAnchor.MiddleCenter, Color.gray); UiFactory.SetRect(desc.rectTransform, new Vector2(.5f, .25f), new Vector2(250, 45), Vector2.zero);
            var progress = UiFactory.Text(card.transform, "0%", 18, TextAnchor.MiddleCenter, UiFactory.Accent); UiFactory.SetRect(progress.rectTransform, new Vector2(.5f, .16f), new Vector2(220, 30), Vector2.zero);
            var start = UiFactory.Button(card.transform, "Бастау", () => SceneLoader.Load(SceneId.AulPrototype)); UiFactory.SetRect(start.GetComponent<RectTransform>(), new Vector2(.5f, .07f), new Vector2(220, 46), Vector2.zero);
        }
        private static void CreateLockedCard(Transform root, Vector2 anchor)
        {
            var card = UiFactory.Panel(root, new Color(.82f, .75f, .68f)); UiFactory.SetRect(card.rectTransform, anchor, new Vector2(210, 300), Vector2.zero);
            var text = UiFactory.Text(card.transform, "🔒\nЖақында", 24, TextAnchor.MiddleCenter, UiFactory.Primary); UiFactory.Stretch(text.rectTransform, Vector2.zero, Vector2.one, new Vector2(12, 12), new Vector2(-12, -12));
        }
    }
}
