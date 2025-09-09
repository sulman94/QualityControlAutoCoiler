using System;
using System.Collections.Generic;
namespace Entities.ViewModels
{
    public class UserPermissionsModel
    {
        public int ModuleId { get; set; }
        public string FunctionalityName { get; set; }
        public bool AllowAccess { get; set; }
        public string ModuleName { get; set; }
        public string ControllerName { get; set; }
        public string ActionMethodName { get; set; }
    }
}
