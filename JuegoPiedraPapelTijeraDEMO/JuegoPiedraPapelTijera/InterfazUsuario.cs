using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoPiedraPapelTijera
{
    public interface InterfazUsuario
    {
        public void MostrarMenuPrincipal()
        {
            var menuPrincipal = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("┌──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┐\r\n│[bold blue]****************************************************[/]BIENVENIDO[bold blue]********************************************************[/]│\r\n└──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘" +
                    "\n\r\n¡Bienvenido al juego de Piedra, Papel o Tijera! Soy Astro, tu oponente ¡Prepárate para divertirte y retar tus habilidades! Que gane el mejor ¡y que empiece la diversión!")
                    .AddChoices("1. Instrucciones", "2. Jugar", "3. Salir"));

            switch (menuPrincipal)
            {

                case "1. Instrucciones":
                    MostrarAyuda();
                    break;
                case "2. Jugar":
                    var juego = new Juego();
                    juego.Iniciar();
                    break;
                case "3. Salir":

                    AnsiConsole.WriteLine("Gracias por jugar. ¡Adiós!");
                    break;

            }

        }


        public void MostrarAyuda()
        {
            string[] instrucciones = {
          "[bold blue]!!!!!INSTRUCCIONES!!!!!!.[/]\n",
          "\nPiedra vence a tijera.\n",
          "Papel vence a piedra.\n",
          "Tijera vence a papel.\n",
          "Si ambos jugadores eligen la misma opción (por ejemplo, ambos eligen piedra) entonces es un empate.\n"
      };


            foreach (var linea in instrucciones)
            {
                AnsiConsole.Markup($"{linea}");
                Thread.Sleep(500);
            }


            AnsiConsole.Markup("[bold blue]\nPresiona cualquier tecla para volver al menú principal...[/]\n");
            Console.ReadKey();
            MostrarMenuPrincipal();
        }

    }
}
