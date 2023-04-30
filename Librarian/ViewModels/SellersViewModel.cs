using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.ViewModels
{
    public class SellersViewModel : ViewModel
    {
        private readonly IRepository<Seller> _sellersRepository;

        public SellersViewModel() : this(new DebugSellersRepository())
        {
            if(!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public SellersViewModel(IRepository<Seller> sellersRepository)
        {
            _sellersRepository = sellersRepository;
        }
    }
}
