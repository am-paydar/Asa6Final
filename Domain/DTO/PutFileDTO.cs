﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class PutFileDTO
    {
        public string path { get; set; }
        public string RecivedID { get; set; }
        public IFormFile FormFile { get; set; }

    }
}