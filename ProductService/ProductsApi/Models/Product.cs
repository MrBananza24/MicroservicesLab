using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Models;

public class Product
{
    //Код товара
    public Guid Id { get; set; }
    [Required]
    //Название товара
    public string Name { get; set; } = null!;
    //Описание товара
    public string Description { get; set; } = null!;
    //Вес товара
    public float Weight { get; set; }
    //Цвет товара
    public string Color { get; set; } = null!;
}
