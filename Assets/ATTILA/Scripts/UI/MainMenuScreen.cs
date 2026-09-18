using ATTILA.Core;
using UnityEngine;
using UnityEngine.UI;
namespace ATTILA.UI
{
 public sealed class MainMenuScreen:MonoBehaviour
 {
  private void Start(){var c=MenuUiKit.Canvas("Main Menu Canvas");MenuUiKit.Background(c.transform);var v=Resources.Load<MenuVisualDefinition>("UI/MainMenuVisuals");CreateLogo(c.transform,v);var s=UiFactory.Text(c.transform,"◆  Тарихты бізбен бірге сезін  ◆",25,TextAnchor.MiddleCenter,MenuUiKit.Parchment);s.raycastTarget=false;UiFactory.SetRect(s.rectTransform,new Vector2(.5f,.676f),new Vector2(720,44),Vector2.zero);Button(c.transform,"ОЙНАУ",.535f,new Vector2(370,60),()=>SceneLoader.Load(SceneId.CharacterSelect));Button(c.transform,"ГАРДЕРОБ",.445f,new Vector2(340,54),()=>WardrobeScreen.Show(c.transform));Button(c.transform,"ЭНЦИКЛОПЕДИЯ",.365f,new Vector2(340,54),()=>EncyclopediaScreen.Show(c.transform));Button(c.transform,"БАПТАУЛАР",.285f,new Vector2(340,54),()=>SettingsScreen.Show(c.transform));Button(c.transform,"ШЫҒУ",.205f,new Vector2(340,54),QuitSafely);}
  private static void CreateLogo(Transform root,MenuVisualDefinition visuals){if(visuals!=null&&visuals.logo!=null){var go=new GameObject("ATTILA Pixel Logo",typeof(Image));go.transform.SetParent(root,false);var image=go.GetComponent<Image>();image.sprite=visuals.logo;image.type=Image.Type.Simple;image.color=Color.white;image.preserveAspect=true;image.raycastTarget=false;UiFactory.SetRect(image.rectTransform,new Vector2(.5f,.815f),new Vector2(820,180),Vector2.zero);return;}var fallback=new GameObject("ATTILA Pixel Logo Fallback",typeof(PixelWordmark)).GetComponent<PixelWordmark>();fallback.transform.SetParent(root,false);fallback.color=MenuUiKit.Gold;fallback.raycastTarget=false;fallback.SetValue("ATTILA");UiFactory.SetRect(fallback.rectTransform,new Vector2(.5f,.815f),new Vector2(820,180),Vector2.zero);}
  private static void Button(Transform r,string x,float y,Vector2 z,System.Action a){var b=MenuPixelButton.Create(r,x,a);UiFactory.SetRect(b.GetComponent<RectTransform>(),new Vector2(.5f,y),z,Vector2.zero);}
  private static void QuitSafely(){
#if UNITY_EDITOR
   Debug.Log("ATTILA: Quit requested (ignored safely in the Unity Editor).");
#else
   Application.Quit();
#endif
  }
 }
}
