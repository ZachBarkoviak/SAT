using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SAT.DATA.EF.MetaData
{
    public class CourseMetadata
    {
        [NotNull]

        public int CourseId { get; set; }
        
        [Display(Name = "Category")]

        [NotNull]
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters.")]
        public string? CourseName { get; set; }

        [NotNull]
        [StringLength(5000, ErrorMessage = "Cannot exceed 5000 characters")]
        [Display(Name ="Description")]
        public string? Description { get; set; }
        [NotNull]
        public int CreditHours { get; set; }
        [StringLength(250, ErrorMessage = "Cannot exceed 250 characters.")]
        public string? Curriculum { get; set; }

        [StringLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string? Notes { get; set; }

        [NotNull] public int IsActive { get; set; }

    }
    public partial class EnrollmentMetaData
    {
        [NotNull]
        public int EnrollmentId { get; set; }
        [NotNull]
        public int StudentId { get; set; }
        [NotNull]
        public int ScheduledClassId { get; set; }
        [NotNull]
        public DateTime EnrollmentDate { get; set; }
       
    }

    public partial class ScheduledClassMetaData
    {
        [NotNull]
        public int ScheduledClassId { get; set; }
        [NotNull]
        public int CourseId { get; set; }
        [NotNull]
        public DateTime StartDate { get; set; }
        [NotNull]
        public DateTime EndDate { get; set; }
        [NotNull]
        [StringLength(40, ErrorMessage = "Cannot exceed 40 characters.")]
        public string? InstructorName { get; set; }
        [NotNull]
        [StringLength(20, ErrorMessage = "Cannot exceed 20 characters.")]
        public string? Location { get; set; }
        [NotNull]
        public int Scsid { get; set; }

    }

    public partial class StudentsMetaData
    {
        [NotNull]
        public int StudentId { get; set; }
        [NotNull]
        [StringLength(20, ErrorMessage = "Cannot exceed 20 characters")]
        public string? FirstName { get; set; }
        [NotNull]
        [StringLength(20, ErrorMessage = "Cannot exceed 20 characters")]
        public string? LastName { get; set; }
        [StringLength(15, ErrorMessage = "Cannot exceed 15 characters")]
        public string? Major { get; set; }
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        public string? Address { get; set; }
        [StringLength(25, ErrorMessage = "Cannot exceed 25 characters")]
        public string? City { get; set; }
        [StringLength(2, ErrorMessage = "Cannot exceed 2 characters")]
        public string? State { get; set; }
        [StringLength(10, ErrorMessage = "Cannot exceed 10 characters")]
        public string? ZipCode { get; set; }
        [StringLength(13, ErrorMessage = "Cannot exceed 13 characters")]
        public string? Phone { get; set; }
        [NotNull]
        [StringLength(60, ErrorMessage = "Cannot exceed 60 characters")]
        public string? Email { get; set; }
        [StringLength(100, ErrorMessage = "Cannot exceed 100 characters")]
        public string? PhotoUrl { get; set; }
        [NotNull]
        public int Ssid { get; set; }
    }

    public partial class StudentStatusMetaData
    {
        [NotNull]
        public int Ssid { get; set; }
        [NotNull]
        [StringLength(30, ErrorMessage = "Cannot exceed 30 characters")]
        public string? SSName { get; set; }
        [StringLength(250, ErrorMessage = "Cannot exceed 250 characters")]
        public string? SSDescription { get; set; }    
    }

    public partial class ScheduledClassStatusMetaData
    {
        [NotNull]
        public int Scsid { get; set; }
        [NotNull]
        [StringLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        public string? SCSName { get; set; }
    }
}
