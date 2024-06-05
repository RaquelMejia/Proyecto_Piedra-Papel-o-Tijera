using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace JuegoPiedraPapelTijera
{
    public class Juego
    {
        private int victoriasJugador;
        private int victoriasComputadora;
        private int empates;

        public Juego()
        {
            victoriasJugador = 0;
            victoriasComputadora = 0;
            empates = 0;
        }

        public void Iniciar()
        {
            bool jugarDeNuevo = true;

            while (jugarDeNuevo)
            {
                Console.Clear();
                Opcion eleccionJugador = MostrarMenuOpciones();
                Opcion eleccionComputadora = GenerarOpcionComputadora();

                AnsiConsole.WriteLine($"\nTú elegiste:\n{ObtenerFigura(eleccionJugador)}");
                AnsiConsole.WriteLine($"\nAstro eligio:\n{ObtenerFigura(eleccionComputadora)}");

                DeterminarGanador(eleccionJugador, eleccionComputadora);

                MostrarResultados();

                bool opcionValida = false;
                while (!opcionValida)
                {
                    try
                    {
                        jugarDeNuevo = AnsiConsole.Confirm("¿Quieres jugar de nuevo?");
                        opcionValida = true;

                    }
                    catch (InvalidOperationException)
                    {
                        AnsiConsole.WriteLine("Opción no válida. Por favor, elige 'sí' o 'no'.");
                    }
                }
            }

            AnsiConsole.WriteLine("Gracias por jugar. ¡Adiós!");
        }

        public Opcion MostrarMenuOpciones()
        {
            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Elige una opción:")
                    .AddChoices("1. Piedra", "2. Papel", "3. Tijera"));

            return opcion switch
            {
                "1. Piedra" => Opcion.Piedra,
                "2. Papel" => Opcion.Papel,
                "3. Tijera" => Opcion.Tijera,
                _ => throw new InvalidOperationException("Opción no válida")
            };
        }

        public Opcion GenerarOpcionComputadora()
        {
            Random random = new Random();
            return (Opcion)random.Next(1, 4);
        }

        public void DeterminarGanador(Opcion eleccionJugador, Opcion eleccionComputadora)
        {
            if (eleccionJugador == eleccionComputadora)
            {
                AnsiConsole.WriteLine("Es un empate!");
                if (OperatingSystem.IsWindows())
                {
                    SoundPlayer soundPlayer = new SoundPlayer("C:\\Users\\MINEDUCYT\\OneDrive\\Escritorio\\Proyecto_Piedra-Papel-o-Tijera\\JuegoPiedraPapelTijeraDEMO\\JuegoPiedraPapelTijera\\Sonido\\Empate.wav");
                    soundPlayer.PlaySync();

                }

                empates++;
            }
            else if (eleccionJugador == Opcion.Piedra && eleccionComputadora == Opcion.Tijera ||
                     eleccionJugador == Opcion.Papel && eleccionComputadora == Opcion.Piedra ||
                     eleccionJugador == Opcion.Tijera && eleccionComputadora == Opcion.Papel)
            {
                AnsiConsole.WriteLine("¡Ganaste!");
                if (OperatingSystem.IsWindows())
                {
                    SoundPlayer soundPlayer = new SoundPlayer("C:\\Users\\MINEDUCYT\\OneDrive\\Escritorio\\Proyecto_Piedra-Papel-o-Tijera\\JuegoPiedraPapelTijeraDEMO\\JuegoPiedraPapelTijera\\Sonido\\Ganaste.wav");
                    soundPlayer.PlaySync();

                }
                victoriasJugador++;
            }
            else
            {
                AnsiConsole.WriteLine("Perdiste.");
                if (OperatingSystem.IsWindows())
                {
                    SoundPlayer soundPlayer = new SoundPlayer("C:\\Users\\MINEDUCYT\\OneDrive\\Escritorio\\Proyecto_Piedra-Papel-o-Tijera\\JuegoPiedraPapelTijeraDEMO\\JuegoPiedraPapelTijera\\Sonido\\Perdiste.wav");
                    soundPlayer.PlaySync();

                }
                victoriasComputadora++;
            }
        }

        public string ObtenerFigura(Opcion opcion)
        {
            return opcion switch
            {
                Opcion.Piedra => @"
    _______
---'   ____)
      (_____)
      (_____)
      (____)
---.__(___)
",
                Opcion.Papel => @"
    _______
---'   ____)____
          ______)
          _______)
         _______)
---.__________)
",
                Opcion.Tijera => @"
    _______
---'   ____)____
          ______)
       __________)
      (____)
---.__(___)
",
                _ => string.Empty
            };
        }

        public void MostrarResultados()
        {
            var tabla = new Table().Border(TableBorder.Rounded)
                                    .BorderColor(Color.Blue)
                                    .AddColumn(new TableColumn("[bold]Resultados[/]").Centered())
                                    .AddColumn(new TableColumn("[bold]Victorias[/]").Centered())
                                    .AddColumn(new TableColumn("[bold]Derrotas[/]").Centered())
                                    .AddColumn(new TableColumn("[bold]Empates[/]").Centered());


            tabla.AddRow(
                new Markup("[yellow]Total[/]"),
                new Markup($"[green]{victoriasJugador}[/]"),
                new Markup($"[red]{victoriasComputadora}[/]"),
                new Markup($"[blue]{empates}[/]")
            );

            AnsiConsole.WriteLine();
            AnsiConsole.Render(tabla);
        }
    }

}

