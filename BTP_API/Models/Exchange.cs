using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BTP_API.Models
{
    public partial class Exchange
    {
        public Exchange()
        {
            ExchangeBills = new HashSet<ExchangeBill>();
            ExchangeDetails = new HashSet<ExchangeDetail>();
        }

        /// <summary>
        /// Mã giao dịch đổi
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Mã người dùng 1
        /// </summary>
        [Required]
        public int UserId1 { get; set; }
        /// <summary>
        /// Mã người dùng 2
        /// </summary>
        [Required]
        public int UserId2 { get; set; }
        /// <summary>
        /// Ngày giao dịch
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        [Required]
        public string Status { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        [Required]
        public string StorageStatus1 { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        [Required]
        public string StorageStatus2 { get; set; }
        /// <summary>
        /// Ngày gửi
        /// </summary>
        public DateOnly? SendDate { get; set; }
        /// <summary>
        /// Ngày nhận
        /// </summary>
        public DateOnly? ReceiveDate { get; set; }
        /// <summary>
        /// Ngày thu hồi
        /// </summary>
        public DateOnly? RecallDate { get; set; }
        /// <summary>
        /// Ngày hoàn trả
        /// </summary>
        public DateOnly? RefundDate { get; set; }

        public virtual User UserId1Navigation { get; set; }
        public virtual User UserId2Navigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<ExchangeBill> ExchangeBills { get; set; }
        [JsonIgnore]
        public virtual ICollection<ExchangeDetail> ExchangeDetails { get; set; }
    }
}
