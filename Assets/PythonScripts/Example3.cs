using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Example3 : MonoBehaviour {
 
 string line;
 StreamReader file ;
 List<string> codes;
 string code ;

 void Start()
 {
     codes = new List<string>();
     var engine = global::UnityPython.CreateEngine();
     var scope = engine.CreateScope();
     using (file = new StreamReader("Assets/PythonScripts/Example3.py"))
     {
         while ((line = file.ReadLine()) != null)
         {
             codes.Add(line.ToString() + "\n");
         }

         foreach (var _code in codes)
         {
             code += _code;
         }
         var source = engine.CreateScriptSourceFromString(code);
         source.Execute(scope);
     }
 }
 
 void Update ()
 {
 
 }
}
