﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.NganhFd
{
    public class AddNganhDtos
    {
        public string NganhId { get; set; }
        public string? TenNganh { get; set; }
        public string KhoaId { get; set; }
        public DateTime NgayThanhLap {  get; set; }
        public string TruongNganh { get; set; }
        public string PhoNganh { get; set; }
    }
}
