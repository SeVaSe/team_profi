//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace team_profi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Grades
    {
        public int GradeID { get; set; }
        public int AnswerID { get; set; }
        public int TeacherID { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
    
        public virtual Answers Answers { get; set; }
        public virtual Users Users { get; set; }
    }
}