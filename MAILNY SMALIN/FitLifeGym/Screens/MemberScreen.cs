using Spectre.Console;
using FitLifeGym.Services;
using FitLifeGym.Models;
using System;
using System.Collections.Generic;

namespace FitLifeGym.Screens
{
    public class MemberScreen
    {
        private readonly IMiembroService _miembroService;

        public MemberScreen(IMiembroService miembroService)
        {
            _miembroService = miembroService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new FigletText("FitLife Gym").Centered().Color(Color.Red1));
                AnsiConsole.Write(new Rule("[red]Gestión de Miembros[/]").RuleStyle("grey").LeftJustified());
                AnsiConsole.WriteLine();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold grey]¿Qué desea hacer?[/]")
                        .PageSize(10)
                        .HighlightStyle(new Style(foreground: Color.Red1, decoration: Decoration.Bold))
                        .AddChoices(new[] {
                            "1. Registrar nuevo miembro",
                            "2. Listar todos los miembros",
                            "3. Buscar miembro por cédula",
                            "4. Actualizar teléfono de miembro",
                            "5. Eliminar un miembro",
                            "6. Salir"
                        }));

                switch (choice)
                {
                    case "1. Registrar nuevo miembro":
                        RegistrarMiembro();
                        break;
                    case "2. Listar todos los miembros":
                        ListarMiembros();
                        break;
                    case "3. Buscar miembro por cédula":
                        BuscarMiembro();
                        break;
                    case "4. Actualizar teléfono de miembro":
                        ActualizarTelefono();
                        break;
                    case "5. Eliminar un miembro":
                        EliminarMiembro();
                        break;
                    case "6. Salir":
                        return;
                }
                AnsiConsole.MarkupLine("\n[grey]Presiona cualquier tecla para continuar...[/]");
                Console.ReadKey();
            }
        }

        private void RegistrarMiembro()
        {
            AnsiConsole.MarkupLine("[bold blue]>>> Registrar Miembro[/]");
            var cedula = AnsiConsole.Ask<string>("Número de cédula:");
            var nombre = AnsiConsole.Ask<string>("Nombre completo:");
            var tel = AnsiConsole.Ask<string>("Teléfono:");

            _miembroService.Registrar(new Miembro 
            { 
               Cedula = cedula, 
               NombreCompleto = nombre, 
               Telefono = tel 
            });

            AnsiConsole.MarkupLine("[bold green]¡Miembro registrado con éxito![/]");
        }

        private void ListarMiembros()
        {
            AnsiConsole.MarkupLine("[bold blue]>>> Lista de Miembros[/]");
            var miembros = _miembroService.ObtenerTodos();

            if (miembros.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No hay miembros registrados todavía.[/]");
                return;
            }

            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("Cédula");
            table.AddColumn("Nombre Completo");
            table.AddColumn("Teléfono");

            foreach (var m in miembros)
            {
                table.AddRow(m.Cedula, m.NombreCompleto, m.Telefono);
            }

            AnsiConsole.Write(table);
        }

        private void BuscarMiembro()
        {
            AnsiConsole.MarkupLine("[bold blue]>>> Buscar Miembro[/]");
            var cedula = AnsiConsole.Ask<string>("Ingrese la cédula a buscar:");
            var miembro = _miembroService.BuscarPorCedula(cedula);

            if (miembro != null)
            {
                AnsiConsole.MarkupLine($"[green]Encontrado:[/]");
                AnsiConsole.Write(new Panel(miembro.ToString()).Header("Detalles").BorderColor(Color.Green1));
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Miembro no encontrado.[/]");
            }
        }

        private void ActualizarTelefono()
        {
            AnsiConsole.MarkupLine("[bold blue]>>> Actualizar Teléfono[/]");
            var cedula = AnsiConsole.Ask<string>("Cédula del miembro:");
            var miembro = _miembroService.BuscarPorCedula(cedula);

            if (miembro != null)
            {
                var nuevoTel = AnsiConsole.Ask<string>("Nuevo teléfono:");
                _miembroService.ActualizarTel(cedula, nuevoTel);
                AnsiConsole.MarkupLine("[bold green]¡Teléfono actualizado con éxito![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Miembro no encontrado.[/]");
            }
        }

        private void EliminarMiembro()
        {
            AnsiConsole.MarkupLine("[bold blue]>>> Eliminar Miembro[/]");
            var cedula = AnsiConsole.Ask<string>("Cédula del miembro a eliminar:");
            var miembro = _miembroService.BuscarPorCedula(cedula);

            if (miembro != null)
            {
                if (AnsiConsole.Confirm($"¿Está seguro que desea eliminar a {miembro.NombreCompleto}?"))
                {
                    _miembroService.BorrarMiembro(cedula);
                    AnsiConsole.MarkupLine("[bold green]¡Miembro eliminado![/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Miembro no encontrado.[/]");
            }
        }
    }
}
