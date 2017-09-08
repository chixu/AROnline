﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using System;

public class Xml
{
	public static string Attribute(XElement node, string name){
		if (node.Attribute (name) != null) {
			return node.Attribute (name).Value;
		}
		return "";
	}

	public static float Float(XElement node, string name, float def = 0){
		if (node.Attribute (name) != null) {
			return float.Parse(node.Attribute (name).Value);
		}
		return def;
	}

	public static string Version (XElement node ){
		return Attribute(node, "version");
	}

	public static XElement GetChildByAttribute (XElement node, string attr, string value){
		if (node == null)
			return null;
		var nodes = node.Elements ();
		//XElement res = null;
		foreach(XElement n in nodes){
			var a = n.Attribute (attr);
			if (a != null && a.Value == value) {
				return n;
			}
		}
		return null;
	}
}
