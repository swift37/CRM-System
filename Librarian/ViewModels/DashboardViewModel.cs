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
        private readonly IRepository<Order> _transactionsRepository;

        #region Properties

        #region LastTransactions
        private ObservableCollection<Order>? _LastTransactions;

        /// <summary>
        /// Last Transactions
        /// </summary>
        public ObservableCollection<Order>? LastTransactions { get => _LastTransactions; set => Set(ref _LastTransactions, value); } 
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
            if (_transactionsRepository.Entities is null) return;

            LastTransactions = (await _transactionsRepository.Entities.OrderByDescending(t => t.TransactionDate).Take(7).ToArrayAsync()).ToObservableCollection();
        }
        #endregion

        #endregion

        public DashboardViewModel() : this(new DebugTransactionsRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));

            _ = OnLoadLastTransactionsCommandExecuted();
        }

        public DashboardViewModel(IRepository<Order> transactionsRepository) 
        {
            _transactionsRepository = transactionsRepository;
        }
    }
}
