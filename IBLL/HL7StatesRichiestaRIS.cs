﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public class HL7StatesRichiestaRIS
    {
        public static readonly string Idle = "IDLE";
        public static readonly string Sending = "SENDING";
        public static readonly string Sent = "SENT";
        public static readonly string Deleting = "DELETING";
        public static readonly string Deleted = "DELETED";
        public static readonly string Errored = "ERRORED";
    }
}
