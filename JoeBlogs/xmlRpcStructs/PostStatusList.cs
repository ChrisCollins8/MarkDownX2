﻿using System;
using CookComputing.XmlRpc;

namespace JoeBlogs
{
	/// <summary>
	/// A comment on a blog item
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcPostStatusList
	{
		public string Status;
	}
}