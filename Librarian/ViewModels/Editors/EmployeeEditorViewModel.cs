using Librarian.DAL.Entities;
using Librarian.Infrastructure.DebugServices;
using Librarian.Interfaces;
using Swftx.Wpf.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Librarian.ViewModels
{
    public class EmployeeEditorViewModel : ViewModel
    {
        private readonly IRepository<WorkingRate> _workingRatesRepository;

        private CollectionViewSource _workingRatesViewSource;

        #region Tilte
        private string? _Title = "Seller Editor";

        /// <summary>
        /// Window title
        /// </summary>
        public string? Title { get => _Title; set => Set(ref _Title, value); }
        #endregion

        #region WorkingRatesView
        /// <summary>
        /// WorkingRates collection view.
        /// </summary>
        public ICollectionView WorkingRatesView => _workingRatesViewSource.View;
        #endregion

        #region WorkingRatesFilter
        private string? _WorkingRatesFilter;

        /// <summary>
        /// Filter working rates
        /// </summary>
        public string? WorkingRatesFilter
        {
            get => _WorkingRatesFilter;
            set
            {
                if (Set(ref _WorkingRatesFilter, value))
                    _workingRatesViewSource.View.Refresh();
            }
        }
        #endregion

        #region WorkingRates
        private ObservableCollection<WorkingRate>? _WorkingRates;

        /// <summary>
        /// WorkingRates collection
        /// </summary>
        public ObservableCollection<WorkingRate>? WorkingRates
        {
            get => _WorkingRates;
            set
            {
                if (Set(ref _WorkingRates, value))
                    _workingRatesViewSource.Source = value;
                OnPropertyChanged(nameof(WorkingRatesView));
            }
        }
        #endregion

        #region EmployeeId
        /// <summary>
        /// Employee id
        /// </summary>
        public int EmployeeId { get; }
        #endregion

        #region EmployeeName
        private string? _EmployeeName;

        /// <summary>
        /// Employee name
        /// </summary>
        public string? EmployeeName { get => _EmployeeName; set => Set(ref _EmployeeName, value); }
        #endregion

        #region EmployeeSurname
        private string? _EmployeeSurname;

        /// <summary>
        /// Employee surname
        /// </summary>
        public string? EmployeeSurname { get => _EmployeeSurname; set => Set(ref _EmployeeSurname, value); }
        #endregion

        #region EmployeeDateOfBirth
        private DateTime _EmployeeDateOfBirth;

        /// <summary>
        /// Employee date of birth
        /// </summary>
        public DateTime EmployeeDateOfBirth { get => _EmployeeDateOfBirth; set => Set(ref _EmployeeDateOfBirth, value); }
        #endregion

        #region EmployeeHireDate
        private DateTime _EmployeeHireDate;

        /// <summary>
        /// Employee hire date
        /// </summary>
        public DateTime EmployeeHireDate { get => _EmployeeHireDate; set => Set(ref _EmployeeHireDate, value); }
        #endregion

        #region EmployeeExtension
        private DateTime? _EmployeeExtension;

        /// <summary>
        /// Employee hire date
        /// </summary>
        public DateTime? EmployeeExtension { get => _EmployeeExtension; set => Set(ref _EmployeeExtension, value); }
        #endregion

        #region EmployeeTitle
        private string? _EmployeeTitle;

        /// <summary>
        /// Employee title
        /// </summary>
        public string? EmployeeTitle { get => _EmployeeTitle; set => Set(ref _EmployeeTitle, value); }
        #endregion

        #region EmployeeContactNumber
        private string? _EmployeeContactNumber;

        /// <summary>
        /// Employee contact number
        /// </summary>
        public string? EmployeeContactNumber { get => _EmployeeContactNumber; set => Set(ref _EmployeeContactNumber, value); }
        #endregion

        #region EmployeeMail
        private string? _EmployeeMail;

        /// <summary>
        /// Employee mail
        /// </summary>
        public string? EmployeeMail { get => _EmployeeMail; set => Set(ref _EmployeeMail, value); }
        #endregion

        #region EmployeeIdentityDocumentNumber
        private string? _EmployeeIdentityDocumentNumber;

        /// <summary>
        /// Employee identity document number
        /// </summary>
        public string? EmployeeIdentityDocumentNumber { get => _EmployeeIdentityDocumentNumber; set => Set(ref _EmployeeIdentityDocumentNumber, value); }
        #endregion

        #region EmployeeWorkingRate
        private WorkingRate? _EmployeeWorkingRate;

        /// <summary>
        /// Employee working rate
        /// </summary>
        public WorkingRate? EmployeeWorkingRate { get => _EmployeeWorkingRate; set => Set(ref _EmployeeWorkingRate, value); }
        #endregion

        #region EmployeeAddress
        private string? _EmployeeAddress;

        /// <summary>
        /// Employee address
        /// </summary>
        public string? EmployeeAddress { get => _EmployeeAddress; set => Set(ref _EmployeeAddress, value); }
        #endregion

        public EmployeeEditorViewModel() : this(
            new Employee { Id = 1, Name = "John", Surname = "Winston" },
            new DebugWorkingRatesRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public EmployeeEditorViewModel(Employee employee, IRepository<WorkingRate> workingRatesRepository)
        {
            _workingRatesRepository = workingRatesRepository;

            _workingRatesViewSource = new CollectionViewSource();

            EmployeeId = employee.Id;
            EmployeeName = employee.Name;
            EmployeeSurname = employee.Surname;
            EmployeeDateOfBirth = employee.DateOfBirth;
            EmployeeHireDate = employee.HireDate;
            EmployeeExtension = employee.Extension;
            EmployeeTitle = employee.Title;
            EmployeeContactNumber = employee.ContactNumber;
            EmployeeMail = employee.ContactMail;
            EmployeeIdentityDocumentNumber = employee.IdentityDocumentNumber;
            EmployeeWorkingRate = employee.WorkingRate;
            EmployeeAddress = employee.Address;

            _workingRatesViewSource.Filter += OnWorkingRatesFilter;
        }

        private void OnWorkingRatesFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is WorkingRate workingRate) || string.IsNullOrWhiteSpace(WorkingRatesFilter)) return;

            if (workingRate.Name is null || !workingRate.Name.Contains(WorkingRatesFilter))
                e.Accepted = false;
        }
    }
}
