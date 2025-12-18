namespace CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionBaseDatos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TCita",
                c => new
                    {
                        CitaId = c.Int(nullable: false, identity: true),
                        PacienteId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        Asunto = c.String(nullable: false, maxLength: 255),
                        FechaHoraInicio = c.DateTime(nullable: false),
                        FechaHoraFin = c.DateTime(nullable: false),
                        TodoElDia = c.Boolean(nullable: false),
                        Ubicacion = c.String(maxLength: 100),
                        Estado = c.String(nullable: false, maxLength: 20),
                        Eliminado = c.Boolean(nullable: false),
                        ReglaRecurrencia = c.String(maxLength: 500),
                        IdCitaPadre = c.Int(),
                    })
                .PrimaryKey(t => t.CitaId)
                .ForeignKey("dbo.TDoctor", t => t.DoctorId)
                .ForeignKey("dbo.TPaciente", t => t.PacienteId)
                .Index(t => t.PacienteId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.TDoctor",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(nullable: false, maxLength: 100),
                        Especialidad = c.String(nullable: false, maxLength: 50),
                        Telefono = c.String(maxLength: 20),
                        Correo = c.String(maxLength: 100),
                        Disponible = c.Boolean(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.DoctorId)
                .ForeignKey("dbo.TUsuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.TExpediente",
                c => new
                    {
                        PacienteId = c.Int(nullable: false),
                        FechaApertura = c.DateTime(nullable: false),
                        NumeroExpediente = c.String(maxLength: 50),
                        Alergias = c.String(),
                        AntecedentesFamiliares = c.String(),
                        AntecedentesPersonales = c.String(),
                        TipoSangre = c.String(maxLength: 20),
                        NotasGenerales = c.String(),
                        ContactoEmergencia = c.String(maxLength: 100),
                        TelefonoEmergencia = c.String(maxLength: 20),
                        Eliminado = c.Boolean(nullable: false),
                        TDoctor_DoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.PacienteId)
                .ForeignKey("dbo.TPaciente", t => t.PacienteId)
                .ForeignKey("dbo.TDoctor", t => t.TDoctor_DoctorId)
                .Index(t => t.PacienteId)
                .Index(t => t.TDoctor_DoctorId);
            
            CreateTable(
                "dbo.TConsulta",
                c => new
                    {
                        ConsultaId = c.Int(nullable: false, identity: true),
                        ExpedienteId = c.Int(nullable: false),
                        CitaId = c.Int(),
                        DoctorId = c.Int(nullable: false),
                        NumeroConsulta = c.String(nullable: false, maxLength: 50),
                        FechaConsulta = c.DateTime(nullable: false),
                        MotivoConsulta = c.String(nullable: false, maxLength: 500),
                        Sintomas = c.String(maxLength: 2000),
                        PresionArterial = c.String(maxLength: 20),
                        Temperatura = c.Decimal(precision: 18, scale: 2),
                        FrecuenciaCardiaca = c.Decimal(precision: 18, scale: 2),
                        FrecuenciaRespiratoria = c.Decimal(precision: 18, scale: 2),
                        Peso = c.Decimal(precision: 18, scale: 2),
                        Altura = c.Decimal(precision: 18, scale: 2),
                        IMC = c.Decimal(precision: 18, scale: 2),
                        Saturacion = c.String(maxLength: 20),
                        Diagnostico = c.String(maxLength: 2000),
                        EnfermedadId = c.Int(),
                        Tratamiento = c.String(maxLength: 2000),
                        Observaciones = c.String(maxLength: 2000),
                        IndicacionesMedicas = c.String(maxLength: 2000),
                        CostoConsulta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstadoPago = c.String(nullable: false, maxLength: 20),
                        FechaPago = c.DateTime(),
                        VentaId = c.Int(),
                        ProximaCita = c.DateTime(),
                        NotasProximaCita = c.String(maxLength: 500),
                        Estado = c.String(nullable: false, maxLength: 20),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ConsultaId)
                .ForeignKey("dbo.TCita", t => t.CitaId)
                .ForeignKey("dbo.TDoctor", t => t.DoctorId)
                .ForeignKey("dbo.TEnfermedad", t => t.EnfermedadId)
                .ForeignKey("dbo.TExpediente", t => t.ExpedienteId)
                .ForeignKey("dbo.TVenta", t => t.VentaId)
                .Index(t => t.ExpedienteId)
                .Index(t => t.CitaId)
                .Index(t => t.DoctorId)
                .Index(t => t.EnfermedadId)
                .Index(t => t.VentaId);
            
            CreateTable(
                "dbo.TDiagnostico",
                c => new
                    {
                        DiagnosticoId = c.Int(nullable: false, identity: true),
                        ExpedienteId = c.Int(nullable: false),
                        EnfermedadId = c.Int(nullable: false),
                        ConsultaId = c.Int(),
                        FechaDiagnostico = c.DateTime(nullable: false),
                        Observaciones = c.String(),
                        Estado = c.String(maxLength: 20),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosticoId)
                .ForeignKey("dbo.TConsulta", t => t.ConsultaId)
                .ForeignKey("dbo.TEnfermedad", t => t.EnfermedadId)
                .ForeignKey("dbo.TExpediente", t => t.ExpedienteId)
                .Index(t => t.ExpedienteId)
                .Index(t => t.EnfermedadId)
                .Index(t => t.ConsultaId);
            
            CreateTable(
                "dbo.TEnfermedad",
                c => new
                    {
                        EnfermedadId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Sintomas = c.String(maxLength: 1000),
                        Tratamiento = c.String(maxLength: 1000),
                        Tipo = c.String(maxLength: 100),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EnfermedadId);
            
            CreateTable(
                "dbo.TReceta",
                c => new
                    {
                        RecetaId = c.Int(nullable: false, identity: true),
                        PacienteId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        CitaId = c.Int(),
                        ConsultaId = c.Int(),
                        NumeroReceta = c.String(nullable: false, maxLength: 50),
                        FechaEmision = c.DateTime(nullable: false),
                        FechaVencimiento = c.DateTime(),
                        Diagnostico = c.String(maxLength: 500),
                        IndicacionesGenerales = c.String(maxLength: 1000),
                        Estado = c.String(nullable: false, maxLength: 20),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RecetaId)
                .ForeignKey("dbo.TCita", t => t.CitaId)
                .ForeignKey("dbo.TConsulta", t => t.ConsultaId)
                .ForeignKey("dbo.TDoctor", t => t.DoctorId)
                .ForeignKey("dbo.TPaciente", t => t.PacienteId)
                .Index(t => t.PacienteId)
                .Index(t => t.DoctorId)
                .Index(t => t.CitaId)
                .Index(t => t.ConsultaId);
            
            CreateTable(
                "dbo.TDetalleReceta",
                c => new
                    {
                        DetalleRecetaId = c.Int(nullable: false, identity: true),
                        RecetaId = c.Int(nullable: false),
                        MedicamentoId = c.Int(),
                        NombreMedicamentoExterno = c.String(maxLength: 300),
                        CantidadPrescrita = c.Int(nullable: false),
                        Dosis = c.String(nullable: false, maxLength: 200),
                        DuracionDias = c.Int(nullable: false),
                        Indicaciones = c.String(maxLength: 500),
                        CantidadSurtida = c.Int(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DetalleRecetaId)
                .ForeignKey("dbo.TMedicamento", t => t.MedicamentoId)
                .ForeignKey("dbo.TReceta", t => t.RecetaId)
                .Index(t => t.RecetaId)
                .Index(t => t.MedicamentoId);
            
            CreateTable(
                "dbo.TMedicamento",
                c => new
                    {
                        MedicamentoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Presentacion = c.String(maxLength: 50),
                        Dosis = c.String(maxLength: 100),
                        Proveedor = c.String(maxLength: 100),
                        FechaVencimiento = c.DateTime(nullable: false),
                        Stock = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MedicamentoId);
            
            CreateTable(
                "dbo.TDetalleFactura",
                c => new
                    {
                        DetalleId = c.Int(nullable: false, identity: true),
                        FacturaId = c.Int(nullable: false),
                        MedicamentoId = c.Int(),
                        Descripcion = c.String(maxLength: 500),
                        Cantidad = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DetalleId)
                .ForeignKey("dbo.TFactura", t => t.FacturaId)
                .ForeignKey("dbo.TMedicamento", t => t.MedicamentoId)
                .Index(t => t.FacturaId)
                .Index(t => t.MedicamentoId);
            
            CreateTable(
                "dbo.TFactura",
                c => new
                    {
                        VentaId = c.Int(nullable: false),
                        NumeroFactura = c.String(nullable: false, maxLength: 100),
                        Fecha = c.DateTime(nullable: false),
                        PacienteId = c.Int(),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Impuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MetodoPago = c.String(nullable: false, maxLength: 50),
                        Observaciones = c.String(maxLength: 500),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VentaId)
                .ForeignKey("dbo.TPaciente", t => t.PacienteId)
                .ForeignKey("dbo.TVenta", t => t.VentaId)
                .Index(t => t.VentaId)
                .Index(t => t.PacienteId);
            
            CreateTable(
                "dbo.TPaciente",
                c => new
                    {
                        PacienteId = c.Int(nullable: false, identity: true),
                        DNI = c.String(nullable: false, maxLength: 15),
                        NombreCompleto = c.String(nullable: false, maxLength: 100),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Sexo = c.String(maxLength: 10),
                        Telefono = c.String(maxLength: 20),
                        Direccion = c.String(maxLength: 200),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PacienteId);
            
            CreateTable(
                "dbo.TVenta",
                c => new
                    {
                        VentaId = c.Int(nullable: false, identity: true),
                        NumeroVenta = c.String(nullable: false, maxLength: 50),
                        FechaVenta = c.DateTime(nullable: false),
                        RecetaId = c.Int(),
                        PacienteId = c.Int(),
                        DoctorId = c.Int(),
                        UsuarioVentaId = c.Int(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Impuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MetodoPago1 = c.String(maxLength: 20),
                        MontoPago1 = c.Decimal(precision: 18, scale: 2),
                        MetodoPago2 = c.String(maxLength: 20),
                        MontoPago2 = c.Decimal(precision: 18, scale: 2),
                        MontoPagado = c.Decimal(precision: 18, scale: 2),
                        Cambio = c.Decimal(precision: 18, scale: 2),
                        TipoVenta = c.String(nullable: false, maxLength: 20),
                        Estado = c.String(nullable: false, maxLength: 20),
                        Observaciones = c.String(maxLength: 500),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VentaId)
                .ForeignKey("dbo.TDoctor", t => t.DoctorId)
                .ForeignKey("dbo.TPaciente", t => t.PacienteId)
                .ForeignKey("dbo.TReceta", t => t.RecetaId)
                .ForeignKey("dbo.TUsuario", t => t.UsuarioVentaId)
                .Index(t => t.RecetaId)
                .Index(t => t.PacienteId)
                .Index(t => t.DoctorId)
                .Index(t => t.UsuarioVentaId);
            
            CreateTable(
                "dbo.TDetalleVenta",
                c => new
                    {
                        DetalleVentaId = c.Int(nullable: false, identity: true),
                        VentaId = c.Int(nullable: false),
                        MedicamentoId = c.Int(nullable: false),
                        DetalleRecetaId = c.Int(),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DetalleVentaId)
                .ForeignKey("dbo.TDetalleReceta", t => t.DetalleRecetaId)
                .ForeignKey("dbo.TMedicamento", t => t.MedicamentoId)
                .ForeignKey("dbo.TVenta", t => t.VentaId)
                .Index(t => t.VentaId)
                .Index(t => t.MedicamentoId)
                .Index(t => t.DetalleRecetaId);
            
            CreateTable(
                "dbo.TUsuario",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        NombreUsuario = c.String(nullable: false, maxLength: 50),
                        Contrasena = c.String(nullable: false),
                        HuellaDactilar = c.Binary(),
                        RolId = c.Int(nullable: false),
                        FechaRegistro = c.DateTime(nullable: false),
                        UltimoAcceso = c.DateTime(),
                        Estado = c.Boolean(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.TRol", t => t.RolId)
                .Index(t => t.RolId);
            
            CreateTable(
                "dbo.TRol",
                c => new
                    {
                        RolId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RolId);
            
            CreateTable(
                "dbo.TInventarioMovimiento",
                c => new
                    {
                        MovimientoId = c.Int(nullable: false, identity: true),
                        MedicamentoId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        TipoMovimiento = c.String(nullable: false, maxLength: 20),
                        StockAnterior = c.Int(nullable: false),
                        StockNuevo = c.Int(nullable: false),
                        Motivo = c.String(maxLength: 200),
                        UsuarioRegistro = c.String(maxLength: 100),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MovimientoId)
                .ForeignKey("dbo.TMedicamento", t => t.MedicamentoId)
                .Index(t => t.MedicamentoId);
            
            CreateTable(
                "dbo.TExamen",
                c => new
                    {
                        ExamenId = c.Int(nullable: false, identity: true),
                        ExpedienteId = c.Int(nullable: false),
                        CitaId = c.Int(),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Tipo = c.String(nullable: false, maxLength: 100),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaSolicitud = c.DateTime(nullable: false),
                        FechaResultado = c.DateTime(),
                        Resultado = c.String(),
                        Estado = c.String(maxLength: 50),
                        EsExterno = c.Boolean(nullable: false),
                        LugarRealizacion = c.String(maxLength: 200),
                        VentaId = c.Int(),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ExamenId)
                .ForeignKey("dbo.TCita", t => t.CitaId)
                .ForeignKey("dbo.TExpediente", t => t.ExpedienteId)
                .ForeignKey("dbo.TVenta", t => t.VentaId)
                .Index(t => t.ExpedienteId)
                .Index(t => t.CitaId)
                .Index(t => t.VentaId);
            
            CreateTable(
                "dbo.THorario",
                c => new
                    {
                        HorarioDoctorId = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        DiaSemana = c.Int(nullable: false),
                        HoraInicio = c.Time(nullable: false, precision: 7),
                        HoraFin = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.HorarioDoctorId)
                .ForeignKey("dbo.TDoctor", t => t.DoctorId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.TEmpresa",
                c => new
                    {
                        EmpresaId = c.Int(nullable: false, identity: true),
                        NombreEmpresa = c.String(nullable: false, maxLength: 200),
                        RTN = c.String(maxLength: 20),
                        Direccion = c.String(maxLength: 300),
                        Telefono = c.String(maxLength: 50),
                        Email = c.String(maxLength: 100),
                        SitioWeb = c.String(maxLength: 100),
                        Logo = c.Binary(),
                        Slogan = c.String(maxLength: 500),
                        RepresentanteLegal = c.String(maxLength: 100),
                        FechaActualizacion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmpresaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TDoctor", "UsuarioId", "dbo.TUsuario");
            DropForeignKey("dbo.THorario", "DoctorId", "dbo.TDoctor");
            DropForeignKey("dbo.TExpediente", "TDoctor_DoctorId", "dbo.TDoctor");
            DropForeignKey("dbo.TExamen", "VentaId", "dbo.TVenta");
            DropForeignKey("dbo.TExamen", "ExpedienteId", "dbo.TExpediente");
            DropForeignKey("dbo.TExamen", "CitaId", "dbo.TCita");
            DropForeignKey("dbo.TConsulta", "VentaId", "dbo.TVenta");
            DropForeignKey("dbo.TReceta", "PacienteId", "dbo.TPaciente");
            DropForeignKey("dbo.TReceta", "DoctorId", "dbo.TDoctor");
            DropForeignKey("dbo.TDetalleReceta", "RecetaId", "dbo.TReceta");
            DropForeignKey("dbo.TDetalleReceta", "MedicamentoId", "dbo.TMedicamento");
            DropForeignKey("dbo.TInventarioMovimiento", "MedicamentoId", "dbo.TMedicamento");
            DropForeignKey("dbo.TDetalleFactura", "MedicamentoId", "dbo.TMedicamento");
            DropForeignKey("dbo.TVenta", "UsuarioVentaId", "dbo.TUsuario");
            DropForeignKey("dbo.TUsuario", "RolId", "dbo.TRol");
            DropForeignKey("dbo.TVenta", "RecetaId", "dbo.TReceta");
            DropForeignKey("dbo.TVenta", "PacienteId", "dbo.TPaciente");
            DropForeignKey("dbo.TFactura", "VentaId", "dbo.TVenta");
            DropForeignKey("dbo.TVenta", "DoctorId", "dbo.TDoctor");
            DropForeignKey("dbo.TDetalleVenta", "VentaId", "dbo.TVenta");
            DropForeignKey("dbo.TDetalleVenta", "MedicamentoId", "dbo.TMedicamento");
            DropForeignKey("dbo.TDetalleVenta", "DetalleRecetaId", "dbo.TDetalleReceta");
            DropForeignKey("dbo.TFactura", "PacienteId", "dbo.TPaciente");
            DropForeignKey("dbo.TExpediente", "PacienteId", "dbo.TPaciente");
            DropForeignKey("dbo.TCita", "PacienteId", "dbo.TPaciente");
            DropForeignKey("dbo.TDetalleFactura", "FacturaId", "dbo.TFactura");
            DropForeignKey("dbo.TReceta", "ConsultaId", "dbo.TConsulta");
            DropForeignKey("dbo.TReceta", "CitaId", "dbo.TCita");
            DropForeignKey("dbo.TConsulta", "ExpedienteId", "dbo.TExpediente");
            DropForeignKey("dbo.TConsulta", "EnfermedadId", "dbo.TEnfermedad");
            DropForeignKey("dbo.TConsulta", "DoctorId", "dbo.TDoctor");
            DropForeignKey("dbo.TDiagnostico", "ExpedienteId", "dbo.TExpediente");
            DropForeignKey("dbo.TDiagnostico", "EnfermedadId", "dbo.TEnfermedad");
            DropForeignKey("dbo.TDiagnostico", "ConsultaId", "dbo.TConsulta");
            DropForeignKey("dbo.TConsulta", "CitaId", "dbo.TCita");
            DropForeignKey("dbo.TCita", "DoctorId", "dbo.TDoctor");
            DropIndex("dbo.THorario", new[] { "DoctorId" });
            DropIndex("dbo.TExamen", new[] { "VentaId" });
            DropIndex("dbo.TExamen", new[] { "CitaId" });
            DropIndex("dbo.TExamen", new[] { "ExpedienteId" });
            DropIndex("dbo.TInventarioMovimiento", new[] { "MedicamentoId" });
            DropIndex("dbo.TUsuario", new[] { "RolId" });
            DropIndex("dbo.TDetalleVenta", new[] { "DetalleRecetaId" });
            DropIndex("dbo.TDetalleVenta", new[] { "MedicamentoId" });
            DropIndex("dbo.TDetalleVenta", new[] { "VentaId" });
            DropIndex("dbo.TVenta", new[] { "UsuarioVentaId" });
            DropIndex("dbo.TVenta", new[] { "DoctorId" });
            DropIndex("dbo.TVenta", new[] { "PacienteId" });
            DropIndex("dbo.TVenta", new[] { "RecetaId" });
            DropIndex("dbo.TFactura", new[] { "PacienteId" });
            DropIndex("dbo.TFactura", new[] { "VentaId" });
            DropIndex("dbo.TDetalleFactura", new[] { "MedicamentoId" });
            DropIndex("dbo.TDetalleFactura", new[] { "FacturaId" });
            DropIndex("dbo.TDetalleReceta", new[] { "MedicamentoId" });
            DropIndex("dbo.TDetalleReceta", new[] { "RecetaId" });
            DropIndex("dbo.TReceta", new[] { "ConsultaId" });
            DropIndex("dbo.TReceta", new[] { "CitaId" });
            DropIndex("dbo.TReceta", new[] { "DoctorId" });
            DropIndex("dbo.TReceta", new[] { "PacienteId" });
            DropIndex("dbo.TDiagnostico", new[] { "ConsultaId" });
            DropIndex("dbo.TDiagnostico", new[] { "EnfermedadId" });
            DropIndex("dbo.TDiagnostico", new[] { "ExpedienteId" });
            DropIndex("dbo.TConsulta", new[] { "VentaId" });
            DropIndex("dbo.TConsulta", new[] { "EnfermedadId" });
            DropIndex("dbo.TConsulta", new[] { "DoctorId" });
            DropIndex("dbo.TConsulta", new[] { "CitaId" });
            DropIndex("dbo.TConsulta", new[] { "ExpedienteId" });
            DropIndex("dbo.TExpediente", new[] { "TDoctor_DoctorId" });
            DropIndex("dbo.TExpediente", new[] { "PacienteId" });
            DropIndex("dbo.TDoctor", new[] { "UsuarioId" });
            DropIndex("dbo.TCita", new[] { "DoctorId" });
            DropIndex("dbo.TCita", new[] { "PacienteId" });
            DropTable("dbo.TEmpresa");
            DropTable("dbo.THorario");
            DropTable("dbo.TExamen");
            DropTable("dbo.TInventarioMovimiento");
            DropTable("dbo.TRol");
            DropTable("dbo.TUsuario");
            DropTable("dbo.TDetalleVenta");
            DropTable("dbo.TVenta");
            DropTable("dbo.TPaciente");
            DropTable("dbo.TFactura");
            DropTable("dbo.TDetalleFactura");
            DropTable("dbo.TMedicamento");
            DropTable("dbo.TDetalleReceta");
            DropTable("dbo.TReceta");
            DropTable("dbo.TEnfermedad");
            DropTable("dbo.TDiagnostico");
            DropTable("dbo.TConsulta");
            DropTable("dbo.TExpediente");
            DropTable("dbo.TDoctor");
            DropTable("dbo.TCita");
        }
    }
}
