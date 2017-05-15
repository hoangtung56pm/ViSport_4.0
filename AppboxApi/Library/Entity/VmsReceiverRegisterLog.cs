using System;

namespace AppboxApi.Library.Entity
{
    public class VmsReceiverRegisterLog
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ServiceId { get; set; }

        public string Msisdn { get; set; }

        public string SubsTime { get; set; }

        public int Params { get; set; }

        public string Mo { get; set; }

        public DateTime CreatedDate { get; set; }

    }

    public class VmsAppboxMoLog
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string ShortCode { get; set; }

        public string Moseq { get; set; }

        public string CmdCode { get; set; }

        public string MsgBody { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class VmsAppboxMtLog
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string ShortCode { get; set; }

        public Int32 MtSeq { get; set; }

        public string MsgType { get; set; }

        public string MsgTitle { get; set; }

        public string MsgBody { get; set; }

        public string MoSeq { get; set; }

        public int ProcResult { get; set; }

        public string CpId { get; set; }

        public string ServiceId { get; set; }

        public string ContentId { get; set; }

        public int Price { get; set; }

        public string Channel { get; set; }

        public int SrcPort { get; set; }

        public int DestPort { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

    }

    public class VmsAppboxGamelinkLog
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public string Msisdn { get; set; }

        public string ReqTime { get; set; }

        public string ShortCode { get; set; }

        public string ReqId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

    }

    public class VmsAppboxBillingLog
    {
        public int Id { get; set; }

        public string CpRequestId { get; set; }

        public string Mobile { get; set; }

        public string ChargeType { get; set; }

        public int ResponseCode { get; set; }

        public int Price { get; set; }

        public int Type { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}