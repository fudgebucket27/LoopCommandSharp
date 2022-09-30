﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopCommandSharp.Models
{
    public class Settings
    {
        public string? LoopringApiKey { get; set; }
        public string? LoopringPrivateKey { get; set; }
        public string? LoopringAddress { get; set; }
        public int LoopringAccountId { get; set; }
        public long ValidUntil { get; set; }
        public int MaxFeeTokenId { get; set; }
        public string? NftFactory { get; set; }

        public string? NftFactoryCollection { get; set; }
        public string? Exchange { get; set; }
    }

    public class AppSettings
    {
        public Settings? Settings { get; set; }
    }
}
