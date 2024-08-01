namespace IWantApp.Domain.Products;

public class Product : Entity
{
    public string Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string Description { get; private set; }
    public bool HasStock { get; private set; }
    public bool Active { get; private set; } = true;
    public decimal Price { get; private set; }
    
    private Product() {}

    public Product(string name, Category? category, string description, bool hasStock, decimal price, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;
        Price = price;

        CreatedBy = createdBy;
        EditedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
    }

    public void Validate()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrEmpty(Name, "Name", "É obrigatório ter um nome")
            .IsGreaterOrEqualsThan(Name, 3, "Name", "Nome deve ter mais do que 3 caracteres.")
            .IsNotNull(Category, "Category", "Categoria não encontrada")
            .IsNotNullOrEmpty(Description, "Description", "Descrição não pode ser vazia")
            .IsGreaterOrEqualsThan(Price, 1, "Price", "Produto não pode custar menos que 1 real")
            .IsGreaterOrEqualsThan(Description, 3, "Description", "Descrição deve ter mais do que 3 caracteres")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy", "Não é possível salvar o produto sem identificar o usuário")
            .IsNotNullOrEmpty(EditedBy, "EditedBy", "Não é possível salvar o produto sem identificar o usuário");

        AddNotifications(contract);
    }
}
