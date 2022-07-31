using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DTO
{
    public class ImageUser
    {
        public int Id { get; set; }
        public string Avatar { get; set; }

        public ImageUser(int id, string avatar)
        {
            Id = id;
            Avatar = avatar;
        }
    }
}