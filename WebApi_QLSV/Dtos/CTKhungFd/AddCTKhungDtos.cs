﻿using WebApi_QLSV.Dtos.MonHocFd;

namespace WebApi_QLSV.Dtos.CTKhungFd
{
    public class AddCTKhungDtos
    {
        public string CTKhungId { get; set; }
        
        public List<AddMonHocDtos> MonHocs { get; set; }
    }
}
