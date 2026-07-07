using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Asignacion_2_Recordatorios.Models
{
    [Table("usuarios")]
    public class Usuario : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("correo")]
        public string Correo { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("bloqueado")]
        public bool Bloqueado { get; set; }
    }
}