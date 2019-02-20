namespace appMoteleros.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "Placa")]
        [StringLength(7)]
        public string LicensePlate { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [StringLength(60)]
        public string Description { get; set; }

        [Display(Name = "Propietario")]
        [StringLength(60)]
        public string FirstName{ get; set; }

        [Display(Name = "Teléfono")]
        [StringLength(20)]
        public string Phone { get; set; }


        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Display(Name = "Foto")]
        public string ImagePath { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noproduct";
                }

                // api en Casa
                return $"http://192.168.1.36:8075/{this.ImagePath.Substring(1)}";
            }
        }

        public override string ToString()
        {
            return this.Description;
        }
    }
}
