using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundController : MonoBehaviour {
	
	public GameObject grounds;
	private float screenWidthInPoints; 
	public List<GameObject> currentRooms;
	
	// Use this for initialization
	void Start () {
		float height = Camera.main.orthographicSize;
		Debug.Log ("screenWidthInPoints " + height);
		screenWidthInPoints = height * Camera.main.aspect; 
		Debug.Log ("screenWidthInPoints " + screenWidthInPoints);
	}
	
	void Update () 
	{
		GenerateRoomIfRequired();
	} 
	
	void GenerateRoomIfRequired()
	{
		List<GameObject> roomsToRemove = new List<GameObject>();
		bool addRooms = true;        
		float playerX = transform.position.x;
		float removeRoomX = playerX - screenWidthInPoints;        

		float addRoomX = playerX + screenWidthInPoints;

		float farthestRoomEndX = 0;
		
		foreach(var room in currentRooms)
		{
			float roomWidth = room.transform.FindChild("floor").localScale.x;
			float roomStartX = room.transform.position.x - (roomWidth * 0.5f);    
			float roomEndX = roomStartX + roomWidth;                            
			
			if (roomStartX > addRoomX)
				addRooms = false;
			
			if (roomEndX < removeRoomX)
				roomsToRemove.Add(room);
			
			farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
		}
		
		foreach(var room in roomsToRemove)
		{
			currentRooms.Remove(room);
			Destroy(room);            
		}
		
		if (addRooms)
			AddRoom(farthestRoomEndX);
	} 
	
	void AddRoom(float farhtestRoomEndX)
	{
		Debug.Log ("farhtestRoomEndX " + farhtestRoomEndX);
		GameObject room = (GameObject)Instantiate(grounds);
		
		float roomWidth = room.transform.FindChild("floor").localScale.x;
		Debug.Log ("roomWidth " + roomWidth);
		float roomCenter = farhtestRoomEndX + roomWidth * 0.5f;
		
		room.transform.position = new Vector3(roomCenter, -2, 0);
		
		currentRooms.Add(room);         
	} 
}
