//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ta3lim.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Examination
    {
        public int id { get; set; }
        [DisplayName("�����")]
        public string Desc { get; set; }
        public Nullable<int> Subjectid { get; set; }
        public Nullable<int> Studentid { get; set; }
        public Nullable<int> Stageid { get; set; }
        public Nullable<int> ExamTypeid { get; set; }
        [Required(ErrorMessage = "���� ����� �������")]
        [DisplayName("�������")]
        public Nullable<double> Mark { get; set; }
        [Required(ErrorMessage = "���� ����� �������")]
        [DisplayName("�������")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/p/yyyy}")]

        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Stage Stage { get; set; }
        public virtual Student Student { get; set; }
        public virtual Study_subject Study_subject { get; set; }
        public virtual ExamType ExamType { get; set; }
    }
}
