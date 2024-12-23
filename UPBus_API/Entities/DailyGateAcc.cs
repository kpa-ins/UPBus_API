﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UPBus_API.Entities
{
    public class DailyGateAcc
    {
        [Key]
        [Column(TypeName = "varchar(4)")]
        public string GateCode { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? AccDate { get; set; } = null!;

        [Column(TypeName = "float")]
        public float? IncTotalAmt { get; set; }
        [Column(TypeName = "float")]
        public float? ExpTotalAmt { get; set; }
        [Column(TypeName = "float")]
        public float? IncCreditAmt { get; set; }
        [Column(TypeName = "float")]
        public float? IncReceiveAmt { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Remark { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? CreatedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? UpdatedUser { get; set; }
    }
}
