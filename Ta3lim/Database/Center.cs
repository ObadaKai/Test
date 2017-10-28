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

    public partial class Center
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Center()
        {
            this.Students = new HashSet<Student>();
            this.Employees = new HashSet<Employee>();
        }
    
        public int id { get; set; }
        [Required(ErrorMessage = "���� ����� ��� ������")]
        [DisplayName("��� ������")]
        public string Name { get; set; }
        [Required(ErrorMessage="���� ����� ������")]
        [DisplayName("������")]
        public string Place { get; set; }
        [DisplayName("�����")]
        public string Desc { get; set; }
        [DisplayName("������")]
        public string State { get; set; }
        [Required(ErrorMessage = "���� ����� ��� �������")]
        [DisplayName("��� �������")]
        public Nullable<int> HolesN { get; set; }
        public Nullable<int> Managerid { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}