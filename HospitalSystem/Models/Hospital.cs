using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalSystem.Models
{
    public class Hospital
    {
        
        public int Id { get; set; }
        [Required]
        [Display(Name = "اسم المستشفى")]
        public string Name { get; set; }

        [Display(Name = "العنوان")]
        public string Location { get; set; }

        [Display(Name = " الإحداثيات على الخريطة")]
        public string Coordinates { get; set; }

        [Display(Name = "التيليفون")]
        public string Phone { get; set; }
        [Required]

        [Display(Name="عدد الأسرة الكلي")]
        public int NumberOfBeds { get; set; }

        [Display(Name="عدد الأسرة المتاحة")]
        public int NumberOfBedsAvailable { get; set; }

        [Display(Name = "عدد أكياس الدم المتاحة")]
        public int NumberOfBloodBags { get; set; }

        [Display(Name = "A+")]
        public int A_Plus { get; set; }

        [Display(Name = "A-")]
        public int A_Minus { get; set; }

        [Display(Name = "B+")]
        public int B_Plus { get; set; }

        [Display(Name = "B-")]
        public int B_Minus { get; set; }

        [Display(Name = "AB+")]
        public int AB_Plus { get; set; }

        [Display(Name = "AB-")]
        public int AB_Minus { get; set; }

        [Display(Name = "O+")]
        public int O_Plus { get; set; }

        [Display(Name = "O-")]
        public int O_Minus { get; set; }


    }
}