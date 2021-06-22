
using Maganizer_Project.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maganizer_Project.Models
{
  public class AdminIndexViewModel
  {
    public List<UserInfoDTO> Users { get; set; }
    public List<TagDTO> Tags { get; set; }

  }
}
