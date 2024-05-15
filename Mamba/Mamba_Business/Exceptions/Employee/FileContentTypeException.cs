﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba_Business.Exceptions.Employee
{
    public class FileContentTypeException : Exception
    {
        public string PropertyName { get; set; }
        public FileContentTypeException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
