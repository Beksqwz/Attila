using ATTILA.CameraSystem;
using ATTILA.Player;
using ATTILA.UI;
using ATTILA.Data;
using ATTILA.NPC;
using ATTILA.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace ATTILA.Environment
{
    public sealed class AulPrototypeBuilder : MonoBehaviour
    {
        private readonly Color groundColor = new(.57f, .74f, .40f);
        private void Start()
        {
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(.90f, .93f, .86f);
            RenderSettings.fog = false;
            BuildGround(); BuildPath(); BuildYurts(); BuildNature(); BuildFence(); BuildNpcs(); var player = BuildPlayer(); BuildCamera(player.transform); BuildHud();
            var stage2 = gameObject.AddComponent<Stage2Game>(); stage2.Initialize(player.GetComponent<PlayerController>());
        }
        private void BuildGround() { var ground = Primitive(PrimitiveType.Plane, "Ground", Vector3.zero, new Vector3(5.5f, 1, 5.5f), groundColor);  }
        private void BuildPath()
        {
            var path = Primitive(PrimitiveType.Cube, "Path", new Vector3(0, .025f, 0), new Vector3(3.5f, .05f, 44), new Color(.83f, .72f, .52f));
        }
        private void BuildYurts()
        {
            CreateYurt(new(-12, 0, 9)); CreateYurt(new(12, 0, 10)); CreateYurt(new(-13, 0, -9)); CreateYurt(new(13, 0, -10));
        }
        private void CreateYurt(Vector3 p)
        {
            var root = new GameObject("Placeholder Yurt"); root.transform.position = p;
            Primitive(PrimitiveType.Cylinder, "Round base", p + Vector3.up * 1f, new Vector3(3.5f, 1f, 3.5f), new Color(.86f, .79f, .64f)).transform.SetParent(root.transform);
            CreateRoof(root.transform);
            Primitive(PrimitiveType.Cube, "Door placeholder", p + new Vector3(0, .8f, -1.76f), new Vector3(.85f, 1.6f, .12f), UiFactory.Primary).transform.SetParent(root.transform);
        }
        private void BuildNature()
        {
            foreach (var p in new[] { new Vector3(-20,0,16),new Vector3(19,0,17),new Vector3(-20,0,-15),new Vector3(19,0,-16),new Vector3(-5,0,18),new Vector3(7,0,-19) }) CreateTree(p);
            foreach (var p in new[] { new Vector3(-7,.3f,-5),new Vector3(7,.3f,4),new Vector3(-18,.3f,2),new Vector3(17,.3f,2) }) Primitive(PrimitiveType.Sphere,"Rock",p,new Vector3(1.1f,.6f,.9f),new Color(.65f,.65f,.53f));
        }
        private void CreateTree(Vector3 p)
        {
            Primitive(PrimitiveType.Cylinder,"Tree trunk",p + Vector3.up * 1.5f,new Vector3(.45f,1.5f,.45f),new Color(.49f,.35f,.22f));
            Primitive(PrimitiveType.Sphere,"Tree canopy",p + Vector3.up * 4f,new Vector3(3.2f,2.6f,3.2f),new Color(.29f,.57f,.31f));
        }
        private void BuildFence()
        {
            for (var x = -9; x <= 9; x += 3) { Primitive(PrimitiveType.Cube,"Fence",new Vector3(x,.7f,14),new Vector3(.18f,1.4f,.18f),new Color(.38f,.24f,.13f)); Primitive(PrimitiveType.Cube,"Fence rail",new Vector3(x,.75f,14),new Vector3(3,.12f,.12f),new Color(.38f,.24f,.13f)); }
        }
        private void BuildNpcs()
        {
            var content = Resources.Load<Stage2Content>("Data/Stage2Content");
            CreateNpc(new(-4, 0, 6), content.Npc("NPC_Father_01"));
            CreateNpc(new(5, 0, 7), content.Npc("NPC_Children_01"));
            CreateNpc(new(-7, 0, -7), content.Npc("NPC_Neutral_01"));
        }
        private void CreateNpc(Vector3 p, NpcDefinition definition)
        {
            var npc = new GameObject(definition.id); npc.transform.position = p + Vector3.up;
            npc.AddComponent<CharacterController>(); npc.AddComponent<PlayerVisual>(); npc.AddComponent<NpcInteractable>().Configure(definition);
        }
        private GameObject BuildPlayer()
        {
            var player = new GameObject("TEMP_HERO Player"); player.transform.position = new Vector3(0,1,0);
            player.AddComponent<CharacterController>(); player.AddComponent<PlayerVisual>(); player.AddComponent<PlayerController>(); return player;
        }
        private void BuildCamera(Transform target)
        {
            var cam = new GameObject("Gameplay Camera", typeof(Camera), typeof(AudioListener)); cam.tag = "MainCamera"; var controller = cam.AddComponent<CameraController>(); controller.SetTarget(target);  cam.GetComponent<Camera>().backgroundColor = new Color(.65f,.82f,.91f);
            var light = new GameObject("Sun", typeof(Light)); var sun = light.GetComponent<Light>(); sun.type = LightType.Directional; sun.intensity = .55f; sun.shadows = LightShadows.None; light.transform.rotation = Quaternion.Euler(50,-30,0);
        }
        private void BuildHud()
        {
            var canvas = UiFactory.CreateCanvas("Aul HUD"); var label = UiFactory.Text(canvas.transform,"WASD",20,TextAnchor.MiddleCenter,UiFactory.Background); var panel=UiFactory.Panel(canvas.transform,new Color(0,0,0,.28f)); UiFactory.SetRect(panel.rectTransform,new Vector2(.5f,.08f),new Vector2(150,42),Vector2.zero); label.transform.SetParent(panel.transform,false); UiFactory.Stretch(label.rectTransform,Vector2.zero,Vector2.one,Vector2.zero,Vector2.zero);
        }

        private readonly System.Collections.Generic.Dictionary<Color, Material> materials = new();
        private Mesh roofMesh;
        private Material FlatMaterial(Color color)
        {
            if (materials.TryGetValue(color, out var material)) return material;
            material = new Material(Resources.Load<Material>("Art/PlaceholderFlat"));
            material.SetColor("_BaseColor", color);
            materials.Add(color, material);
            return material;
        }
        private void CreateRoof(Transform parent)
        {
            // A shallow, twelve-sided temporary roof; the existing round base keeps collision.
            if (roofMesh == null)
            {
                const int sides = 12;
                var vertices = new Vector3[sides + 1];
                var triangles = new int[sides * 3];
                vertices[0] = new Vector3(0f, 3.05f, 0f);
                for (int i = 0; i < sides; i++)
                {
                    float angle = i * Mathf.PI * 2f / sides;
                    vertices[i + 1] = new Vector3(Mathf.Cos(angle) * 1.9f, 1.95f, Mathf.Sin(angle) * 1.9f);
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = (i + 1) % sides + 1;
                    triangles[i * 3 + 2] = i + 1;
                }
                roofMesh = new Mesh { name = "Placeholder yurt roof", vertices = vertices, subMeshCount = 2 };
                var back = new int[18]; var front = new int[18];
                System.Array.Copy(triangles, 0, back, 0, 18);
                System.Array.Copy(triangles, 18, front, 0, 18);
                roofMesh.SetTriangles(back, 0); roofMesh.SetTriangles(front, 1);
                roofMesh.RecalculateNormals(); roofMesh.RecalculateBounds();
            }
            var roof = new GameObject("Simple placeholder roof", typeof(MeshFilter), typeof(MeshRenderer));
            roof.transform.SetParent(parent, false);
            roof.GetComponent<MeshFilter>().sharedMesh = roofMesh;
            var renderer = roof.GetComponent<MeshRenderer>();
            renderer.sharedMaterials = new[] { FlatMaterial(new Color(.97f, .89f, .73f)), FlatMaterial(new Color(.89f, .79f, .61f)) };
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
        private void OnDestroy()
        {
            foreach (var material in materials.Values) Destroy(material);
            if (roofMesh != null) Destroy(roofMesh);
        }
        private GameObject Primitive(PrimitiveType type, string name, Vector3 position, Vector3 scale, Color color)
        { var go = GameObject.CreatePrimitive(type); go.name = name; go.transform.position = position; go.transform.localScale = scale; var renderer = go.GetComponent<Renderer>(); renderer.sharedMaterial = FlatMaterial(color); renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; renderer.receiveShadows = false; return go; }
    }
}
