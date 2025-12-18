using CapaDatos.BaseDatos.Tablas;
using CapaDatos.BaseDatos.Tablas.Catalogos;
using CapaDatos.BaseDatos.Tablas.ControlCitas;
using CapaDatos.BaseDatos.Tablas.ExpedienteClinico;
using CapaDatos.BaseDatos.Tablas.InventarioYFacturacion;
using CapaDatos.BaseDatos.Tablas.Login;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.BaseDatos
{
    public class MediNovaContext : DbContext
    {
        public MediNovaContext() : base("MediNova")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Relación 1:1 entre Paciente y Expediente
            modelBuilder.Entity<TPaciente>()
                .HasOptional(p => p.Expediente)        // Un paciente puede o no tener expediente
                .WithRequired(e => e.Paciente);        // Un expediente debe tener un paciente

            // Relación 1:1 entre TVenta y TFactura
            modelBuilder.Entity<TVenta>()
                .HasOptional(v => v.Factura)
                .WithRequired(f => f.Venta);

            // Relación opcional de TDoctor a TUsuario
            modelBuilder.Entity<TDoctor>()
                .HasOptional(d => d.Usuario)
                .WithMany() 
                .HasForeignKey(d => d.UsuarioId)
                .WillCascadeOnDelete(false);
        }

        public DbSet<TUsuario> TUsuarios { get; set; }
        public DbSet<TRol> TRols { get; set; }
        public DbSet<TDoctor> TDoctores { get; set; }
        public DbSet<THorario> THorario { get; set; }
        public DbSet<TPaciente> TPacientes { get; set; }
        public DbSet<TEnfermedad> TEnfermedades { get; set; }
        public DbSet<TExamen> TExamenes { get; set; }
        public DbSet<TMedicamento> TMedicamentos { get; set; }
        public DbSet<TInventarioMovimiento> TInventarioMovimientos { get; set; }
        public DbSet<TFactura> TFacturas { get; set; }
        public DbSet<TDetalleFactura> TDetalleFacturas { get; set; }
        public DbSet<TCita> TCitas { get; set; }
        public DbSet<TExpediente> TExpedientes { get; set; }
        public DbSet<TConsulta> TConsultas { get; set; }
        public DbSet<TDiagnostico> TDiagnosticos { get; set; }
        public DbSet<TVenta> TVentas { get; set; }
        public DbSet<TDetalleVenta> TDetalleVentas { get; set; }
        public DbSet<TReceta> TRecetas { get; set; }
        public DbSet<TDetalleReceta> TDetalleRecetas { get; set; }
        public DbSet<TEmpresa> TEmpresas { get; set; }

        }
}
