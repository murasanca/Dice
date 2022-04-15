// Murat Sancak

using System.IO;
using UnityEditor;
using UnityEngine;
using static System.Environment;
using static System.String;
using static UnityEditor.AssetDatabase;
using static UnityEditor.EditorGUI;
using static UnityEditor.EditorStyles;

namespace murasanca
{
    public class Screenshot:EditorWindow
    {
        private bool
            l = true, // l: Left.
            r = false; // r: Right.

        private GameObject c; // c: Camera.

        private GUIContent m; // m: murasanca.

        private int pu; // pu: Pop-up.

        private string
            e = "", // e: Extension.
            n = "", // n: Name.
            p = ""; // p: Path.

        private readonly string[] o = new string[3] { "Data","Desktop","Other" }; // o: Options.

        private static EditorWindow eW; // eW: Editor Window.

        private static Vector2Int
            h = new(256,128), // h: Horizontal.
            v = new(256,512); // v: Vertical.

        // Murat Sancak

        [MenuItem("Murat Sancak/Screenshot",false,0)]
        private static void S() // S: Screenshot.
        {
            eW=GetWindow<Screenshot>(true,"Screenshot by Murat Sancak",true);
            eW.maxSize=eW.minSize=h;
            eW.position=Rect.zero;
            eW.Show();
        }

        // Murat Sancak

        private void Awake() => m=new GUIContent("Screenshot by Murat Sancak",LoadAssetAtPath<Texture2D>("Assets/Murat Sancak/Sprites/Murat Sancak.png"));

