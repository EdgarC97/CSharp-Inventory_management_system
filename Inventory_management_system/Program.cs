//*****************************************************************************//

//Creacion de clase principal: Producto
public class Product
{
    public string Name { get; set; }
    public double UnitPrice { get; set; }
    public int AvailableQuantity { get; set; }

    // Constructor de clase - Creacion de instancia (objeto específico creado a partir de una clase)
    public Product(string name, double unitPrice, int availableQuantity)
    {
        Name = name;
        UnitPrice = unitPrice;
        AvailableQuantity = availableQuantity;
    }
    //Sobreescribo con metodo ToString() para obtener representacion visual legible
    public override string ToString()
    {
        return $"Name: {Name}, Unit Price: {UnitPrice:C}, Avaliable Quantity: {AvailableQuantity}";
    }
}

class Program
{
    //Punto de entrada de la aplicacion
    static void Main(string[] args)
    {
        //Creacion de lista que contiene objetos de la clase Product
        List<Product> inventory = new List<Product>()
        {
            new Product ("Arepa", 90, 10),
            new Product ("Pan", 20, 15),
            new Product ("Huevo", 15, 20)
        };

        //Creacion de menu en bucle while
        bool flag = false;

        while (!flag)
        {
            Console.Clear(); // Limpiar la consola

            Console.WriteLine(
@"###############################################################################################
#                             SISTEMA DE GESTION DE INVENTARIOS                               #
###############################################################################################");
            Console.WriteLine("\n1. Agregar producto");
            Console.WriteLine("2. Actualizar stock de producto");
            Console.WriteLine("3. Buscar producto");
            Console.WriteLine("4. Mostrar inventario");
            Console.WriteLine("5. Eliminar producto");
            Console.WriteLine("6. Salir");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.Write("Selecciona una opcion --> ");

            //Switch para manejar las opciones ingresadas por el usuario
            switch (Console.ReadLine())
            {
                case "1":
                    AddProduct(inventory);
                    break;
                case "2":
                    UpdateStock(inventory);
                    break;
                case "3":
                    SearchProduct(inventory);
                    break;
                case "4":
                    DisplayInventory(inventory);
                    break;
                case "5":
                    DeleteProduct(inventory);
                    break;
                case "6":
                    Console.WriteLine("Saliendo del programa...");
                    Console.WriteLine("Gracias por usar el gestor de inventarios.");
                    flag = true;
                    break;
                default:
                    Console.WriteLine("Opcion invalida, intenta de nuevo");
                    pressKey();
                    break;
            }
        }
    }

    //Funcion para devolver al usuario al menu 
    public static void pressKey()
    {
        // Esperar a que el usuario presione cualquier tecla para continuar
        Console.WriteLine("\nPresiona cualquier tecla para volver al menú principal...");
        Console.ReadKey(true);
    }

    //Metodo para agregar un producto al inventario
    public static void AddProduct(List<Product> inventory)
    {
        Console.Clear(); // Limpiar la consola
        Console.WriteLine(
@"###############################################################################################
#                                    AGREGAR UN PRODUCTO                                      #
###############################################################################################");
        //Se piden datos del producto al usuario

        //Se pide el nombre
        Console.WriteLine();
        Console.Write("Ingresa el nombre del producto --> ");
        string? name = Console.ReadLine();
        //Verifico que el nombre no sea nulo o tenga espacios en blanco
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("El nombre no puede estar vacio");
            return;
        }
        //Se pide el precio unitaria
        Console.Write("Ingresa el precio unitario del producto --> ");
        //Verifico que el precio no sea invalido
        if (!double.TryParse(Console.ReadLine(), out double unitPrice))
        {
            Console.WriteLine("Precio unitario invalido");
            return;
        }
        //Se pide la cantidad
        Console.Write("Ingresa la cantidad del producto --> ");
        if (!int.TryParse(Console.ReadLine(), out int availableQuantity))
        {
            Console.WriteLine("Cantidad invalida");
            return;
        }

        //Agregamos el producto al inventario
        Product? product = new Product(name, unitPrice, availableQuantity);
        inventory.Add(product);

        Console.WriteLine("Producto agregado satisfactoriamente");

        //Devolver al menu principal 
        pressKey();
    }

    //Metodo para actualizar stock de productos por nombre
    public static void UpdateStock(List<Product> inventory)
    {
        Console.Clear(); // Limpiar la consola

        //Layout del inventario
        InventoryLayout(inventory);

        Console.WriteLine(
@"###############################################################################################
#                             ACTUALIZAR STOCK DE UN PRODUCTO                                 #
###############################################################################################");
        Console.WriteLine();
        Console.Write("Ingresa el Nro del producto que deseas actualizar --> ");
        string? input = Console.ReadLine();

        //Se verifica que el input sea un numero valido
        if (int.TryParse(input, out int productIndex) && productIndex >= 0 && productIndex < inventory.Count)
        {
            //Accedo al producto usando el indice
            Product product = inventory[productIndex];

            Console.Write($"Ingrese la nueva cantidad para {product.Name} --> ");
            int availableQuantity = Convert.ToInt32(Console.ReadLine());

            //Actualizamos la cantidad en el inventario
            product.AvailableQuantity = availableQuantity;
            Console.WriteLine("Cantidad en stock actualizada correctamente.");
        }
        else
        {
            Console.Write("############## Producto no encontrado ##############");
            Console.WriteLine();
        }

        //Devolver al menu principal
        pressKey();
    }

    //Metodo para buscar un producto por nombre
    public static void SearchProduct(List<Product> inventory)
    {
        Console.Clear(); // Limpiar la consola
        Console.WriteLine(
@"###############################################################################################
#                                     BUSCAR UN PRODUCTO                                      #
###############################################################################################");
        Console.WriteLine();
        Console.Write("Ingresa el nombre del producto que deseas buscar --> ");
        string? name = Console.ReadLine();

        //Se busca el producto en la lista de inventario por su nombre
        Product? product = inventory.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        //Se verifica que la busqueda no devuelva null
        if (product != null)
        {
            Console.WriteLine(product);
        }
        else
        {
            Console.WriteLine("############## Producto no encontrado ##############");
        }

        //Devuelve al usuario al menu principal
        pressKey();
    }
    
    //Layout del inventario
    public static void InventoryLayout(List<Product> inventory)
    {
        Console.Clear(); // Limpiar la consola
        Console.WriteLine(
@"###############################################################################################
#                                    LISTADO DE PRODUCTOS                                     #
###############################################################################################");
        Console.WriteLine(
@"                                                                                              
Nro | Nombre                                   | Valor         | Cantidad   | Total                                     
-----------------------------------------------------------------------------------------------");

        double totalGeneral = 0; // Variable para almacenar el total general de todos los productos

        // Iterar sobre cada producto en el inventario
        for (int i = 0; i < inventory.Count; i++)
        {
            Product product = inventory[i];

            // Calculo el total del producto (valor x cantidad)
            double totalProducto = product.UnitPrice * product.AvailableQuantity;
            totalGeneral += totalProducto; // Agrego al total general

            // Mostrar cada producto en la tabla usando formatos fijos para mantener el alineamiento
            Console.WriteLine($"{i,-3} | {product.Name,-40} |  {product.UnitPrice,-12:C} | {product.AvailableQuantity,-10} | {totalProducto,-10:C} ");
        }

        // Mostrar línea separadora y total general
        Console.WriteLine(
@"-----------------------------------------------------------------------------------------------");
        Console.WriteLine(
@$"                      Total general de inventario: {totalGeneral:C}                                 |");
        Console.WriteLine(
@"-----------------------------------------------------------------------------------------------");
    }

    //Metodo para mostrar inventario completo
    public static void DisplayInventory(List<Product> inventory)
    {
        //Invocamos el Layout
        InventoryLayout(inventory);

        //Devolver al usuario al menu principal
        pressKey();
    }

    //Metodo para eliminar un producto del inventario
    public static void DeleteProduct(List<Product> inventory)
    {
        //Invocamos el Layout
        InventoryLayout(inventory);

        //Se le pide nombre al usuario del producto a eliminar
        Console.Write("Ingresa el Nro del producto que deseas eliminar --> ");
        string? input = Console.ReadLine();

        //Se verifica que el input sea un numero valido
        if (int.TryParse(input, out int productIndex) && productIndex >= 0 && productIndex < inventory.Count)
        {
            //Accedo al producto usando el indice
            Product product = inventory[productIndex];

            //Pedimos al usuario confirmacion para eliminar producto
            Console.Write($"¿Estas seguro que deseas eliminar el producto: {product.Name}? (s/n) ");
            char userConfirmation = Console.ReadKey().KeyChar;

            if (char.ToLower(userConfirmation) == 's')
            {
                inventory.Remove(product);
                Console.WriteLine("\nProducto eliminado satisfactoriamente");
            }
            else if (char.ToLower(userConfirmation) == 'n')
            {
                Console.WriteLine("\n############## Eliminacion cancelada ##############");
            }
            else
            {
                Console.WriteLine("\n############## Opcion invalida ##############");
            }

        }
        else
        {
            Console.WriteLine("\n############## Producto no encontrado ##############");
        }

        //Devolver  al menu principal
        pressKey();
    }
}