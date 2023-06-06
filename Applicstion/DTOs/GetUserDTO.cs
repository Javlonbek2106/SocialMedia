using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetUserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte Age { get; set; }
        public List<GetPostDTO> Posts { get; set; }
    }
}
