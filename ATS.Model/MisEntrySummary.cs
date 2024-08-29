﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Model
{
    public class MisEntrySummary
    {
        public long UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string ProfilePic { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int TotalCount { get; set; }
    }
}