        private void OnGUI()
        {
            // Camera Label Field.
            LabelField(new Rect(8,8,position.width-16,16),"Camera",centeredGreyMiniLabel);
            // Camera Object Field.
            if
            (
                (
                    c=c
                        ? ObjectField(new Rect(8,32,position.width-16,16),c,typeof(GameObject),true) as GameObject
                        : ObjectField(new Rect(8,32,position.width-16,16),Camera.main.gameObject,typeof(GameObject),true) as GameObject
                )
                is not null
                &&
                c.TryGetComponent(out Camera _)
            )
            {
                maxSize=minSize=v;

                // Path Label Field.
                LabelField(new Rect(8,56,position.width-16,16),"Path",centeredGreyMiniLabel);
                // Path Popup
                switch(pu=Popup(new Rect(8,80,position.width-16,16),pu,o,popup))
                {
                    case 0:
                        p="Assets/Murat Sancak";
                        break;
                    case 1:
                        p=GetFolderPath(SpecialFolder.Desktop);
                        break;
                    case 2:
                        // Path Text Field.
                        p=p is ""
                            ? TextArea(new Rect(8,104,position.width-16,32),Concat(Application.dataPath,"/Murat Sancak"),textArea)
                            : TextArea(new Rect(8,104,position.width-16,32),p,textArea);
                        break;
                    default:
                        break;
                }

                if(pu is 2)
                {
                    // Name Label Field.
                    LabelField(new Rect(8,144,position.width-16,16),"Name",centeredGreyMiniLabel);
                    // Name Text Field.
                    n=n is ""
                        ? TextArea(new Rect(8,168,position.width-16,16),"Screenshot",textArea)
                        : TextArea(new Rect(8,168,position.width-16,16),n,textArea);

                    // Extension Label Field.
                    LabelField(new Rect(8,192,position.width-16,16),"Extension",centeredGreyMiniLabel);
                    // Extension Text Field.
                    e=e is ""
                        ? TextArea(new Rect(8,216,position.width-16,16),"png",textArea)
                        : TextArea(new Rect(8,216,position.width-16,16),e,textArea);

                    // Eyes Label Field.
                    LabelField(new Rect(8,240,position.width-16,16),"Eyes",centeredGreyMiniLabel);
                    // Left Toggle.
                    l=ToggleLeft(new Rect(8,264,position.width/2-16,16),"Left",l,centeredGreyMiniLabel);
                    // Right Toggle.
                    r=ToggleLeft(new Rect(position.width/2+8,264,position.width/2-16,16),"Right",r,centeredGreyMiniLabel);

                    // Resolution Label Field
                    LabelField(new Rect(8,288,position.width-16,16),"Resolution",centeredGreyMiniLabel);
                    // X Label Field.
                    LabelField(new Rect(8,312,position.width/2-16,16),Concat("X:\t",c.GetComponent<Camera>().pixelWidth),centeredGreyMiniLabel);
                    // Y Label Field.
                    LabelField(new Rect(position.width/2+8,312,position.width/2-16,16),Concat("Y:\t",c.GetComponent<Camera>().pixelHeight),centeredGreyMiniLabel);

                    // Screenshot Label Field.
                    LabelField(new Rect(8,336,position.width-16,16),"Screenshot",centeredGreyMiniLabel);
                    // File Label Field.
                    LabelField(new Rect(8,360,position.width-16,32),R('/',Concat(p,'/',n,'.',e)),wordWrappedMiniLabel);
                }
                else // popup is 0 or 1.
                {
                    // Name Label Field.
                    LabelField(new Rect(8,104,position.width-16,16),"Name",centeredGreyMiniLabel);
                    // Name Text Field.
                    n=n is ""
                        ? TextArea(new Rect(8,128,position.width-16,16),"Screenshot",textArea)
                        : TextArea(new Rect(8,128,position.width-16,16),n,textArea);

                    // Extension Label Field.
                    LabelField(new Rect(8,152,position.width-16,16),"Extension",centeredGreyMiniLabel);
                    // Extension Text Field.
                    e=e is ""
                        ? TextArea(new Rect(8,176,position.width-16,16),"png",textArea)
                        : TextArea(new Rect(8,176,position.width-16,16),e,textArea);

                    // Eyes Label Field.
                    LabelField(new Rect(8,200,position.width-16,16),"Eyes",centeredGreyMiniLabel);
                    // Left Toggle.
                    l=ToggleLeft(new Rect(8,224,position.width/2-16,16),"Left",l,centeredGreyMiniLabel);
                    // Right Toggle.
                    r=ToggleLeft(new Rect(position.width/2+8,224,position.width/2-16,16),"Right",r,centeredGreyMiniLabel);

                    // Resolution Label Field
                    LabelField(new Rect(8,248,position.width-16,16),"Resolution",centeredGreyMiniLabel);
                    // X Label Field.
                    LabelField(new Rect(8,272,position.width/2-16,16),Concat("X:\t",c.GetComponent<Camera>().pixelWidth),centeredGreyMiniLabel);
                    // Y Label Field.
                    LabelField(new Rect(position.width/2+8,272,position.width/2-16,16),Concat("Y:\t",c.GetComponent<Camera>().pixelHeight),centeredGreyMiniLabel);

                    // Screenshot Label Field.
                    LabelField(new Rect(8,296,position.width-16,16),"Screenshot",centeredGreyMiniLabel);
                    // File Label Field.
                    LabelField(new Rect(8,320,position.width-16,72),R('/',Concat(p,'/',n,'.',e)),wordWrappedMiniLabel);
                }

                // Capture Screenshot Button.
                if(GUI.Button(new Rect(8,position.height-112,position.width-16,32),"Capture Screenshot"))
                    if(Directory.Exists(R('\\',p)))
                    {
                        if(l)
                            ScreenCapture.CaptureScreenshot(R('\\',Concat(p,'\\',n,'.',e)),ScreenCapture.StereoScreenCaptureMode.LeftEye);
                        else if(r)
                            ScreenCapture.CaptureScreenshot(R('\\',Concat(p,'\\',n,'.',e)),ScreenCapture.StereoScreenCaptureMode.RightEye);
                        else if(l&&r)
                            ScreenCapture.CaptureScreenshot(R('\\',Concat(p,'\\',n,'.',e)),ScreenCapture.StereoScreenCaptureMode.BothEyes);
                        else
                            ScreenCapture.CaptureScreenshot(R('\\',Concat(p,'\\',n,'.',e)),1);
                    }
                    else
                        Debug.LogWarning(Concat("Directory.Exists(",p,"):\t",Directory.Exists(R('\\',p))));
            }
            else
                maxSize=minSize=h;

            // Drop Shadow Label.
            DropShadowLabel(new Rect(8,position.height-72,position.width-16,64),m,centeredGreyMiniLabel);
        }

        private void OnInspectorUpdate()
        {
            m=new GUIContent("Screenshot by Murat Sancak",LoadAssetAtPath<Texture2D>("Assets/Murat Sancak/Sprites/Murat Sancak.png"));

            Repaint();
        }

        private void OnProjectChange() => Refresh();

        private void OnSelectionChange() => Refresh();

        // Murat Sancak

        private string R(char c,string s) => c is '/' ? s.Replace('\\','/') : s.Replace('/','\\'); // R: Replace, c: Character, s: String.
    }
}

// Murat Sancak