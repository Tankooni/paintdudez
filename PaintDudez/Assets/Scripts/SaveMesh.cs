using UnityEngine;
using UnityEditor;
 
class SaveMesh
{
 
	[MenuItem("Assets/Save Mesh")]
	static void Create()
	{     
	    string filePath = 
	        EditorUtility.SaveFilePanelInProject("Save Procedural Mesh", "Procedural Mesh", "asset", "");
	    if (filePath == "") return;
	    AssetDatabase.CreateAsset(new Mesh(), filePath);    
	}
}