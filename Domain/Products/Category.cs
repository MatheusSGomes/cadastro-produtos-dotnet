using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; set; }
    public bool Active { get; set; }

    public Category(string name, string createdBy, string editedBy)
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name", "Nome é obrigatório")
            .IsNotNullOrEmpty(createdBy, "CreatedBy", "Nome de quem criou o a categoria é obrigatório")
            .IsNotNullOrEmpty(editedBy, "EditedBy", "Nome de quem editou a categoria é obrigatório");

            // .IsNullOrEmpty(email, "Email").IsEmail(email, "Email");

        AddNotifications(contract);
        
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;
    }
}