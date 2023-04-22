﻿using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.ViewModels
{
    public class BuyersViewModel : ViewModel
    {
        private readonly IRepository<Buyer> _buyersRepository;

        public BuyersViewModel(IRepository<Buyer> buyersRepository)
        {
            _buyersRepository = buyersRepository;
        }
    }
}
