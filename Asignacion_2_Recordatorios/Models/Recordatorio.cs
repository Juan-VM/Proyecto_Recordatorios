
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Asignacion_2_Recordatorios.Models
{
    [Table("recordatorios")]
    public class Recordatorio : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Column("titulo")]
        public string Titulo { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("fecha_recordatorio")]
        public DateTime FechaRecordatorio { get; set; }

        [Column("completado")]
        public bool Completado { get; set; }

        [Column("creado_at")]
        public DateTime CreadoAt { get; set; }
    }
}
