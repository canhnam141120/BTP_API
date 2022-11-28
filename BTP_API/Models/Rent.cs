using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BTP_API.Models
{
    public partial class Rent
    {
        public Rent()
        {
            RentBills = new HashSet<RentBill>();
            RentDetails = new HashSet<RentDetail>();
        }

        /// <summary>
        /// Mã giao dịch thuê
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Mã người cho thuê
        /// </summary>
        [Required]
        public int OwnerId { get; set; }
        /// <summary>
        /// Mã người thuê
        /// </summary>
        [Required]
        public int RenterId { get; set; }
        /// <summary>
        /// Ngày giao dịch
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }
        /// <summary>
        /// Trạng thái giao dịch
        /// </summary>
        [Required]
        public string Status { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        [Required]
        public string StorageStatus { get; set; }
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

        public virtual User Owner { get; set; }
        public virtual User Renter { get; set; }
        [JsonIgnore]
        public virtual ICollection<RentBill> RentBills { get; set; }
        [JsonIgnore]
        public virtual ICollection<RentDetail> RentDetails { get; set; }
    }
}
