﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Entity
{
    public class Genre : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
