using System.Diagnostics;
using Models;

class InvalidCaloriasException : Exception
{
    public InvalidCaloriasException(string message = "") : base(message) { }
}

public class Postre : Producto
{
    public int Calorias { get; set; }

    public Postre() { } // Constructor sin parámetros

    public Postre(string nombre, double precio, int calorias) : base(nombre, precio)
    {
        Calorias = calorias;

        if (calorias <= 0)
        {
            throw new InvalidCaloriasException("Las calorías deben ser mayores a cero");
        }
    }

    public override void MostrarDetalles()
    {
        Console.WriteLine($"Postre: {Nombre}, Precio {Precio:C}, Calorías {Calorias}");
    }

    /*
    public override string MostrarDetallesGuardado()
    {
        var message = $"Postre|{Nombre}|{Precio}|{Calorias}";
        return message;
    }
    */
}