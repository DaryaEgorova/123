using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Maganizer_Project.BLL.Infrastructure
{
    public static class ImageConvertion
    {
        public static byte[] ConvertToByteArray(IFormFile file) 
        {
            byte[] fileInByte;

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                fileInByte = binaryReader.ReadBytes((int)file.Length);
            }

            return fileInByte;
        }
    }
}
