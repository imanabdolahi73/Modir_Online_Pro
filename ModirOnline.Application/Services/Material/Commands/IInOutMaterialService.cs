using ModirOnline.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModirOnline.Application.Services.Material.Commands
{
    public interface IInOutMaterialService
    {

    }
    public class InOutMaterialService : IInOutMaterialService
    {
        private readonly IModirOnlineDbContext _context;
        public InOutMaterialService(IModirOnlineDbContext context) 
        {
            _context = context;
        }

    }
}
