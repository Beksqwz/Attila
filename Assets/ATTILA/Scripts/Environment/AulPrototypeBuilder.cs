using ATTILA.CameraSystem;
using ATTILA.Player;
using ATTILA.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ATTILA.Environment
{
    public sealed class AulPrototypeBuilder : MonoBehaviour
    {
        private readonly Color groundColor = new(.40f, .62f, .31f);
        private void Start()
        {
            RenderSettings.ambientLight = new Color(.85f, .85f, .85f);
            BuildGround(); BuildPath(); BuildYurts(); BuildNature(); BuildFence(); BuildNpcs(); var player = BuildPlayer(); BuildCamera(player.transform); BuildHud();
        }
        private void BuildGround() { var ground = Primitive(PrimitiveType.Plane, "Ground", Vector3.zero, new Vector3(5.5f, 1, 5.5f), groundColor); ground.AddComponent<BoxCollider>(); }
        private void BuildPath()
        {
            var path = Primitive(PrimitiveType.Cube, "Path", new Vector3(0, .025f, 0), new Vector3(3.5f, .05f, 44), new Color(.67f, .52f, .36f));
        }
        private void BuildYurts()
        {
            CreateYurt(new(-12, 0, 9)); CreateYurt(new(12, 0, 10)); CreateYurt(new(-13, 0, -9)); CreateYurt(new(13, 0, -10));
        }
        private void CreateYurt(Vector3 p)
        {
            var root = new GameObject("Placeholder Yurt"); root.transform.position = p;
            Primitive(PrimitiveType.Cylinder, "Round base", p + Vector3.up * 1f, new Vector3(3.5f, 1f, 3.5f), new Color(.82f, .73f, .58f)).transform.SetParent(root.transform);
            Primitive(PrimitiveType.Sphere, "Dome", p + Vector3.up * 2.2f, new Vector3(3.55f, 2.5f, 3.55f), new Color(.9f, .82f, .67f)).transform.SetParent(root.transform);
            Primitive(PrimitiveType.Cube, "Door placeholder", p + new Vector3(0, .9f, -3.42f), new Vector3(.9f, 1.8f, .12f), UiFactory.Primary).transform.SetParent(root.transform);
        }
        private void BuildNature()
        {
            foreach (var p in new[] { new Vector3(-20,0,16),new Vector3(19,0,17),new Vector3(-20,0,-15),new Vector3(19,0,-16),new Vector3(-5,0,18),new Vector3(7,0,-19) }) CreateTree(p);
            foreach (var p in new[] { new Vector3(-7,.3f,-5),new Vector3(7,.3f,4),new Vector3(-18,.3f,2),new Vector3(17,.3f,2) }) Primitive(PrimitiveType.Sphere,"Rock",p,new Vector3(1.1f,.6f,.9f),new Color(.42f,.40f,.35f));
        }
        private void CreateTree(Vector3 p)
        {
            Primitive(PrimitiveType.Cylinder,"Tree trunk",p + Vector3.up * 1.5f,new Vector3(.45f,1.5f,.45f),new Color(.35f,.22f,.12f));
            Primitive(PrimitiveType.Sphere,"Tree canopy",p + Vector3.up * 4f,new Vector3(3.2f,3.3f,3.2f),new Color(.20f,.48f,.20f));
        }
        private void BuildFence()
        {
            for (var x = -9; x <= 9; x += 3) { Primitive(PrimitiveType.Cube,"Fence",new Vector3(x,.7f,14),new Vector3(.18f,1.4f,.18f),new Color(.38f,.24f,.13f)); Primitive(PrimitiveType.Cube,"Fence rail",new Vector3(x,.75f,14),new Vector3(3,.12f,.12f),new Color(.38f,.24f,.13f)); }
        }
        private void BuildNpcs()
        {
            CreateNpc(new(-4, 0, 6), "NPC Placeholder 1"); CreateNpc(new(5, 0, 7), "NPC Placeholder 2"); CreateNpc(new(-7, 0, -7), "NPC Placeholder 3");
        }
        private void CreateNpc(Vector3 p, string label)
        {
            var npc = Primitive(PrimitiveType.Capsule,label,p + Vector3.up,Vector3.one,new Color(.88f,.47f,.30f)); npc.AddComponent<CharacterController>();
        }
        private GameObject BuildPlayer()
        {
            var player = Primitive(PrimitiveType.Capsule,"TEMP_HERO Player",new Vector3(0,1,0),Vector3.one,new Color(.30f,.36f,.76f));
            player.AddComponent<CharacterController>(); var visual = player.AddComponent<PlayerVisual>(); var root = new GameObject("VisualRoot"); root.transform.SetParent(player.transform); visual.GetType(); player.AddComponent<PlayerController>(); return player;
        }
        private void BuildCamera(Transform target)
        {
            var cam = new GameObject("Gameplay Camera", typeof(Camera), typeof(AudioListener)); cam.tag = "MainCamera"; var controller = cam.AddComponent<CameraController>(); controller.SetTarget(target); cam.transform.position = target.position + new Vector3(0,17,-14); cam.transform.rotation = Quaternion.Euler(52,0,0); cam.GetComponent<Camera>().backgroundColor = new Color(.35f,.68f,.90f);
            var light = new GameObject("Sun", typeof(Light)); light.GetComponent<Light>().type = LightType.Directional; light.transform.rotation = Quaternion.Euler(50,-30,0);
        }
        private void BuildHud()
        {
            var canvas = UiFactory.CreateCanvas("Aul HUD"); var label = UiFactory.Text(canvas.transform,"WASD",20,TextAnchor.MiddleCenter,UiFactory.Background); var panel=UiFactory.Panel(canvas.transform,new Color(0,0,0,.28f)); UiFactory.SetRect(panel.rectTransform,new Vector2(.5f,.08f),new Vector2(150,42),Vector2.zero); label.transform.SetParent(panel.transform,false); UiFactory.Stretch(label.rectTransform,Vector2.zero,Vector2.one,Vector2.zero,Vector2.zero);
        }
        private static GameObject Primitive(PrimitiveType type, string name, Vector3 position, Vector3 scale, Color color)
        { var go = GameObject.CreatePrimitive(type); go.name = name; go.transform.position = position; go.transform.localScale = scale; go.GetComponent<Renderer>().material.color = color; return go; }
    }
}
