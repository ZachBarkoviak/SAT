using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SAT.DATA.EF.MetaData
{
    //public class Partials
    //{

    //}
    [ModelMetadataType(typeof(CourseMetadata))]
    public partial class Course {}
    [ModelMetadataType(typeof(EnrollmentMetaData))]
    public partial class EnrollmentMetaData {}
    public partial class ScheduledClassMetaData {}
    [ModelMetadataType(typeof(StudentsMetaData))]
    public partial class StudentsMetaData {}
    [ModelMetadataType(typeof(StudentStatusMetaData))]
    public partial class StudentStatusMetaData {}
    [ModelMetadataType(typeof(ScheduledClassStatusMetaData))]
    public partial class ScheduledClassStatusMetaData {}

}
    
