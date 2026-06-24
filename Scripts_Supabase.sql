CREATE OR REPLACE VIEW vw_mis_recordatorios AS
SELECT
    id,
    titulo,
    descripcion,
    fecha_recordatorio,
    completado,

    CASE
        WHEN completado THEN 'Finalizado'
        WHEN fecha_recordatorio::date = CURRENT_DATE THEN 'Hoy'
        WHEN fecha_recordatorio < NOW() THEN 'Vencido'
        ELSE 'Próximo'
    END AS situacion

FROM recordatorios;



create policy "Temporary Dev Insert - Allow All"
on "public"."recordatorios"
as PERMISSIVE
for INSERT
to public
with check ( true );


CREATE POLICY "Permitir actualizacion de recordatorios"
ON recordatorios
FOR UPDATE
USING (true)
WITH CHECK (true);

INSERT INTO recordatorios
(usuario_id, titulo, descripcion, fecha_recordatorio, completado)
VALUES
(1,
'Entregar Proyecto',
'Proyecto ASP.NET MVC',
CURRENT_TIMESTAMP,
FALSE),

(1,
'Comprar comida',
'Ir al supermercado',
CURRENT_TIMESTAMP + INTERVAL '1 day',
FALSE),

(1,
'Pagar internet',
'Factura mensual',
CURRENT_TIMESTAMP - INTERVAL '2 day',
FALSE),

(1,
'Reunión finalizada',
'Proyecto terminado',
CURRENT_TIMESTAMP - INTERVAL '1 day',
TRUE),

(1,
'Estudiar Bases de Datos',
'Repasar particiones',
CURRENT_TIMESTAMP + INTERVAL '5 day',
FALSE);
