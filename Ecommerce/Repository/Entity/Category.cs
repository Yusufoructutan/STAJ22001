﻿public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigasyon Özellikleri
    public ProductCategory ProductCategory { get; set; }
}