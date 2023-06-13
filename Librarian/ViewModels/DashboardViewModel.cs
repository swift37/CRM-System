using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Librarian.Services;
using Microsoft.EntityFrameworkCore;
using Swftx.Wpf.Commands;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Librarian.ViewModels
{
    public class DashboardViewModel : ViewModel
    {
        private readonly IRepository<Order> _ordersRepository;

        #region Properties

        #region LastOrders
        private ObservableCollection<Order>? _LastOrders;

        /// <summary>
        /// Last Orders
        /// </summary>
        public ObservableCollection<Order>? LastOrders { get => _LastOrders; set => Set(ref _LastOrders, value); } 
        #endregion

        #endregion

        #region Commands

        #region LoadLastTransactionsCommand
        private ICommand? _LoadLastTransactionsCommand;

        /// <summary>
        /// Load data command
        /// </summary>
        public ICommand? LoadLastTransactionsCommand => _LoadLastTransactionsCommand ??= new LambdaCommandAsync(OnLoadLastTransactionsCommandExecuted, CanLoadLastTransactionsCommandExecute);

        private bool CanLoadLastTransactionsCommandExecute() => true;

        private async Task OnLoadLastTransactionsCommandExecuted()
        {
            if (_ordersRepository.Entities is null) return;

            LastOrders = (await _ordersRepository.Entities.OrderByDescending(t => t.OrderDate).Take(7).ToArrayAsync()).ToObservableCollection();
        }
        #endregion

        #endregion

        public DashboardViewModel() : this(new DebugOrdersRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadLastTransactionsCommandExecuted();
        }

        public DashboardViewModel(IRepository<Order> transactionsRepository) 
        {
            _ordersRepository = transactionsRepository;
        }
    }
}
