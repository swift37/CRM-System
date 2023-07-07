using Librarian.DAL.Entities;
using Swftx.Wpf.ViewModels;
using System;

namespace Librarian.ViewModels
{
    public class EmployeeFullInfoViewModel : ViewModel
    {
        #region EmployeeId
        /// <summary>
        /// Employee id
        /// </summary>
        public int EmployeeId { get; set; }
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

        //public EmployeeFullInfoViewModel() : this(new Employee { Id = 1, Name = "John", Surname = "Winston" })
        //{
        //    if (!App.IsDesignMode)
        //        throw new InvalidOperationException(nameof(App.IsDesignMode));
        //}

        public EmployeeFullInfoViewModel() { }

        public void InitProps(Employee employee)
        {
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
        }
    }
}
