using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface DragAndDrop : IEventSystemHandler {
	void HandleGazeTriggerStart ();
	void HandleGazeTriggerEnd ();

}
