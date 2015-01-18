﻿using System;
using CookComputing.XmlRpc;

namespace WordPressSharp.Models
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class Post
    {
        [XmlRpcMember("post_id")]
        public string Id { get; set; }

        [XmlRpcMember("post_type")]
        public string PostType { get; set; }

        [XmlRpcMember("post_title")]
        public string Title { get; set; }

        [XmlRpcMember("post_status")]
        public string Status { get; set; }

        [XmlRpcMember("post_date")]
        public DateTime PublishDateTime { get; set; }

        [XmlRpcMember("post_content")]
        public string Content { get; set; }

        [XmlRpcMember("post_author")]
        public string Author { get; set; }
        
        [XmlRpcMember("post_name")]
        public string Name { get; set; }

        [XmlRpcMember("link")]
        public string Link { get; set; }

        [XmlRpcMember("terms")]
        public Term[] Terms { get; set; }

        [XmlRpcMember("custom_fields")]
        public CustomField[] CustomFields { get; set; }

        [XmlRpcMember("enclosure")]
        public Enclosure Enclosure { get; set; }

        [XmlRpcMember("media_items")]
        public MediaItem[] MediaItems { get; set; }

        [XmlRpcMember("post_parent")]
        public string ParentId { get; set; }
        [XmlRpcMember("post_thumbnail")]
        public string FeaturedImageId { get; set; }
    }
}
