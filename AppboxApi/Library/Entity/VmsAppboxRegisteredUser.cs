using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppboxApi.Library.Entity
{
    public class VmsAppboxRegisteredUser
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string RequestId { get; set; }

        public string ServiceId { get; set; }

        public string CommandCode { get; set; }

        public int ChargingCount { get; set; }

        public int FailedChargingTimes { get; set; }

        public DateTime RegisteredTime { get; set; }

        public DateTime ExpiredTime { get; set; }

        public string RegistrationChannel { get; set; }

        public int Status { get; set; }

        public string Password { get; set; }

        public int PartnerId { get; set; }

    }
}