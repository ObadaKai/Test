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

    public partial class Study_subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Study_subject()
        {
            this.Examinations = new HashSet<Examination>();
        }
    
        public int id { get; set; }
        [Required(ErrorMessage = "���� ����� ����� ")]
        [DisplayName("��� ������")]
        public string Name { get; set; }
        [DisplayName("�����")]
        public string Desc { get; set; }
        [Required(ErrorMessage = "���� ����� ������� ������� ")]
        [DisplayName("������� �������")]
        public Nullable<double> FullMark { get; set; }
        [Required(ErrorMessage = "���� ����� ����� ������ ")]
        [DisplayName("����� ������")]
        public Nullable<double> LeastMark { get; set; }
        [DisplayName("������")]
        public string State { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Examination> Examinations { get; set; }
    }
}
