﻿using System;
using System.Linq;
using System.Text;
using System.Web;
using IRunes.Data.Models;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Requests;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;

namespace IRunes.Controllers
{
    public class AlbumsController : BaseController
    {
        public IHttpResponse Create(IHttpRequest request)
        {
            var username = this.GetUsername(request);

            if (username == null)
            {
                return this.View("users/login");
            }

            return this.View();
        }

        public IHttpResponse CreatePost(IHttpRequest request)
        {
            var username = this.GetUsername(request);

            if (username == null)
            {
                return this.View("users/login");
            }

            string name = request.FormData["name"].ToString();
            string cover = request.FormData["cover"].ToString();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(cover))
            {
                return this.View("/albums/create");
            }

            var user = db.Users.FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                return this.View("users/login");
            }

            user.Albums.Add(new Album { Name = name, Cover = cover });
            db.SaveChanges();

            var response = All(request);
            return response;
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            var username = this.GetUsername(request);

            if (username == null)
            {
                return this.View("users/login");
            }

            var albumId = request.QueryData["id"].ToString();

            var album = this.db.Albums.Include(x => x.Tracks).FirstOrDefault(x => x.Id == albumId);
            string albumCover = HttpUtility.UrlDecode(album.Cover);

            var tracksPrice = album.Tracks.Sum(x => x.Price);
            var tracksPriceAfterDiscount = tracksPrice - (tracksPrice * 13 / 100);

            var albumData = new StringBuilder();

            albumData.Append($"<br/><img src=\"{albumCover}\" width=\"250\" height=\"250\"><br/>");
            albumData.Append($"<p class=\"text-center\"><b>Album Name: {album.Name}</b></p>");
            albumData.Append($"<p class=\"text-center\"><b>Album Price: ${tracksPriceAfterDiscount}</b></p>");

            var tracks = album.Tracks.ToArray();

            var sbTracks = new StringBuilder();

            this.ViewBag["tracks"] = "";

            if (tracks.Length > 0)
            {
                for (int i = 1; i < tracks.Length; i++)
                {
                    var track = tracks[i];
                    sbTracks.Append($"<b>&bull; {i}.</b> <a href=\"/tracks/details?id={track.Id}&albumId={albumId}\">{track.Name}</a></br>");
                }

                this.ViewBag["tracks"] = sbTracks.ToString();
            }

            this.ViewBag["albumId"] = album.Id;
            this.ViewBag["album"] = albumData.ToString();

            return this.View();
        }

        public IHttpResponse All(IHttpRequest request)
        {
            var username = this.GetUsername(request);

            if (username == null)
            {
                return this.View("users/login");
            }

            this.ViewBag["albums"] = "There are currently no albums.";

            string albumsParameters = null;

            var user = db.Users.Include(x => x.Albums).FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                return this.View("users/login");
            }

            var albums = user.Albums.ToArray();

            foreach (var album in albums)
            {
                albumsParameters += $"<a href=\"/albums/details?id={album.Id}\">{album.Name}</a></li><br/>";
            }

            if (albumsParameters != null)
            {
                this.ViewBag["albums"] = albumsParameters;
            }

            return this.View();
        }
    }
}