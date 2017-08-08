using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITracker {

	void Initialize();
	void TrackEvent (string name, Dictionary<string, object> data);
}
