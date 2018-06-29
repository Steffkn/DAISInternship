using MeTube.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MuTube.Web.Models.ViewModels
{
    public class TubeDetailsViewModel
    {
        public string Title { get; set; }

        public string YoutubeId { get; set; }

        public int Views { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public static Func<Tube, TubeDetailsViewModel> FromTube => tube => new TubeDetailsViewModel()
        {
            Title = tube.Title,
            YoutubeId = tube.YoutubeId,
            Views = tube.Views,
            Author = tube.Author,
            Description = tube.Description,
        };
    }
}
