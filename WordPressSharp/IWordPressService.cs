﻿using System;
using CookComputing.XmlRpc;
using WordPressSharp.Models;

namespace WordPressSharp
{
    public interface IWordPressService : IXmlRpcProxy
    {
        // GET
        [XmlRpcMethod("wp.getPost")]
        Post GetPost(int blog_id, string username, string password, int post_id);

        [XmlRpcMethod("wp.getPosts")]
        Post[] GetPosts(int blog_id, string username, string password, PostFilter postFilter);

        [XmlRpcMethod("wp.getMediaItem")]
        MediaItem GetMediaItem(int blog_id, string username, string password, int attachment_id);

        [XmlRpcMethod("wp.getMediaLibrary")]
        MediaItem[] GetMediaLibrary(int blog_id, string username, string password, MediaFilter filter);

        [XmlRpcMethod("wp.getTaxonomy")]
        Taxonomy GetTaxonomy(int blog_id, string username, string password, string taxonomy, int term_id);

        [XmlRpcMethod("wp.getTaxonomies")]
        Taxonomy[] GetTaxonomies(int blog_id, string username, string password, string taxonomy, TermFilter filter);

        [XmlRpcMethod("wp.getTerm")]
        Term GetTerm(int blog_id, string username, string password, string taxonomy, int term_id);

        [XmlRpcMethod("wp.getTerms")]
        Term[] GetTerms(int blog_id, string username, string password, string taxonomy, TermFilter filter);

        [XmlRpcMethod("wp.getUser")]
        User GetUser(int blog_id, string username, string password, int user_id);

        [XmlRpcMethod("wp.getUsers")]
        User[] GetUsers(int blog_id, string username, string password, UserFilter filter);

        [XmlRpcMethod("wp.getComment")]
        Comment GetComment(int blog_id, string username, string password, int comment_id);

        [XmlRpcMethod("wp.getComments")]
        Comment[] GetComments(int blog_id, string username, string password, CommentFilter filter);

        [XmlRpcMethod("wp.getCommentCount")]
        PostCommentCount GetCommentCount(int blog_id, string username, string password, int post_id);

        [XmlRpcMethod("wp.getOptions")]
        XmlRpcStruct GetOptions(int blog_id, string username, string password, string[] options);

        [XmlRpcMethod("wp.getOptions")]
        XmlRpcStruct GetAllOptions(int blog_id, string username, string password);
        

        // PUT
        [XmlRpcMethod("wp.newPost")]
        string NewPost(int blog_id, string username, string password, Post_Put post);
        [XmlRpcMethod("wp.editPost")]
        bool EditPost(int blog_id, string username, string password, int post_id, Post_Put post);

        [XmlRpcMethod("wp.uploadFile")]
        UploadResult UploadMedia(int blog_id, string username, string password, Data upload);

        [XmlRpcMethod("wp.newTerm")]
        string NewTerm(int blog_id, string username, string password, Term term);


        [XmlRpcMethod("wp.setOptions")]
        XmlRpcStruct SetOptios(int blog_id, string username, string password, XmlRpcStruct options);



        // DELETE
        [XmlRpcMethod("wp.deleteTerm")]
        bool DeleteTerm(int blog_id, string username, string password, string taxonomy, int term_id);

        [XmlRpcMethod("wp.deletePost")]
        bool DeletePost(int blog_id, string username, string password, int post_id);
    }
}
