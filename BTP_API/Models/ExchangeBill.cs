using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTP_API.Models
{
    public partial class ExchangeBill
    {
        /// <summary>
        /// Mã hóa đơn giao dịch đổi
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Mã giao dịch đổi
        /// </summary>
        [Required]
        public int ExchangeId { get; set; }
        /// <summary>
        /// Mã người dùng
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Tổng sách
        /// </summary>
        [Required]
        public int TotalBook { get; set; }
        /// <summary>
        /// Tổng tiền
        /// </summary>
        [Required]
        public float TotalAmount { get; set; }
        /// <summary>
        /// Phí đặt cọc
        /// </summary>
        [Required]
        public float DepositFee { get; set; }
        /// <summary>
        /// Mã phí 1
        /// </summary>
        [Required]
        public int FeeId1 { get; set; }
        /// <summary>
        /// Mã phí 2
        /// </summary>
        [Required]
        public int FeeId2 { get; set; }
        /// <summary>
        /// Mã phí 3
        /// </summary>
        public int? FeeId3 { get; set; }
        /// <summary>
        /// Đã thanh toán?
        /// </summary>
        [Required]
        public bool IsPaid { get; set; }
        /// <summary>
        /// Ngày thanh toán
        /// </summary>
        public DateTime? PaidDate { get; set; }
        /// <summary>
        /// Phương thức thanh toán
        /// </summary>
        public string Payments { get; set; }
        public virtual Exchange Exchange { get; set; }
        public virtual Fee FeeId1Navigation { get; set; }
        public virtual Fee FeeId2Navigation { get; set; }
        public virtual Fee FeeId3Navigation { get; set; }
        public virtual User User { get; set; }
    }
}
