﻿using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.Teacher
{
    public class AddTeacherDtos
    {
        public string TenGiangVien { get; set; }
        public DateTime Birthday { get; set; }
        public bool GioiTinh { get; set; }
        public string Cccd { get; set; }
        public string BoMonId { get; set; }
    }
}