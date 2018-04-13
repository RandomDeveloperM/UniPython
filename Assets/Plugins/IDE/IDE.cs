/*Written by Saika Fatih */

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

class PythonAddScript : EditorWindow
{
	string text = "NewPythonScript";
	string mes = " ";

	CreateClass4Python pythonScript = new CreateClass4Python();

	[MenuItem("PythonScript/Create a Script")]
	public static void Window()
	{
        GetWindow<PythonAddScript> ("Python Script");
	}

	void OnGUI()
	{
		GUILayout.Label ("Python Script", EditorStyles.boldLabel);

		text = EditorGUILayout.TextField ("Script Name ", text); 

		if (GUILayout.Button ("Create Script")) {
			if (text != "" && text != " " && text.Length >= 5) 
			{
				pythonScript.Create (text);
				this.Close ();
			} 
			else 
			{
				if (text.Length < 3) 
				{
					mes = "The script name must be three letters or three letters long!";
				}
			}
		}
		GUILayout.Label (mes);
	}
}

class PythonLocalIDE : EditorWindow
{
	public static List<string> pythonScripts;
	private List<string> currentScripts;
	
    string currentIDEText;
	int selected = 0;
	int control = 1;
	string[] scriptOptions ;
	CreateClass4Python getCodes = new CreateClass4Python();

	string ideText = "import UnityEngine #Initialize library";
	int x = 400,y=400;


    [MenuItem("PythonScript/IDE")]
    public static void IDE()
    {
        if (Directory.Exists("Assets/PythonScripts") == true)
        {
            var allFiles = Directory.GetFiles("Assets/PythonScripts/", "*.py");
            if (allFiles != null && allFiles.Length > 0)
                GetWindow<PythonLocalIDE>("Python Local IDE");
        }
        else
            Debug.LogWarning("Please , firstly add to the a python script (Menu-> PythonScript/Create a Script) ");
    }

	void OnGUI()
	{
		if (pythonScripts == null)
			pythonScripts = new List<string> ();
		if (currentScripts == null)
			currentScripts = new List<string> ();
		if (CreateClass4Python.allPythonScript == null)
			CreateClass4Python.allPythonScript = new List<string> ();
		
		EditorGUILayout.LabelField ("Set size of text box (x,y)", EditorStyles.boldLabel);

		EditorGUILayout.BeginHorizontal ();
		x = EditorGUILayout.IntField (x);
		y = EditorGUILayout.IntField (y);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.LabelField ("Select to script file you want change.", EditorStyles.boldLabel);

        try
        {
            FindAllScripts ("Assets/PythonScripts/");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Sorry , we didn't find to the scripts on PythonScripts Directory. Please , firstly add to the a python script (Menu-> PythonScript/Create a Script) ");
            Debug.LogError(e.ToString());
            throw e;
        }
	
		if (Directory.Exists ("Assets/PythonScripts") == false) {
			Directory.CreateDirectory ("Assets/PythonScripts/");
		} 
		else 
		{
            if (pythonScripts.Capacity != 0 && pythonScripts != null)
            {
                foreach (var pythonScript in pythonScripts)
                {
                    if (!currentScripts.Contains(pythonScript))
                    {
                        //Debug.Log ("test" + pythonScript);
                        currentScripts.Add(pythonScript);

                    }
                }
                if (currentScripts != null)
                {
                    scriptOptions = currentScripts.ToArray();
                    EditorGUILayout.BeginHorizontal();
                    selected = EditorGUILayout.Popup("Scripts", selected, scriptOptions);
                    //Debug.Log ("Selected val : " + selected.ToString ());
                    EditorGUILayout.EndHorizontal();
                }
            }
		}

		if (scriptOptions != null && scriptOptions.Length > 0 && control != selected) 
		{
			//Debug.Log("Selected that path : " + scriptOptions[selected].ToString());
			ideText = getCodes.GetCode4IDE (scriptOptions [selected]);	
			control = selected;
			//Debug.Log ("ide text " + ideText.ToString ());
		}

		EditorGUILayout.BeginHorizontal ();
		ideText = EditorGUI.TextArea (new Rect (10, 120, x, y), ideText);

		if (GUILayout.Button ("Save the Script")) 
		{
			getCodes.SetCode4IDE (scriptOptions [selected], ideText);
		}



		if (GUILayout.Button ("Quit the IDE")) 
		{
			this.Close ();
		}

	}

	public void FindAllScripts(string path)
	{
		var allFiles = Directory.GetFiles (path, "*.py");
		foreach (var file in allFiles) 
		{
			pythonScripts.Add (file);
			//Debug.Log (file);
		}
	}
}