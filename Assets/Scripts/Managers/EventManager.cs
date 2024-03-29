﻿using System;
using System.Collections.Generic;

public class EventManager {

	private Dictionary<string, List<Action<object>>> _events = new Dictionary<string, List<Action<object>>>();

	private static EventManager _instance;
	public static EventManager Instance {
		get {
			if (_instance == null) {
				_instance = new EventManager();
			}
			return _instance;
		}
	}

	public void On(string eventName, Action<object> callback) {
		if (_events.ContainsKey(eventName)) {
			_events[eventName].Add(callback);
			return;
		}
		_events.Add(eventName, new List<Action<object>>() { callback });
	}

	public void Stop(string eventName, Action<object> callback) {
		List<Action<object>> list;
		if (_events.TryGetValue(eventName, out list)) {
			list.Remove(callback);
		}
	}

	public void Trigger(string eventName, object args) {
		List<Action<object>> list;
		if (_events.TryGetValue(eventName, out list)) {
			list.ForEach(e => e(args));
		}
	}

}