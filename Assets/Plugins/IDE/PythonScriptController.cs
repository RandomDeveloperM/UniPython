/* written by saika fatih */

using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
public class CreateClass4Python
{
	private string scriptName;
	static public List<string> allPythonScript;
	StreamReader readCodes;
	string line = "";
	List<string> codes;

	public void Create(string _scriptName)
	{
		// remove whitespace and minus
		string scriptName = _scriptName.Replace(" ","_");
		scriptName = scriptName.Replace("-","_");

		if (Directory.Exists("Assets/PythonScripts/") == false) 
		{
			Directory.CreateDirectory ("Assets/PythonScripts/");
		}

		string copyPath = "Assets/PythonScripts/"+_scriptName+".cs";
		string path4Python = "Assets/PythonScripts/" + _scriptName + ".py";

		Debug.Log("Creating Python Script Classfile: " + copyPath);

		if( File.Exists(copyPath) == false ){ // do not overwrite
			using (StreamWriter outfile = 
				new StreamWriter(copyPath))
			{
				outfile.WriteLine("using UnityEngine;");
				outfile.WriteLine("using System.Collections;");
				outfile.WriteLine("using System.Collections.Generic;");
				outfile.WriteLine("using System.IO;");
				outfile.WriteLine("");
				outfile.WriteLine("public class "+_scriptName+" : MonoBehaviour {");
				outfile.WriteLine(" ");
				outfile.WriteLine(" string line;");
				outfile.WriteLine(" StreamReader file ;");
				outfile.WriteLine(" List<string> codes;");
				outfile.WriteLine(" string code ;");
				outfile.WriteLine(" ");
				outfile.WriteLine(" void Start ()");
				outfile.WriteLine(" {");
				outfile.WriteLine(" \tcodes = new List<string> ();");
				outfile.WriteLine(" \tvar engine = global::UnityPython.CreateEngine();");
				outfile.WriteLine(" \tvar scope = engine.CreateScope();");
                outfile.WriteLine(" \tusing (file = new StreamReader (\"Assets/PythonScripts/" + _scriptName + ".py\")) {"); 
				outfile.WriteLine(" \twhile ((line = file.ReadLine ()) != null) "); 
				outfile.WriteLine(" \t{"); 
				outfile.WriteLine(" \t\tcodes.Add (line.ToString() + \"\\n\");"); 
				outfile.WriteLine(" \t}"); 
				outfile.WriteLine(" "); 
				outfile.WriteLine(" \tforeach (var _code in codes) "); 
				outfile.WriteLine(" \t{"); 
				outfile.WriteLine(" \t\tcode += _code;"); 
				outfile.WriteLine(" \t}"); 
				outfile.WriteLine(" \tvar source = engine.CreateScriptSourceFromString(code);"); 
				outfile.WriteLine(" \tsource.Execute(scope);"); 
				outfile.WriteLine(" }");
				outfile.WriteLine("}");         
				outfile.WriteLine(" ");
				outfile.WriteLine(" void Update ()");
				outfile.WriteLine(" {");
				outfile.WriteLine(" ");
				outfile.WriteLine(" }");
				outfile.WriteLine("}");	
			}//File written

			if (File.Exists (path4Python) == false) 
			{
				using (StreamWriter outfile = 
					       new StreamWriter (path4Python)) 
				{
					outfile.WriteLine ("#Python initialize file");
					outfile.WriteLine ("import UnityEngine");
					outfile.WriteLine ("text =" + "'" + _scriptName.ToString () + "'");
					outfile.WriteLine ("UnityEngine.Debug.Log(text)");

					if (allPythonScript == null)
						allPythonScript = new List<string> ();
					else
						allPythonScript.Add (_scriptName);
				}
			}

		}
		AssetDatabase.Refresh();
		//scriptName----.AddComponent(Type.GetType(_scriptName));
	}

	public string GetCode4IDE(string path)
	{
		string textIDE = "";
		if (File.Exists (path) == true) 
		{
			if (readCodes == null) {
                using (readCodes = new StreamReader(path))
                {
                    while ((line = readCodes.ReadLine()) != null)
                    {
                        if (codes == null)
                        {
                            codes = new List<string>();
                        }
                        codes.Add(line.ToString() + "\n");
                    }
                    foreach (var _code in codes)
                    {
                        textIDE += _code;
                        Debug.Log(_code.ToString() + textIDE.ToString());
                    }
                }
			}
			readCodes = null;
			codes.Clear ();
			return textIDE;
		}
		return "It doesn't exist like that file";
	}

	public void SetCode4IDE(string path , string text)
	{
		string[] lines = text.Split(Environment.NewLine.ToCharArray());
		if (File.Exists (path) == true) 
		{
			using (StreamWriter outfile = 
				new StreamWriter (path) )
			{
				foreach (var line in lines) 
				{
					outfile.WriteLine (line);
					//Debug.Log ("Line to writing = " + line.ToString ());
				}
			}
		}
		AssetDatabase.Refresh ();
	}
}