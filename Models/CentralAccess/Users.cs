using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mvccore.Models.CentralAccess;

public partial class Users
{
    public long UserCode { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
   
    public string? FullName { get; set; }

    public string? EmailAddress { get; set; }

    public bool? Active { get; set; }

    public DateTime? DateRegistered { get; set; }

    public DateTime? DateRegistrationVerified { get; set; }

    public long? RegistrationVerifiedBy { get; set; }

    public DateTime? DateModified { get; set; }

    public long? ModifiedBy { get; set; }

    public string? Remarks { get; set; }

    public string? EmployeeNo { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public string? PasswordEncypt { get; set; }
}
