﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User
    {
        String name;
        String rank;

        public User(string name, string rank)
        {
            Name = name;
            Rank = rank;
        }

        public string Name { get => name; set => name = value; }
        public string Rank { get => rank; set => rank = value; }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   name.Equals(user.name) &&
                   rank.Equals(user.rank);
                  
        }

        public override string ToString()
        {
            return "Nume: " + Name + " Grad: " + Rank;
        }

    }
}
