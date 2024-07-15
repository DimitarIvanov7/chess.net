﻿
using Microsoft.Extensions.Hosting;

namespace WebApplication3.Model.Domain
{
    public class Player
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public int Elo { get; set; } = 100;


    }

}
