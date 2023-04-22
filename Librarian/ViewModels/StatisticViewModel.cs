using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Services;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.ViewModels
{
    public class StatisticViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;
        private readonly IRepository<Seller> _sellersRepository;
        private readonly IRepository<Buyer> _buyersRepository;

        public StatisticViewModel(IRepository<Book> booksRepository, IRepository<Seller> sellersRepository, IRepository<Buyer> buyersRepository)
        {
            _booksRepository = booksRepository;
            _sellersRepository = sellersRepository;
            _buyersRepository = buyersRepository;
        }
    }
}
