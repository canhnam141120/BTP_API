using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BTP_API.Models
{
    public partial class Notification
    {
        /// <summary>
        /// Mã thông báo
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Mã người dùng
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Nội dung
        /// </summary>
        [Required]
        public string Content { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Đã đọc?
        /// </summary>
        [Required]
        public bool IsRead { get; set; }

        public virtual User User { get; set; }
        
    }
}
