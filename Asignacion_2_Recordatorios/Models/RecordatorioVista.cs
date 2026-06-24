using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Asignacion_2_Recordatorios.Models
{
    [Table("vw_mis_recordatorios")]
    public class RecordatorioVista : BaseModel
    {
        [PrimaryKey("id", false)] public int Id { get; set; }

        [Column("titulo")] public string Titulo { get; set; }

        [Column("descripcion")] public string Descripcion { get; set; }

        [Column("fecha_recordatorio")] public DateTime FechaRecordatorio { get; set; }

        [Column("situacion")] public string Situacion { get; set; }

        [Column("completado")] public bool Estado { get; set; }
    }
}