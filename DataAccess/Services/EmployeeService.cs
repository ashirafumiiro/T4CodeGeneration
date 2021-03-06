//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.Services
{
    using System;
    using System.Collections.Generic;
    
    using DataAccess.DTO;
    using DataAccess.Entities;
    using DataAccess.Repository;
    using DataAccess.Interfaces;
    using DataAccess.Context;
    using Microsoft.Extensions.Logging;
    
    public partial class EmployeeService : BaseRepository<Employee, EmployeeDTO>,  IEmployeeService
    {
    
        public EmployeeService(EmployeeContext context, ILogger<EmployeeService> logger): base(context, logger)
        {
        }
    
        public override Employee Update(EmployeeDTO entity)
        {
            Employee returnValue = null;
    
            try
            {
                var model = this.Context.Employees.Where(p => p.ID == entity.ID).FirstOrDefault();
                
    
                if (model != null)
                {
                
                    if (entity.ID != null)
                    {
                        model.ID = entity.ID.Value;
                    }
            
                    if (entity.FirstName != null)
                    {
                        model.FirstName = entity.FirstName;
                    }
            
                    if (entity.LastName != null)
                    {
                        model.LastName = entity.LastName;
                    }
            
                    if (entity.Gender != null)
                    {
                        model.Gender = entity.Gender;
                    }
            
                    if (entity.Salary != null)
                    {
                        model.Salary = entity.Salary.Value;
                    }
            
                    if (entity.DepartmentId != null)
                    {
                        model.DepartmentId = entity.DepartmentId.Value;
                    }
                            
                    this.Context.Employees.Update(model);
                    this.SaveChanges();
                    
                    // refresh entity
                    returnValue = this.Context.Employees.Where(p => p.ID == entity.ID).FirstOrDefault();;
    
                }
    
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
    
            return returnValue;
    	}
    
    }
}
