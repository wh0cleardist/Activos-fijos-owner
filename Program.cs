// Gestión de Activos Fijos - Versión Consola
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    // Modelo de Activo Fijo
    public class ActivoFijo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal ValorCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public string Estado { get; set; }

        public override string ToString() =>
            $"{Codigo} - {Nombre} (Valor: {ValorCompra:C}, Estado: {Estado})";
    }

    // Base de datos en memoria
    public static class Database
    {
        private static List<ActivoFijo> _activos = new List<ActivoFijo>();
        private static int _nextId = 1;

        public static void AgregarActivo(ActivoFijo activo)
        {
            activo.Id = _nextId++;
            _activos.Add(activo);
        }

        public static List<ActivoFijo> ObtenerActivos() => _activos;

        public static ActivoFijo ObtenerPorId(int id) =>
            _activos.FirstOrDefault(a => a.Id == id);
    }

    // Menú principal
    public static void Main()
    {
        // Datos de ejemplo
        Database.AgregarActivo(new ActivoFijo
        {
            Codigo = "AF-001",
            Nombre = "Computadora",
            ValorCompra = 1500,
            FechaCompra = DateTime.Now.AddYears(-1),
            Estado = "Activo"
        });

        Database.AgregarActivo(new ActivoFijo
        {
            Codigo = "AF-002",
            Nombre = "Impresora",
            ValorCompra = 800,
            FechaCompra = DateTime.Now.AddMonths(-6),
            Estado = "En mantenimiento"
        });

        while (true)
        {
            Console.Clear();
            Console.WriteLine("SISTEMA DE GESTIÓN DE ACTIVOS FIJOS");
            Console.WriteLine("1. Listar activos");
            Console.WriteLine("2. Agregar activo");
            Console.WriteLine("3. Buscar activo");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    ListarActivos();
                    break;
                case "2":
                    AgregarActivo();
                    break;
                case "3":
                    BuscarActivo();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opción no válida");
                    break;
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }

    private static void ListarActivos()
    {
        Console.WriteLine("\nLISTADO DE ACTIVOS:");
        foreach (var activo in Database.ObtenerActivos())
        {
            Console.WriteLine(activo);
        }
    }

    private static void AgregarActivo()
    {
        Console.WriteLine("\nNUEVO ACTIVO:");
        var activo = new ActivoFijo();

        Console.Write("Código: ");
        activo.Codigo = Console.ReadLine();

        Console.Write("Nombre: ");
        activo.Nombre = Console.ReadLine();

        Console.Write("Valor de compra: ");
        decimal.TryParse(Console.ReadLine(), out decimal valor);
        activo.ValorCompra = valor;

        activo.FechaCompra = DateTime.Now;
        activo.Estado = "Activo";

        Database.AgregarActivo(activo);
        Console.WriteLine("Activo agregado correctamente!");
    }

    private static void BuscarActivo()
    {
        Console.Write("\nIngrese ID del activo: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var activo = Database.ObtenerPorId(id);
            if (activo != null)
            {
                Console.WriteLine("\nDETALLE DEL ACTIVO:");
                Console.WriteLine($"ID: {activo.Id}");
                Console.WriteLine($"Código: {activo.Codigo}");
                Console.WriteLine($"Nombre: {activo.Nombre}");
                Console.WriteLine($"Valor: {activo.ValorCompra:C}");
                Console.WriteLine($"Fecha compra: {activo.FechaCompra:d}");
                Console.WriteLine($"Estado: {activo.Estado}");
            }
            else
            {
                Console.WriteLine("Activo no encontrado");
            }
        }
        else
        {
            Console.WriteLine("ID no válido");
        }
    }
}