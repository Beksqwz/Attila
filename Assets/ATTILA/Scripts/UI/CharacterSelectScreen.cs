using ATTILA.Characters;
using ATTILA.Core;
using ATTILA.Save;
using UnityEngine;
using UnityEngine.UI;
namespace ATTILA.UI
{
 public sealed class CharacterSelectScreen : MonoBehaviour
 {
  private void Start(){var c=MenuUiKit.Canvas("Character Select Canvas");MenuUiKit.Background(c.transform);MenuUiKit.Header(c.transform,"КЕЙІПКЕРДІ ТАҢДА",()=>SceneLoader.Load(SceneId.MainMenu));var v=Resources.Load<MenuVisualDefinition>("UI/MainMenuVisuals");var hero=Resources.Load<HeroDefinition>("Data/TEMP_HERO");var b=Card(c.transform,"БАТЫР","Жауынгер жолы",.32f,true);MenuUiKit.Portrait(b.transform,v?.character,v?.characterMaterial,new Vector2(.5f,.61f),new Vector2(285,390));var go=MenuPixelButton.Create(b.transform,SaveService.HasSaveFor(hero.HeroId)?"ЖАЛҒАСТЫРУ":"БАСТАУ",()=>{SaveService.SelectHero(hero.HeroId);SceneLoader.Load(SceneId.AulPrototype);});UiFactory.SetRect(go.GetComponent<RectTransform>(),new Vector2(.5f,.07f),new Vector2(250,48),Vector2.zero);var k=Card(c.transform,"ХАН","Билеуші жолы",.68f,false);MenuUiKit.Portrait(k.transform,v?.khan,v?.characterMaterial,new Vector2(.5f,.61f),new Vector2(285,390));var note=UiFactory.Text(k.transform,"ТЕК ТАҢДАУ",17,TextAnchor.MiddleCenter,MenuUiKit.Gold);UiFactory.SetRect(note.rectTransform,new Vector2(.5f,.07f),new Vector2(250,35),Vector2.zero);}
  private static Image Card(Transform root,string title,string sub,float x,bool selected){var p=MenuUiKit.Panel(root,new Vector2(x,.49f),new Vector2(520,630),selected);var n=UiFactory.Text(p.transform,title,34,TextAnchor.MiddleCenter,MenuUiKit.Gold);n.fontStyle=FontStyle.Bold;UiFactory.SetRect(n.rectTransform,new Vector2(.5f,.25f),new Vector2(420,46),Vector2.zero);var s=UiFactory.Text(p.transform,sub,19,TextAnchor.MiddleCenter,MenuUiKit.Parchment);UiFactory.SetRect(s.rectTransform,new Vector2(.5f,.18f),new Vector2(420,34),Vector2.zero);return p;}
 }
}
