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
    
    public partial class AuditEntryService : BaseRepository<AuditEntry, AuditEntryDTO>,  IAuditEntryService
    {
    
        public AuditEntryService(EmployeeContext context, ILogger<AuditEntryService> logger): base(context, logger)
        {
        }
    
        public override AuditEntry Update(AuditEntryDTO entity)
        {
            AuditEntry returnValue = null;
    
            try
            {
                var model = this.Context.AuditEntries.Where(p => p.Id == entity.Id).FirstOrDefault();
                
    
                if (model != null)
                {
                
                    if (entity.Id != null)
                    {
                        model.Id = entity.Id.Value;
                    }
            
                    if (entity.InsertedValue != null)
                    {
                        model.InsertedValue = entity.InsertedValue;
                    }
                            
                    this.Context.AuditEntries.Update(model);
                    this.SaveChanges();
                    
                    // refresh entity
                    returnValue = this.Context.AuditEntries.Where(p => p.Id == entity.Id).FirstOrDefault();;
    
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
