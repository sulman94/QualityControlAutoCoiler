using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class ManualBiltyDropdownViewModel
    {
        [Required(ErrorMessage = "Please select Comodity")]
        public int DescriptionId { get; set; }
        public int? ContainerOffloadingPointId { get; set; }

        [Required(ErrorMessage = "Please select Consignment")]
        public int ConsignmentId { get; set; }

        [Required(ErrorMessage = "Please select Agent")]
        public int AgentNameId { get; set; }

        [Required(ErrorMessage = "Please select Ship")]

        public int ShipId { get; set; }
        [Required(ErrorMessage = "Please select Client")]

        public int ClientId { get; set; }
        [Required(ErrorMessage = "Please select LoadingPoint")]

        public int LoadingPointId { get; set; }

        [Required(ErrorMessage = "Please select Unloading Point")]
        public int UnloadingPointId { get; set; }
    }
    public class UploadManualBiltyModel
    {
        public IFormFile file { get; set; }

        [Required(ErrorMessage = "Please select Comodity")]
        public int DescriptionId { get; set; }
        public int? ContainerOffloadingPointId { get; set; }

        [Required(ErrorMessage = "Please select Consignment")]

        public int ConsignmentId { get; set; }
        public int? OwnerId { get; set; }

        [Required(ErrorMessage = "Please select Agent")]
        public int AgentNameId { get; set; }

        [Required(ErrorMessage = "Please select Ship")]

        public int ShipId { get; set; }
        [Required(ErrorMessage = "Please select Client")]

        public int ClientId { get; set; }
        [Required(ErrorMessage = "Please select LoadingPoint")]

        public int LoadingPointId { get; set; }

        [Required(ErrorMessage = "Please select Unloading Point")]
        public int UnloadingPointId { get; set; }
    }
}
