#Python initialize file
#Find main camera and change position.
import UnityEngine

camera = UnityEngine.Camera() #Created a null camera type object 

#Find all gameobjects on scene
allGameObjects = UnityEngine.GameObject.FindObjectsOfType(UnityEngine.GameObject) 

#Find main camera
for go in allGameObjects:
	#Write all game objects name
	UnityEngine.Debug.Log(go.ToString())
	if go.tag == "MainCamera":
		camera = go
#If we found an object tag is "MainCamera" , change position to (10,10,10) and name to "Changed the name of main camera"
if camera != None:
	camera.transform.position = UnityEngine.Vector3(10,10,10)
	camera.name = "Changed the name of main camera"

